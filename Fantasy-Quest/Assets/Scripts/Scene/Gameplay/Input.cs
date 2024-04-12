using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Scene.Gameplay
{
    [AddComponentMenu("Scripts/Scene/Gameplay/Scene.Gameplay.Input")]
    internal class Input : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private Controller gameplayController;

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
                gameplayController.SwitchSettings();
            }
        }

        private void OnDisable()
        {
            gameplayInputActions.UI.Pause.performed -= PausePerformed;
            gameplayInputActions.Disable();
        }
    }
}
