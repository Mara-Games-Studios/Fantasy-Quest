using UnityEngine;
using UnityEngine.InputSystem;

namespace Panel.Settings
{
    [AddComponentMenu("Scripts/Panel/Settings/Panel.Settings.Input")]
    internal class Input : MonoBehaviour
    {
        [SerializeField]
        private Controller controller;

        private GameplayInput gameplayInputActions;

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
            if (gameplayInputActions.UI.Pause.WasPressedThisFrame())
            {
                controller.HideSettings();
            }
        }

        private void OnDisable()
        {
            gameplayInputActions.UI.Pause.performed -= PausePerformed;
            gameplayInputActions.Disable();
        }
    }
}
