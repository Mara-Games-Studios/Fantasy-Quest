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

        private AltarGameInput input;

        private void Awake()
        {
            input = new();
            input.Player.Agree.performed += AgreePerformed;
            input.Player.Disagree.performed += DisagreePerformed;
        }

        private void AgreePerformed(InputAction.CallbackContext context)
        {
            hand.ChooseAgree();
        }

        private void DisagreePerformed(InputAction.CallbackContext context)
        {
            hand.ChooseDisagree();
        }

        private void OnEnable()
        {
            input.Enable();
        }

        private void OnDisable()
        {
            input.Disable();
        }
    }
}
