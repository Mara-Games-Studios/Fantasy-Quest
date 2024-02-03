using UnityEngine;
using UnityEngine.Events;

namespace Panel.Settings
{
    [AddComponentMenu("Scripts/Panel/Settings/Panel.Settings.View")]
    internal class View : MonoBehaviour
    {
        public UnityEvent OnSettingsClosed;
        public UnityEvent OnSettingsOpened;

        // Must be called by animation clip event
        public void HideAnimationEndCallback()
        {
            OnSettingsClosed?.Invoke();
        }

        public void ShowAnimationEndCallback()
        {
            OnSettingsOpened?.Invoke();
        }
    }
}
