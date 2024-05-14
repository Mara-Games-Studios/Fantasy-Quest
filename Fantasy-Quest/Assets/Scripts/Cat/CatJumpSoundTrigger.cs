using System;
using System.Collections.Generic;
using System.Linq;
using Audio;
using Sirenix.OdinInspector;
using Spine.Unity;
using UnityEngine;

namespace Cat
{
    [AddComponentMenu("Scripts/Cat/Cat.CatJumpSoundTrigger")]
    internal class CatJumpSoundTrigger : MonoBehaviour
    {
        [Serializable]
        private class AnimationInfo
        {
            public AnimationReferenceAsset Asset;
            public float Delay;
        }

        [Required]
        [SerializeField]
        private SkeletonAnimation skeletonAnimation;

        [Required]
        [SerializeField]
        private SoundPlayer jumpSound;

        [SerializeField]
        private List<AnimationInfo> animationInfos;

        [ReadOnly]
        [SerializeField]
        private string previousAnimation;

        private void Update()
        {
            string currentAnimation = skeletonAnimation.AnimationName;
            if (currentAnimation != previousAnimation)
            {
                AnimationStateStart(currentAnimation);
                previousAnimation = currentAnimation;
            }
        }

        private void AnimationStateStart(string animationName)
        {
            AnimationInfo founded = animationInfos.FirstOrDefault(x =>
                x.Asset.Animation.Name == animationName
            );

            if (founded != null)
            {
                jumpSound.PlayClipDelayed(founded.Delay);
            }
        }
    }
}
