using Rails;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Cat
{
    [AddComponentMenu("Scripts/Scripts/Cat/Cat.TriggerWithButton")]
    internal class TriggerWithButton : MonoBehaviour
    {
        [ReadOnly]
        [SerializeField]
        private bool isInTrigger = false;

        [SerializeField]
        private RailsImpl rails;

        [Required]
        [SerializeField]
        private Point point;

        private Movement cat;

        private GameplayInput input;

        private void Awake()
        {
            input = new GameplayInput();
        }

        private void Update()
        {
            if (isInTrigger && input.Player.DownJump.WasPressedThisFrame())
            {
                cat.RemoveFromRails();
                cat.SetOnRails(rails, point);
                cat = null;
                isInTrigger = false;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Movement cat))
            {
                this.cat = cat;
                isInTrigger = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Movement _))
            {
                cat = null;
                isInTrigger = false;
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
