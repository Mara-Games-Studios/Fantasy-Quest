using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

namespace Dialogue
{
    [AddComponentMenu("Scripts/Dialogue/Dialogue.SymonSpeaker")]
    public class SymonSpeaker : DialogueSpeaker
    {
        [SerializeField]
        private SkeletonAnimation skeletonAnimation;

        [SerializeField]
        private AnimationReferenceAsset idleAniamtion;

        [SerializeField]
        private AnimationReferenceAsset talkAnimation;

        protected override void Start()
        {
            if (skeletonAnimation == null)
            {
                Debug.LogError("There's no skeleton animation(Symon)\n");
            }
            base.Start();
        }

        public override void Stop()
        {
            base.Stop();
            StopSpeakAnimation();
        }

        protected override void Say(List<Replica> speech)
        {
            Stop();
            StartSpeakAnimation();
            SayCoroutine = StartCoroutine(SayList(speech));
        }

        protected void StartSpeakAnimation()
        {
            _ = skeletonAnimation.AnimationState.SetAnimation(0, talkAnimation, true);
        }

        protected void StopSpeakAnimation()
        {
            _ = skeletonAnimation.AnimationState.SetAnimation(0, idleAniamtion, true);
        }
    }
}
