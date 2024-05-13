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

        //[ReadOnly]
        //[SerializeField]
        //private string previousAnimation;

        private void Awake()
        {
            //string currentAnimation = skeletonAnimation.AnimationName;
            skeletonAnimation.AnimationState.Start += AnimationStateStart;
        }

        private void AnimationStateStart(Spine.TrackEntry trackEntry)
        {
            AnimationInfo founded = animationInfos.FirstOrDefault(x =>
                x.Asset.Animation.Name == trackEntry.Animation.Name
            );

            if (founded != null)
            {
                jumpSound.PlayClipDelayed((ulong)founded.Delay);
            }
        }
    }
}
