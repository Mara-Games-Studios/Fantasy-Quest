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

        protected override void Awake()
        {
            if (skeletonAnimation == null)
            {
                Debug.LogError("There's no skeleton animation(Symon)\n");
            }
            base.Awake();
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
            _ = skeletonAnimation.AnimationState.SetAnimation(0, "talk", true);
        }

        protected void StopSpeakAnimation()
        {
            _ = skeletonAnimation.AnimationState.SetAnimation(0, "idle", true);
        }
    }
}
