using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [AddComponentMenu("Scripts/UI/UI.SliderMusicSetter")]
    internal class SliderMusicSetter : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private Slider slider;

        private void Start()
        {
            slider.value = Configs.AudioSettings.Instance.MusicValue;
        }
    }
}
