﻿using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Minigames.SquirrelGame
{
    [AddComponentMenu("Scripts/Minigames/SquirrelGame/Minigames.SquirrelGame.Prize")]
    internal class Prize : MonoBehaviour
    {
        [SerializeField]
        private InputAction grabAction;
        public InputAction Input => grabAction;

        [Required]
        [SerializeField]
        private Transform startBindTransform;

        private Paw paw = null;
        private Transform target = null;

        private void Awake()
        {
            grabAction.performed += GrabPerformed;
            grabAction.Enable();
        }

        private void Start()
        {
            RestorePosition();
        }

        public void RestorePosition()
        {
            target = startBindTransform;
        }

        private void GrabPerformed(InputAction.CallbackContext context)
        {
            if (paw != null && !paw.IsPrizeGrabbed)
            {
                paw.IsPrizeGrabbed = true;
                target = paw.transform;
            }
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
            if (collision.TryGetComponent(out Paw paw))
            {
                this.paw = paw;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Paw _))
            {
                paw = null;
            }
        }
    }
}
