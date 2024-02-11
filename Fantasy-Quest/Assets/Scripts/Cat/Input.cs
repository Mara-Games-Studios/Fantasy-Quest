using Configs;
using UnityEngine;

namespace Cat
{
    [AddComponentMenu("Scripts/Cat/Cat.Input")]
    internal class Input : MonoBehaviour
    {
        [SerializeField]
        private Movement railMovement;
        private GameplayInput input;

        private void Awake()
        {
            input = new GameplayInput();
        }

        private void Update()
        {
            if (!LockerSettings.Instance.IsCatMovementLocked)
            {
                float moveInput = input.Player.HorizontalMove.ReadValue<float>();
                railMovement.Move(moveInput * Time.deltaTime);
            }
        }

        public void OnEnable()
        {
            input.Enable();
        }

        public void OnDisable()
        {
            input.Disable();
        }
    }
}
