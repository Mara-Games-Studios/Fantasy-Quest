﻿using UnityEngine;
using UnityEngine.InputSystem;

namespace Minigames.SquirrelGame
{
    [AddComponentMenu("Scripts/Minigames/SquirrelGame/Minigames.SquirrelGame.Prize")]
    internal class Prize : MonoBehaviour
    {
        private Pat pat = null;
        private Transform target = null;
        private SqirrelGame input;

        private void Awake()
        {
            input = new SqirrelGame();
            input.Player.Grab.performed += GrabPerformed;
        }

        private void OnEnable()
        {
            input.Enable();
        }

        private void OnDisable()
        {
            input.Disable();
        }

        private void GrabPerformed(InputAction.CallbackContext context)
        {
            if (pat != null && !pat.IsPrizeGrabbed)
            {
                pat.IsPrizeGrabbed = true;
                BindToTransform(pat.transform);
            }
        }

        public void BindToTransform(Transform transform)
        {
            target = transform;
        }

        private void Update()
        {
            if (target != null)
            {
                transform.position = target.position;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Pat pat))
            {
                this.pat = pat;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Pat _))
            {
                pat = null;
            }
        }
    }
}