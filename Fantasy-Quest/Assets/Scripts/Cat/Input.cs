using Common.DI;
using Configs;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace Cat
{
    [AddComponentMenu("Scripts/Cat/Cat.Input")]
    internal class Input : InjectingMonoBehaviour
    {
        [Inject]
        private LockerApi lockerSettings;

        [Required]
        [SerializeField]
        private Movement railMovement;
        private GameplayInput input;

        private void Awake()
        {
            input = new GameplayInput();
        }

        private void Update()
        {
            if (!lockerSettings.Api.IsCatMovementLocked)
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
