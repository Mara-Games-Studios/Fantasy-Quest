using Sirenix.OdinInspector;
using UI.Pages;
using UnityEngine;

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
            int i = 0;
            while (GetCurrent() != horizontalSlider.Current)
            {
                horizontalSlider.MoveRight();
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
            //CanvasGroup selected = horizontalSlider.ElementsCanvasGroup[index];
            //if (selected == yes)
            //{
            //    SubtitlesSettings.SetShowSubtitles(true);
            //}
            //else if (selected == no)
            //{
            //    SubtitlesSettings.SetShowSubtitles(false);
            //}
            //else
            //{
            //    Debug.LogError("unknows selected CanvasGroup");
            //}
        }

        public CanvasGroup GetCurrent()
        {
            return russian;
            //return SubtitlesSettings.IsSubtitlesShowing ? yes : no;
        }
    }
}
