using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Minigames.MouseInHay
{
    [AddComponentMenu("Scripts/Minigames/MouseInHay/Minigames.MouseInHay.Manager")]
    internal class Manager : MonoBehaviour
    {
        [SerializeField]
        private InputAction quitGameInputAction;

        [SerializeField]
        private bool disableInputOnStart = true;

        [Required]
        [SerializeField]
        private Hay hay;

        [Required]
        [SerializeField]
        private Paw paw;

        public UnityEvent OnManualExitGame;
        public UnityEvent OnLoseExitGame;
        public UnityEvent OnWinExitGame;

        private void Awake()
        {
            quitGameInputAction.performed += (c) => ManualExitGame();
        }

        private void Start()
        {
            if (disableInputOnStart)
            {
                DisableAllMinigameInput();
            }
        }

        [Button]
        public void StartGame()
        {
            hay.ResetHay();
            hay.Launch();
        }

        public void ManualExitGame()
        {
            OnManualExitGame?.Invoke();
        }

        public void WinExitGame()
        {
            OnWinExitGame?.Invoke();
        }

        public void LoseExitGame()
        {
            OnLoseExitGame?.Invoke();
        }

        [Button]
        public void EnableAllMinigameInput()
        {
            quitGameInputAction.Enable();
            paw.EnableInput();
        }

        [Button]
        public void DisableAllMinigameInput()
        {
            quitGameInputAction.Disable();
            paw.DisableInput();
        }
    }
}
