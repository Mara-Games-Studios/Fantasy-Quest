using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Minigames.SquirrelGame
{
    [AddComponentMenu("Scripts/Minigames/SquirrelGame/Minigames.SquirrelGame.Paw")]
    internal class Paw : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private Manager manager;

        [Required]
        [SerializeField]
        private Rigidbody2D rigidBody;

        [SerializeField]
        private float speed = 1.0f;

        [SerializeField]
        private ContactFilter2D contactFilter;

        [SerializeField]
        private InputAction moveAction;
        public InputAction Input => moveAction;

        [Required]
        [SerializeField]
        private Transform startPosition;

        [ReadOnly]
        [SerializeField]
        private bool isPrizeGrabbed = false;
        public bool IsPrizeGrabbed
        {
            get => isPrizeGrabbed;
            set => isPrizeGrabbed = value;
        }

        [ReadOnly]
        [SerializeField]
        private Vector2 inputVector;

        private void Awake()
        {
            moveAction.Enable();
        }

        public void RestorePosition()
        {
            transform.position = startPosition.position;
        }

        private void Update()
        {
            inputVector = moveAction.ReadValue<Vector2>();
        }

        private void FixedUpdate()
        {
            rigidBody.velocity = speed * Time.fixedDeltaTime * inputVector.normalized;
        }

        public void SquirrelTouch()
        {
            manager.ExitGame(ExitGameState.Lose);
        }

        public void ExitReached()
        {
            if (IsPrizeGrabbed)
            {
                manager.ExitGame(ExitGameState.Win);
            }
        }
    }
}
