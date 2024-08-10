using Common.DI;
using Configs;
using DI.Project.Services;
using Sirenix.OdinInspector;
using UI.Pause;
using UnityEngine;
using VContainer;

namespace Scene.Gameplay
{
    [AddComponentMenu("Scripts/Scene/Gameplay/Scene.Gameplay.Controller")]
    internal class Controller : InjectingMonoBehaviour
    {
        [Inject]
        private SoundsManager soundsManager;

        [Inject]
        private CursorController cursorController;

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

        [Required((InfoMessageType)PrefabKind.PrefabInstanceAndNonPrefabInstance)]
        [SerializeField]
        private DarkBackground background;
        private bool animationLocker = true;

        public void SwitchSettings()
        {
            if (!pauseShowed)
            {
                OpenSettings();
            }
            else if (pausePanel.activeSelf && animationLocker)
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
            settingsPage.ShowFromStart();
            LockerSettings.Instance.LockAll();

            cursorController.UnLockCursor();

            background.Show();
        }

        // Called by Settings controller callback
        [Button]
        public void SettingsClosed()
        {
            settingsPage.HideToEnd();
            animationLocker = false;
            background.Hide(() =>
            {
                pauseShowed = false;
                LockerSettings.Instance.UnlockAll();
                Time.timeScale = 1.0f;
                cutsceneManager.LockFromSettings();
                cutsceneManager.Resume();
                soundsManager.ResumeSound();
                animationLocker = true;
                cursorController.LockCursor();
            });
        }
    }
}
