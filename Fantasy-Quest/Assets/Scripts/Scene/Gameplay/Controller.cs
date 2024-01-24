using UnityEngine;
using UnityEngine.Events;

namespace Scene.Gameplay
{
    [AddComponentMenu("Scripts/Scene/Gameplay/Scene.Gameplay.Controller")]
    internal class Controller : MonoBehaviour
    {
        [SerializeField]
        private Panel.Settings.Controller settingsController;

        public UnityEvent OnSettingsOpened;
        public UnityEvent OnSettingsClosed;

        // Called by Input Manager or UI buttons
        public void OpenSettings()
        {
            OnSettingsOpened?.Invoke();
            Time.timeScale = 0.0f;
            settingsController.ShowSettings();
        }

        // Called by Settings controller callback
        public void SettingsClosed()
        {
            Time.timeScale = 1.0f;
            OnSettingsClosed?.Invoke();
        }
    }
}
