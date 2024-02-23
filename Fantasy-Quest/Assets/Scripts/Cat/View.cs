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
            string animationName = state switch
            {
                State.Moving => walkAnimation,
                State.Staying => idleAnimation,
                _ => throw new System.ArgumentException()
            };
            SetAnimation(animationName);
        }

        private void SetAnimation(string animation)
        {
            if (skeletonAnimation.AnimationName != animation)
            {
                skeletonAnimation.AnimationName = animation;
            }
        }
    }
}
