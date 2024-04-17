using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Minigames.SquirrelGame
{
    [AddComponentMenu("Scripts/Minigames/SquirrelGame/Cat/Minigames.SquirrelGame.Cat.Paw")]
    internal class Paw : MonoBehaviour, ISquirrelTouchable
    {
        [RequiredIn(PrefabKind.PrefabInstanceAndNonPrefabInstance)]
        [SerializeField]
        private Manager manager;

        [RequiredIn(PrefabKind.PrefabInstanceAndNonPrefabInstance)]
        [SerializeField]
        private Rigidbody2D rigidBody;

        [SerializeField]
        private float speed = 1.0f;

        [SerializeField]
        private ContactFilter2D contactFilter;

        [ReadOnly]
        [SerializeField]
        private bool inputEnabled = false;

        [RequiredIn(PrefabKind.PrefabInstanceAndNonPrefabInstance)]
        [SerializeField]
        private Transform bindPrizeTransform;
        public Transform BindPrizeTransform => bindPrizeTransform;

        [SerializeField]
        private InputAction moveAction;
        public bool InputEnabled
        {
            set
            {
                inputEnabled = value;
                if (inputEnabled)
                {
                    moveAction.Enable();
                }
                else
                {
                    moveAction.Disable();
                }
            }
            get => inputEnabled;
        }

        [RequiredIn(PrefabKind.PrefabInstanceAndNonPrefabInstance)]
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
        public float Speed
        {
            get => speed;
            set => speed = value;
        }

        [ReadOnly]
        [SerializeField]
        private Vector2 inputVector;

        private void Awake()
        {
            moveAction.Enable();
        }

        public void Refresh()
        {
            rigidBody.position = startPosition.position;
            IsPrizeGrabbed = false;
        }

        private void Update()
        {
            inputVector = moveAction.ReadValue<Vector2>();
        }

        private void FixedUpdate()
        {
            rigidBody.velocity = Speed * Time.fixedDeltaTime * inputVector.normalized;
        }

        public void Touch()
        {
            if (InputEnabled)
            {
                manager.ExitGame(ExitGameState.Lose);
            }
        }

        public void UnTouch() { }

        public void ExitReached()
        {
            if (IsPrizeGrabbed && InputEnabled)
            {
                manager.ExitGame(ExitGameState.Win);
            }
        }
    }

    public interface ISquirrelTouchable
    {
        public void Touch();
        public void UnTouch();
    }
}
