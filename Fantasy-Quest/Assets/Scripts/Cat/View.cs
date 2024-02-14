using Spine.Unity;
using UnityEngine;

namespace Cat
{
    [AddComponentMenu("Scripts/Cat/Cat.View")]
    internal class View : MonoBehaviour
    {
        [SerializeField]
        private Movement catMovement;

        [SerializeField]
        private SkeletonAnimation skeletonAnimation;

        [SerializeField]
        [SpineAnimation]
        private string idleAnimation;

        [SerializeField]
        [SpineAnimation]
        private string walkAnimation;

        private void OnEnable()
        {
            catMovement.OnStateChanged += StateChanged;
        }

        private void OnDisable()
        {
            catMovement.OnStateChanged -= StateChanged;
        }

        private void StateChanged(State state)
        {
            switch (state)
            {
                case State.Moving:
                    if (skeletonAnimation.AnimationName != walkAnimation)
                    {
                        skeletonAnimation.AnimationName = walkAnimation;
                    }
                    break;
                case State.Staying:
                    if (skeletonAnimation.AnimationName != idleAnimation)
                    {
                        skeletonAnimation.AnimationName = idleAnimation;
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
