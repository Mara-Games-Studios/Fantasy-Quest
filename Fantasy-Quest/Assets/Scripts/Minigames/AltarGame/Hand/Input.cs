using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Minigames.AltarGame.Hand
{
    [AddComponentMenu("Scripts/Minigames/AltarGame/Hand/Minigames.AltarGame.Hand.Input")]
    internal class Input : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private HandImpl hand;

        [SerializeField]
        private InputAction agreeAction;

        [SerializeField]
        private InputAction disagreeAction;

        private void Awake()
        {
            agreeAction.performed += (c) => hand.ChooseAgree();
            disagreeAction.performed += (c) => hand.ChooseDisagree();
        }

        public void Enable()
        {
            agreeAction.Enable();
            disagreeAction.Enable();
        }

        public void Disable()
        {
            agreeAction.Disable();
            disagreeAction.Disable();
        }
    }
}
