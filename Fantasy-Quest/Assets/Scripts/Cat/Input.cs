using Configs;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Cat
{
    [AddComponentMenu("Scripts/Cat/Cat.Input")]
    internal class Input : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private Movement railMovement;
        private GameplayInput input;

        public UnityEvent OnCatInteraction;

        private void Awake()
        {
            input = new GameplayInput();
            input.Player.CatInteraction.performed += (c) => OnCatInteraction?.Invoke();
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
