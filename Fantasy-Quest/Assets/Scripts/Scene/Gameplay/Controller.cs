using Audio;
using Common;
using Configs;
using UnityEngine;

namespace Scene.Gameplay
{
    [AddComponentMenu("Scripts/Scene/Gameplay/Scene.Gameplay.Controller")]
    internal class Controller : MonoBehaviour
    {
        [SerializeField]
        private Input gameplayInput;

        [SerializeField]
        private Cutscene.Manager cutsceneManager;

        [SerializeField]
        private Dialogue.Manager dialogueManager;

        [SerializeField]
        private Panel.Settings.Controller settingsController;

        // Called by Input Manager or UI buttons
        public void OpenSettings()
        {
            Time.timeScale = 0.0f;
            ISceneSingleton<MusicManager>.Instance.PauseMusic();
            gameplayInput.enabled = false;
            cutsceneManager.Pause();
            dialogueManager.Pause();
            settingsController.ShowSettings();
            LockerSettings.Instance.LockAll();
        }

        // Called by Settings controller callback
        public void SettingsClosed()
        {
            gameplayInput.enabled = true;
            ISceneSingleton<MusicManager>.Instance.ResumeMusic();
            LockerSettings.Instance.UnlockAll();
            Time.timeScale = 1.0f;
            cutsceneManager.LockFromSettings();
            cutsceneManager.Resume();
            dialogueManager.Resume();
        }

        public void DisableInput()
        {
            gameplayInput.enabled = false;
        }

        public void EnableInput()
        {
            gameplayInput.enabled = true;
        }
    }
}
