using Common.DI;
using DI.Project.Services;
using Sirenix.OdinInspector;
using UI.Pages;
using UnityEngine;
using VContainer;

namespace UI
{
    [AddComponentMenu("Scripts/UI/UI.LanguageSliderListener")]
    internal class LanguageSliderListener : InjectingMonoBehaviour
    {
        [Required]
        [SerializeField]
        private HorizontalSlider horizontalSlider;

        [Required]
        [SerializeField]
        private CanvasGroup russian;

        [Required]
        [SerializeField]
        private CanvasGroup english;

        [Required]
        [SerializeField]
        private CanvasGroup belarusian;

        [Inject]
        private Localization localizationService;

        private void OnEnable()
        {
            horizontalSlider.OnElementIndexChanged += HorizontalSliderElementIndexChanged;
        }

        private void OnDisable()
        {
            horizontalSlider.OnElementIndexChanged -= HorizontalSliderElementIndexChanged;
        }

        private void Start()
        {
            CanvasGroup locale = GetCurrentLocale();
            if (!horizontalSlider.ElementsCanvasGroup.Contains(locale))
            {
                Debug.LogError("No needed locale variant");
                return;
            }

            while (locale != horizontalSlider.Current)
            {
                horizontalSlider.MoveRightImmediately();
            }
        }

        private void HorizontalSliderElementIndexChanged(int index)
        {
            CanvasGroup selected = horizontalSlider.ElementsCanvasGroup[index];
            int localeId = FromCanvasGroup(selected);
            _ = localizationService.SetLocale(localeId);
        }

        public CanvasGroup GetCurrentLocale()
        {
            int index = localizationService.GetLocaleID();
            return index switch
            {
                0 => belarusian,
                1 => english,
                2 => russian,
                _ => russian,
            };
        }

        public int FromCanvasGroup(CanvasGroup canvasGroup)
        {
            if (canvasGroup == belarusian)
            {
                return 0;
            }
            if (canvasGroup == english)
            {
                return 1;
            }
            if (canvasGroup == russian)
            {
                return 2;
            }
            return 0;
        }
    }
}
