using Common;
using Configs;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Minigames.MouseInHay
{
    [AddComponentMenu("Scripts/Minigames/MouseInHay/Minigames.MouseInHay.Manager")]
    internal class Manager : MonoBehaviour
    {
        [SerializeField]
        private Transition.End.Controller endController;

        [SerializeField]
        private InputAction quitGameInputAction;

        [Scene]
        [SerializeField]
        private string nextScene;

        private void Awake()
        {
            quitGameInputAction.performed += QuitGameInputActionPerformed;
        }

        private void QuitGameInputActionPerformed(InputAction.CallbackContext context)
        {
            Debug.Log("Exit by key");
            ExitGame();
        }

        public void ExitGame()
        {
            endController.LoadScene(nextScene, TransitionSettings.Instance.MinLoadingDuration);
        }

        private void OnEnable()
        {
            quitGameInputAction.Enable();
        }

        private void OnDisable()
        {
            quitGameInputAction.Disable();
        }
    }
}
