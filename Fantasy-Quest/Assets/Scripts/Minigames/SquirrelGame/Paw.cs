﻿using Sirenix.OdinInspector;
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

        [ReadOnly]
        [SerializeField]
        private bool inputEnabled = false;

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
            transform.position = startPosition.position;
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

        public void SquirrelTouch()
        {
            if (InputEnabled)
            {
                manager.ExitGame(ExitGameState.Lose);
            }
        }

        public void ExitReached()
        {
            if (IsPrizeGrabbed && InputEnabled)
            {
                manager.ExitGame(ExitGameState.Win);
            }
        }
    }
}
