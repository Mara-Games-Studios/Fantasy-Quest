using Spine.Unity;
using UnityEngine;

namespace Cat
{
    [AddComponentMenu("Scripts/Cat/Cat.DialogueAnimationSwitch")]
    internal class DialogueAnimationSwitch : MonoBehaviour
    {
        [SerializeField]
        private SkeletonAnimation skeletonAnimation;

        [SerializeField]
        private AnimationReferenceAsset seatAniamtion;

        [SerializeField]
        private AnimationReferenceAsset idleAnimation;

        public void SetSeatAnimation()
        {
            _ = skeletonAnimation.AnimationState.SetAnimation(0, seatAniamtion, true);
        }

        public void SetIdleAnimation()
        {
            _ = skeletonAnimation.AnimationState.SetAnimation(0, idleAnimation, true);
        }
    }
}
