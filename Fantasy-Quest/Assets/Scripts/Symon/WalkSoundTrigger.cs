using System.Collections.Generic;
using System.Linq;
using Audio;
using Sirenix.OdinInspector;
using Spine.Unity;
using UnityEngine;

namespace Symon
{
    [AddComponentMenu("Scripts/Symon/Symon.WalkSoundTrigger")]
    internal class WalkSoundTrigger : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private SoundPlayer walkSound;

        [Required]
        [SerializeField]
        private SkeletonAnimation skeletonAnimation;

        [SerializeField]
        private List<AnimationReferenceAsset> triggeringAnimations;
        private IEnumerable<string> TriggeringNames =>
            triggeringAnimations.Select(x => x.Animation.Name);

        private bool isPlayingCorrect = false;

        private void Update()
        {
            Debug.Log(isPlayingCorrect);
            bool currentPlayingCorrect = TriggeringNames.Contains(skeletonAnimation.AnimationName);
            if (isPlayingCorrect != currentPlayingCorrect)
            {
                if (!isPlayingCorrect)
                {
                    walkSound.PlayClip();
                }
                else
                {
                    walkSound.StopClip();
                }
                isPlayingCorrect = currentPlayingCorrect;
            }
        }
    }
}
