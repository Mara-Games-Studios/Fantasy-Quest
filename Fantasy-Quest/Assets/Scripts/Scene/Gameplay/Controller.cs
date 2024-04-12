﻿using Audio;
using Common;
using Configs;
using Sirenix.OdinInspector;
using UnityEngine;

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
            ISceneSingleton<MusicManager>.Instance.PauseMusic();
            pauseShowed = true;
            cutsceneManager.Pause();
            dialogueManager.Pause();
            settingsPage.ShowFromStart();
            LockerSettings.Instance.LockAll();
        }

        // Called by Settings controller callback
        [Button]
        public void SettingsClosed()
        {
            pauseShowed = false;
            settingsPage.HideToEnd();
            ISceneSingleton<MusicManager>.Instance.ResumeMusic();
            LockerSettings.Instance.UnlockAll();
            Time.timeScale = 1.0f;
            cutsceneManager.LockFromSettings();
            cutsceneManager.Resume();
            dialogueManager.Resume();
        }
    }
}
