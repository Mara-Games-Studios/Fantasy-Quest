using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

namespace Dialogue
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(AudioSource))]
    [AddComponentMenu("Scripts/Dialogue/Dialogue.SymonSpeaker")]
    public class SymonSpeaker : DialogueSpeaker
    {
        [SerializeField]
        private SkeletonAnimation skeletonAnimation;

        private void Awake()
        {
            if (!SubtitlesViewGameObject.TryGetComponent(out SubtitlesView))
            {
                Debug.LogError(
                    $"{SubtitlesView} is NULL\n{GetType()} callback in {gameObject.name}"
                );
            }

            Voice = new Voice(GetComponent<AudioSource>());
        }

        public override void Stop()
        {
            if (SayCoroutine != null)
            {
                StopCoroutine(SayCoroutine);
                SayCoroutine = null;
            }
            StopSpeakAnimation();
            Voice.Silence();
            SubtitlesView.Hide();
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
