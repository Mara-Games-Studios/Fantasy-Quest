using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

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
        [SerializeField]
        private float statusPanelShowDuration = 1f;

        [Required]
        [SerializeField]
        private StatusPanel statusPanel;

        [Required]
        [SerializeField]
        private Paw paw;

        [Required]
        [SerializeField]
        private Prize prize;

        [SerializeField]
        private InputAction exitAction;

        public UnityEvent OnGameFinishedWin;
        public UnityEvent OnGameFinishedLose;
        public UnityEvent OnGameFinishedManual;

        private void Awake()
        {
            exitAction.performed += (c) => ExitGame(ExitGameState.Manual);
        }

        public void ExitGame(ExitGameState exitState)
        {
            DisableAllMinigameInput();
            statusPanel.ShowPanel(
                exitState,
                () => _ = StartCoroutine(WaitRoutine(statusPanelShowDuration, exitState))
            );
        }

        private IEnumerator WaitRoutine(float time, ExitGameState exitState)
        {
            yield return new WaitForSeconds(time);
            switch (exitState)
            {
                case ExitGameState.Win:
                    prize.gameObject.SetActive(false);
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
            paw.RestorePosition();
            statusPanel.HidePanel();
        }

        [Button]
        public void EnableAllMinigameInput()
        {
            exitAction.Enable();
            paw.Input.Enable();
            prize.Input.Enable();
        }

        [Button]
        public void DisableAllMinigameInput()
        {
            exitAction.Disable();
            paw.Input.Disable();
            prize.Input.Disable();
        }
    }
}
