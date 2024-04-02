using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Minigames.SquirrelGame
{
    [AddComponentMenu("Scripts/Minigames/SquirrelGame/Minigames.SquirrelGame.QuitInput")]
    internal class QuitInput : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private Manager manager;

        [SerializeField]
        private InputAction quitGameInputAction;

        private void Awake()
        {
            quitGameInputAction.performed += (c) => manager.ExitGame(ExitGameState.Manual);
        }

        public void OnEnable()
        {
            quitGameInputAction.Enable();
        }

        public void OnDisable()
        {
            quitGameInputAction.Disable();
        }
    }
}
