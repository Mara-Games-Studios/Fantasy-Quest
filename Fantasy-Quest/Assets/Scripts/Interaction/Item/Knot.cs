using UnityEngine;

namespace Interaction.Item
{
    [RequireComponent(typeof(Animator))]
    [AddComponentMenu("Scripts/Interaction/Item/Interaction.Item.Knot")]
    internal class Knot : MonoBehaviour, IInteractable
    {
        private enum State
        {
            Left,
            Right
        }

        [SerializeField]
        private bool canCatInteract = true;

        [SerializeField]
        private Animator animator;

        [SerializeField]
        private Animator hintAnimator;

        [SerializeField]
        private State state = State.Left;

        bool IInteractable.CanCatInteract => canCatInteract;

        public void InteractionByCat()
        {
            if (canCatInteract)
            {
                switch (state)
                {
                    case State.Left:
                        Debug.Log("Move To Right");
                        canCatInteract = false;
                        animator.SetTrigger("MoveToRight");
                        hintAnimator.SetTrigger("ToRight");
                        state = State.Right;
                        break;
                    case State.Right:
                        Debug.Log("Move To Left");
                        canCatInteract = false;
                        animator.SetTrigger("MoveToLeft");
                        hintAnimator.SetTrigger("ToLeft");
                        state = State.Left;
                        break;
                }
            }
        }

        public void InteractionByHuman()
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
