using Sirenix.OdinInspector;
using UnityEngine;

namespace Minigames.SquirrelGame
{
    [AddComponentMenu("Scripts/Minigames/SquirrelGame/Minigames.SquirrelGame.Pat")]
    internal class Pat : MonoBehaviour
    {
        [SerializeField]
        private StatusPanel statusPanel;

        [SerializeField]
        private Rigidbody2D rigidBody;

        [SerializeField]
        private float speed = 1.0f;

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

        [SerializeField]
        private ContactFilter2D contactFilter;

        private SqirrelGame input;

        private void Awake()
        {
            input = new SqirrelGame();
        }

        private void OnEnable()
        {
            input.Enable();
        }

        private void OnDisable()
        {
            input.Disable();
        }

        private void Update()
        {
            inputVector = input.Player.PatMove.ReadValue<Vector2>();
        }

        private void FixedUpdate()
        {
            rigidBody.velocity = speed * Time.fixedDeltaTime * inputVector.normalized;
        }

        public void SquirrelTouch()
        {
            input.Disable();
            statusPanel.ShowPanel(StatusPanel.State.Lose);
        }

        public void ExitReached()
        {
            if (IsPrizeGrabbed)
            {
                input.Disable();
                statusPanel.ShowPanel(StatusPanel.State.Win);
            }
        }
    }
}
