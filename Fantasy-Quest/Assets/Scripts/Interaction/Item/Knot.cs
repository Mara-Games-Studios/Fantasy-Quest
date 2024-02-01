using UnityEngine;

namespace Interaction.Item
{
    [RequireComponent(typeof(Animator))]
    [AddComponentMenu("Scripts/Interaction/Item/Interaction.Item.Knot")]
    internal class Knot : MonoBehaviour, IInteractable
    {
        private enum KnotState
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
        private KnotState state = KnotState.Left;

        bool IInteractable.CanCatInteract => canCatInteract;

        public void InteractionByCat()
        {
            if (canCatInteract)
            {
                switch (state)
                {
                    case KnotState.Left:
                        animator.SetTrigger("MoveToRight");
                        hintAnimator.SetTrigger("ToRight");
                        state = KnotState.Right;
                        break;
                    case KnotState.Right:
                        animator.SetTrigger("MoveToLeft");
                        hintAnimator.SetTrigger("ToLeft");
                        state = KnotState.Left;
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
