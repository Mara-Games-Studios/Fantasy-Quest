using Configs;
using Sirenix.OdinInspector;
using UI.Pages;
using UnityEngine;

namespace UI
{
    [AddComponentMenu("Scripts/UI/UI.SubtitlesSliderListener")]
    internal class SubtitlesSliderListener : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private HorizontalSlider horizontalSlider;

        [Required]
        [SerializeField]
        private CanvasGroup yes;

        [Required]
        [SerializeField]
        private CanvasGroup no;

        private SubtitlesSettings SubtitlesSettings =>
            SingletonScriptableObject<SubtitlesSettings>.Instance;

        private void Awake()
        {
            horizontalSlider.OnElementIndexChanged += HorizontalSliderElementIndexChanged;
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
            CanvasGroup selected = horizontalSlider.ElementsCanvasGroup[index];
            if (selected == yes)
            {
                SubtitlesSettings.SetShowSubtitles(true);
            }
            else if (selected == no)
            {
                SubtitlesSettings.SetShowSubtitles(false);
            }
            else
            {
                Debug.LogError("unknows selected CanvasGroup");
            }
        }

        public CanvasGroup GetCurrent()
        {
            return SubtitlesSettings.IsSubtitlesShowing ? yes : no;
        }
    }
}
