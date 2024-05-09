using Spine.Unity;
using UnityEngine;

namespace LevelSpecific.MainMenu
{
    [AddComponentMenu(
        "Scripts/LevelSpecific/MainMenu/LevelSpecific.MainMenu.EyesAnimationController"
    )]
    internal class EyesAnimationController : MonoBehaviour
    {
        [SpineAnimation]
        [SerializeField]
        private string blinkAnimation;

        private SkeletonAnimation skeletonAnimation;

        [SerializeField]
        private float minDelay = 1;

        [SerializeField]
        private float maxDelay = 5;

        private void Start()
        {
            skeletonAnimation = GetComponent<SkeletonAnimation>();
            WaitAndPlay(Random.Range(minDelay, maxDelay));
        }

        private void WaitAndPlay(float delay)
        {
            Spine.TrackEntry trackEntry = skeletonAnimation.AnimationState.AddAnimation(
                0,
                blinkAnimation,
                false,
                delay
            );
            trackEntry.Complete += (entry) => WaitAndPlay(Random.Range(minDelay, maxDelay));
        }
    }
}
