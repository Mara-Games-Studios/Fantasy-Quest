using Interaction.Item;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace LevelSpecific.House
{
    [RequireComponent(typeof(Animator))]
    [AddComponentMenu("Scripts/LevelSpecific/House/LevelSpecific.House.Knot")]
    internal class Knot : MonoBehaviour, IInteractable
    {
        private enum State
        {
            Left,
            Right
        }

        [InfoBox("CALLED BY E")]
        [SerializeField]
        private bool canCatInteract = true;

        [SerializeField]
        private Animator animator;

        [SerializeField]
        private State state = State.Left;

        public UnityEvent OnKnotHinted;

        public void InteractionByCat()
        {
            if (canCatInteract)
            {
                OnKnotHinted?.Invoke();
                switch (state)
                {
                    case State.Left:
                        canCatInteract = false;
                        animator.SetTrigger("MoveToRight");
                        state = State.Right;
                        break;
                    case State.Right:
                        canCatInteract = false;
                        animator.SetTrigger("MoveToLeft");
                        state = State.Left;
                        break;
                }
            }
        }

        public void InteractByHuman()
        {
            return;
        }

        public void SetTrueCanInteract()
        {
            canCatInteract = true;
        }

        public void SetFalseCanInteract()
        {
            canCatInteract = false;
        }
    }
}
