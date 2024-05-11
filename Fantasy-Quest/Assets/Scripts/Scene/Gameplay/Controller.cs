using Audio;
using Configs;
using Sirenix.OdinInspector;
using UI.Pause;
using UnityEngine;
using Utils;

namespace Scene.Gameplay
{
    [AddComponentMenu("Scripts/Scene/Gameplay/Scene.Gameplay.Controller")]
    internal class Controller : MonoBehaviour
    {
        [RequiredIn(PrefabKind.PrefabInstanceAndNonPrefabInstance)]
        [SerializeField]
        private Cutscene.Manager cutsceneManager;

        [RequiredIn(PrefabKind.PrefabInstanceAndNonPrefabInstance)]
        [SerializeField]
        private Dialogue.Manager dialogueManager;

        [RequiredIn(PrefabKind.PrefabInstanceAndNonPrefabInstance)]
        [SerializeField]
        private UI.Pages.View settingsPage;

        [RequiredIn(PrefabKind.PrefabInstanceAndNonPrefabInstance)]
        [SerializeField]
        private GameObject pausePanel;

        [ReadOnly]
        [SerializeField]
        private bool pauseShowed = false;

        [RequiredIn(PrefabKind.PrefabInstanceAndNonPrefabInstance)]
        [SerializeField]
        private SoundsManager soundsManager;

        [Required]
        [SerializeField]
        private DarkBackground background;

        public void SwitchSettings()
        {
            if (!pauseShowed)
            {
                OpenSettings();
            }
            else if (pausePanel.activeSelf)
            {
                SettingsClosed();
            }
        }

        // Called by Input Manager or UI buttons
        [Button]
        public void OpenSettings()
        {
            Time.timeScale = 0.0f;
            pauseShowed = true;
            cutsceneManager.Pause();
            soundsManager.PauseSound();
            dialogueManager.Pause();
            settingsPage.ShowFromStart();
            LockerSettings.Instance.LockAll();

            CursorLockUnlock.UnLockCursor();

            background.Show();
        }

        // Called by Settings controller callback
        [Button]
        public void SettingsClosed()
        {
            pauseShowed = false;
            settingsPage.HideToEnd();
            LockerSettings.Instance.UnlockAll();
            Time.timeScale = 1.0f;
            cutsceneManager.LockFromSettings();
            cutsceneManager.Resume();
            dialogueManager.Resume();
            soundsManager.ResumeSound();

            CursorLockUnlock.LockCursor();

            background.Hide();
        }
    }
}
