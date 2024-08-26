using System;
using Interaction.Item;
using Sirenix.OdinInspector;
using Spine.Unity;
using UnityEngine;
using UnityEngine.Events;

namespace LevelSpecific.House
{
    [RequireComponent(typeof(Animator))]
    [AddComponentMenu("Scripts/LevelSpecific/House/LevelSpecific.House.NewKnot")]
    internal class NewKnot : MonoBehaviour, IInteractable
    {
        [Serializable]
        private struct Animations
        {
            public AnimationReferenceAsset None;
            public AnimationReferenceAsset MoveRight;
            public AnimationReferenceAsset MoveLeft;
        }

        private enum State
        {
            None,
            MoveLeft,
            MoveRight
        }

        [SerializeField]
        private Animations animations;

        [InfoBox("CALLED BY E")]
        [SerializeField]
        private bool canCatInteract = true;

        [SerializeField]
        private State state = State.None;

        [SerializeField]
        private SkeletonAnimation skeleton;

        //add reils + speed

        public UnityEvent OnKnotHinted;

        public void InteractionByCat()
        {
            if (canCatInteract)
            {
                OnKnotHinted?.Invoke();
                switch (state)
                {
                    case State.MoveLeft:
                        canCatInteract = false;
                        _ = skeleton.AnimationState.SetAnimation(0, animations.MoveRight, false);
                        // animator.SetTrigger("MoveToRight");
                        state = State.MoveRight;
                        break;
                    case State.MoveRight:
                        canCatInteract = false;
                        _ = skeleton.AnimationState.SetAnimation(0, animations.MoveLeft, false);
                        // animator.SetTrigger("MoveToLeft");
                        state = State.MoveLeft;
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
