using Sirenix.OdinInspector;
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

        [SerializeField]
        private float magnetSpeed = 0.7f;

        [Required]
        [SerializeField]
        private Effects.Fade fade;

        private Paw paw = null;
        private Transform target = null;

        private void OnEnable()
        {
            grabAction.performed += GrabPerformed;
            grabAction.Enable();
        }

        private void OnDisable()
        {
            grabAction.performed += GrabPerformed;
            grabAction.Enable();
        }

        private void Start()
        {
            RestorePosition();
            fade.Disappear();
        }

        public void RestorePosition()
        {
            target = startBindTransform;
            fade.Disappear();
        }

        private void GrabPerformed(InputAction.CallbackContext context)
        {
            if (paw != null && !paw.IsPrizeGrabbed)
            {
                paw.IsPrizeGrabbed = true;
                target = paw.BindPrizeTransform;
                fade.Disappear();
            }
        }

        private void Update()
        {
            if (target != null)
            {
                transform.position = Vector3.Lerp(
                    transform.position,
                    target.position,
                    magnetSpeed * Time.deltaTime
                );
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Paw paw))
            {
                this.paw = paw;
                fade.Appear();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Paw _))
            {
                paw = null;
                fade.Disappear();
            }
        }
    }
}
