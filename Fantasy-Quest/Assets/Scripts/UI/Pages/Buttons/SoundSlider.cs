using UnityEngine;
using UnityEngine.UI;
using AudioSettings = Configs.AudioSettings;

namespace UI.Pages
{
    [AddComponentMenu("Scripts/UI/Pages/Buttons/Pages.Buttons.SoundSlider")]
    public class SoundSlider : MonoBehaviour, IHorizontalSlider
    {
        [SerializeField]
        private Slider audioSlider;

        [Range(0, 1)]
        [SerializeField]
        private float step = 0.1f;

        public void MoveLeftWithAnimation()
        {
            if (audioSlider.value <= 0)
            {
                return;
            }

            float audioValue = audioSlider.value;
            audioValue -= step;

            if (audioValue < 0)
            {
                audioValue = 0;
            }

            audioSlider.value = audioValue;
            AudioSettings.Instance.SoundsValue = audioValue;
        }

        public void MoveRightWithAnimation()
        {
            if (audioSlider.value >= 1)
            {
                return;
            }

            float audioValue = audioSlider.value;
            audioValue += step;

            if (audioValue > 1)
            {
                audioValue = 1;
            }

            audioSlider.value = audioValue;
            AudioSettings.Instance.SoundsValue = audioValue;
        }
    }
}
