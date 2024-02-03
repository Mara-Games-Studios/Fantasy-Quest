using Configs;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Panel.Settings
{
    [AddComponentMenu("Scripts/Panel/Settings/Panel.Settings.Controller")]
    internal partial class Controller : MonoBehaviour
    {
        [SerializeField]
        private Input input;

        [SerializeField]
        private Slider musicSlider;

        [SerializeField]
        private Slider volumeSlider;

        [SerializeField]
        private Animator animator;

        [SerializeField]
        [ValueDropdown("@AnimatorBools")]
        private string isShownBoolAnimatorFlagName;

        public UnityEvent OnSettingsClosed;

        public void ShowSettings()
        {
            animator.SetBool(isShownBoolAnimatorFlagName, true);
            LoadFromConfig();
        }

        public void ShowSettingsComplete()
        {
            input.enabled = true;
        }

        // Must be called by view (button) callback
        public void HideSettings()
        {
            animator.SetBool(isShownBoolAnimatorFlagName, false);
            input.enabled = false;
        }

        public void OnSettingsClosedViewCallback()
        {
            OnSettingsClosed?.Invoke();
        }

        private void LoadFromConfig()
        {
            Configs.AudioSettings config = Configs.AudioSettings.Instance;
            musicSlider.value = config.MusicValue;
            volumeSlider.value = config.SoundsValue;
        }

        // Called by volume slider Callback
        public void VolumeValueChanged(float value)
        {
            Configs.AudioSettings.Instance.SoundsValue = value;
        }

        // Called by music slider Callback
        public void MusicValueChanged(float value)
        {
            Configs.AudioSettings.Instance.MusicValue = value;
        }

        public void ShowSubtitlesChanged(Toggle change)
        {
            SubtitlesSettings.Instance.SetShowSubtitles(change.isOn);
        }
    }
}
