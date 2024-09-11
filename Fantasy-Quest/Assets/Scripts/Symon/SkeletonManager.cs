﻿using Sirenix.OdinInspector;
using Spine.Unity;
using UnityEngine;

namespace Symon
{
    [AddComponentMenu("Scripts/Symon/Symon.SkeletonManager")]
    internal class SkeletonManager : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private SkeletonAnimation symonSkeleton;

        [Required]
        [SerializeField]
        private AnimationReferenceAsset startTalkDownWithBread;

        [Required]
        [SerializeField]
        private AnimationReferenceAsset talkDownWithBread;

        [Required]
        [SerializeField]
        private AnimationReferenceAsset finishTalkDownWithBread;

        public void TellDownWithBread(float duration)
        {
            string animation = symonSkeleton.AnimationName;

            float talkDuration =
                duration
                - (
                    startTalkDownWithBread.Animation.Duration
                    + finishTalkDownWithBread.Animation.Duration
                    + talkDownWithBread.Animation.Duration
                );
            _ = symonSkeleton.state.SetAnimation(0, startTalkDownWithBread.Animation, false);
            _ = symonSkeleton.state.AddAnimation(0, talkDownWithBread.Animation, true, 0);
            _ = symonSkeleton.state.AddAnimation(
                0,
                finishTalkDownWithBread.Animation,
                false,
                talkDuration
            );

            _ = symonSkeleton.state.AddAnimation(0, animation, true, 0);
        }
    }
}