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
        private DI.Project.Services.Cursor cursorController;

        [Inject]
        private LockerApi lockerSettings;

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
        public void OpenSettings()
        {
            Time.timeScale = 0.0f;
            pauseShowed = true;
            soundsManager.PauseSound();
            settingsPage.ShowFromStart();
            lockerSettings.Api.LockAll(this);
            cursorController.UnLockCursor();
            background.Show();
        }

        // Called by Settings controller callback
        public void SettingsClosed()
        {
            settingsPage.HideToEnd();
            animationLocker = false;
            background.Hide(() =>
            {
                pauseShowed = false;
                lockerSettings.Api.UnlockAll(this);
                Time.timeScale = 1.0f;
                soundsManager.ResumeSound();
                animationLocker = true;
                cursorController.LockCursor();
            });
        }
    }
}
