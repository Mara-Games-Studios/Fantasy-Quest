﻿using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Minigames.SquirrelGame
{
    internal enum ExitGameState
    {
        Win,
        Lose,
        Manual
    }

    [AddComponentMenu("Scripts/Minigames/SquirrelGame/Minigames.SquirrelGame.Manager")]
    internal class Manager : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private QuitInput quitInput;

        [Required]
        [SerializeField]
        private StatusPanel statusPanel;

        [Required]
        [SerializeField]
        private Paw paw;

        [Required]
        [SerializeField]
        private Prize prize;

        public UnityEvent OnGameFinishedWin;
        public UnityEvent OnGameFinishedLose;
        public UnityEvent OnGameFinishedManual;

        public void ExitGame(ExitGameState exitState)
        {
            quitInput.enabled = false;
            DisableAllMinigameInput();
            statusPanel.ShowPanel(exitState, () => TriggerEvent(exitState));
        }

        private void TriggerEvent(ExitGameState exitState)
        {
            switch (exitState)
            {
                case ExitGameState.Win:
                    OnGameFinishedWin?.Invoke();
                    break;
                case ExitGameState.Lose:
                    OnGameFinishedLose?.Invoke();
                    break;
                case ExitGameState.Manual:
                    OnGameFinishedManual?.Invoke();
                    break;
            }
        }

        [Button]
        public void RefreshGame()
        {
            prize.gameObject.SetActive(true);
            prize.RestorePosition();
            paw.Refresh();
            statusPanel.HidePanel();
        }

        [Button]
        public void EnableAllMinigameInput()
        {
            paw.InputEnabled = true;
            prize.Input.Enable();
        }

        [Button]
        public void DisableAllMinigameInput()
        {
            paw.InputEnabled = false;
            prize.Input.Disable();
        }
    }
}
