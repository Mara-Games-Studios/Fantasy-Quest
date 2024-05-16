using System.Collections;
using Sirenix.OdinInspector;
using UI.Pages;
using UnityEngine;
using UnityEngine.Localization.Settings;

namespace UI
{
    [AddComponentMenu("Scripts/UI/UI.LanguageSliderListener")]
    internal class LanguageSliderListener : MonoBehaviour
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

        private bool active = false;

        public void ChangeLocale(int localeID)
        {
            Debug.Log("Locale changed to " + localeID);
            if (active)
            {
                return;
            }
            _ = StartCoroutine(SetLocale(localeID));
        }

        private IEnumerator SetLocale(int localeID)
        {
            active = true;
            yield return LocalizationSettings.InitializationOperation;
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[
                localeID
            ];
            PlayerPrefs.SetInt("LocaleKey", localeID);
            active = false;
        }

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
            ChangeLocale(PlayerPrefs.GetInt("LocaleKey", 2));
            int i = 0;
            while (GetCurrent() != horizontalSlider.Current)
            {
                horizontalSlider.MoveRightImmediately();
                i++;
                if (i == 50)
                {
                    Debug.LogError("INFINITIE LOOP!");
                    break;
                }
            }
        }

        private void HorizontalSliderElementIndexChanged(int index)
        {
            CanvasGroup selected = horizontalSlider.ElementsCanvasGroup[index];
            ChangeLocale(FromCanvasGroup(selected));
        }

        public CanvasGroup GetCurrent()
        {
            return FromIndex(PlayerPrefs.GetInt("LocaleKey", 2));
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

        public CanvasGroup FromIndex(int index)
        {
            switch (index)
            {
                case 0:
                    return belarusian;
                case 1:
                    return english;
                case 2:
                    return russian;
                default:
                    return russian;
            }
        }
    }
}
