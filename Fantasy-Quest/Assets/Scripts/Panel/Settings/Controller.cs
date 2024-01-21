using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Panel.Settings
{
    [AddComponentMenu("Scripts/Panel/Settings/Panel.Settings.Controller")]
    internal class Controller : MonoBehaviour
    {
        [SerializeField]
        private Slider musicSlider;

        [SerializeField]
        private Slider volumeSlider;

        [SerializeField]
        private Animator animator;

        [SerializeField]
        private AnimatorController controller;

        [SerializeField]
        [ValueDropdown("@" + nameof(AnimatorBools))]
        private string isShownBoolAnimatorFlagName;

        private IEnumerable<string> AnimatorBools =>
            animator == null ? null : controller.parameters.Select(x => x.name);

        public UnityEvent OnSettingsClosed;

        public void ShowSettings()
        {
            animator.SetBool(isShownBoolAnimatorFlagName, true);
            LoadFromConfig();
        }

        // Must be called by view (button) callback
        public void HideSettings()
        {
            animator.SetBool(isShownBoolAnimatorFlagName, false);
        }

        public void OnSettingsClosedViewCallback()
        {
            OnSettingsClosed?.Invoke();
        }

        private void LoadFromConfig()
        {
            Configs.AudioSettings config = Configs.AudioSettings.Instance;
            musicSlider.value = config.MusicValue;
            volumeSlider.value = config.VolumeValue;
        }

        // Called by volume slider Callback
        public void VolumeValueChanged(float value)
        {
            Configs.AudioSettings.Instance.VolumeValue = value;
        }

        // Called by music slider Callback
        public void MusicValueChanged(float value)
        {
            Configs.AudioSettings.Instance.MusicValue = value;
        }
    }
}
