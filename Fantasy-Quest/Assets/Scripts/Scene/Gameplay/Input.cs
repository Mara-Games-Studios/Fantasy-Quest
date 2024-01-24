using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Scene.Gameplay
{
    [AddComponentMenu("Scripts/Scene/Gameplay/Scene.Gameplay.Input")]
    internal class Input : MonoBehaviour
    {
        private GameplayInput gameplayInputActions;

        public UnityEvent EscPressed;

        private void Awake()
        {
            gameplayInputActions = new();
            gameplayInputActions.Enable();
        }

        private void OnEnable()
        {
            gameplayInputActions.Enable();
            gameplayInputActions.UI.Pause.performed += PausePerformed;
        }

        private void PausePerformed(InputAction.CallbackContext context)
        {
            EscPressed?.Invoke();
        }

        private void OnDisable()
        {
            gameplayInputActions.UI.Pause.performed -= PausePerformed;
            gameplayInputActions.Disable();
        }
    }
}
