using Spine.Unity;
using UnityEngine;

namespace Utils
{
    [AddComponentMenu("Scripts/Utils/Utils.SpineAnimationSwitch")]
    internal class SpineTalkAnimationSwitch : MonoBehaviour
    {
        [SerializeField]
        private SkeletonAnimation skeletonAnimation;

        [SerializeField]
        private AnimationReferenceAsset idleAniamtion;

        [SerializeField]
        private AnimationReferenceAsset talkAnimation;

        public void SetIdleAnimation()
        {
            _ = skeletonAnimation.AnimationState.SetAnimation(0, idleAniamtion, true);
        }

        public void SetTalkAnimation()
        {
            _ = skeletonAnimation.AnimationState.SetAnimation(0, talkAnimation, true);
        }
    }
}
