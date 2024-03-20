using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Minigames.MouseInHay
{
    internal enum ExitGameState
    {
        Win,
        Lose,
        Manual
    }

    [AddComponentMenu("Scripts/Minigames/MouseInHay/Minigames.MouseInHay.Manager")]
    internal class Manager : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private Hay hay;

        [Required]
        [SerializeField]
        private Paw paw;

        public UnityEvent OnManualExitGame;
        public UnityEvent OnLoseExitGame;
        public UnityEvent OnWinExitGame;

        [Button]
        public void RefreshGame()
        {
            hay.ResetHay();
            hay.Launch();
        }

        public void ExitGame(ExitGameState gameState)
        {
            switch (gameState)
            {
                case ExitGameState.Win:
                    OnWinExitGame?.Invoke();
                    break;
                case ExitGameState.Lose:
                    OnLoseExitGame?.Invoke();
                    break;
                case ExitGameState.Manual:
                    OnManualExitGame?.Invoke();
                    break;
            }
            OnManualExitGame?.Invoke();
        }

        [Button]
        public void EnableAllMinigameInput()
        {
            paw.EnableInput();
        }

        [Button]
        public void DisableAllMinigameInput()
        {
            paw.DisableInput();
        }
    }
}
