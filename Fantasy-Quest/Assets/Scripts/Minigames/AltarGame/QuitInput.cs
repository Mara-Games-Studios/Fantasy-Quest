using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Minigames.AltarGame
{
    [AddComponentMenu("Scripts/Minigames/AltarGame/Minigames.AltarGame.QuitInput")]
    internal class QuitInput : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private Manager manager;

        [SerializeField]
        private InputAction quitGameInputAction;

        private void Awake()
        {
            quitGameInputAction.performed += (c) => manager.QuitMiniGameManual();
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
