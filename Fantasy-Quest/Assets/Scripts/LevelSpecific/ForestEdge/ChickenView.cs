using System.Collections;
using Sirenix.OdinInspector;
using Spine.Unity;
using UnityEngine;

namespace LevelSpecific.ForestEdge
{
    [AddComponentMenu("Scripts/LevelSpecific/ForestEdge/LevelSpecific.ForestEdge.ChickenView")]
    internal class ChickenView : MonoBehaviour
    {
        [Header("Skeleton and script")]
        [Required]
        [SerializeField]
        private SkeletonAnimation skeletonAnimation;

        [Required]
        [SerializeField]
        private ChickenBehaviour chickenBehaviour;

        [Header("Animations")]
        [SerializeField]
        private AnimationReferenceAsset idleAnimation;

        [SerializeField]
        private AnimationReferenceAsset walkAnimation;

        [SerializeField]
        private AnimationReferenceAsset runAnimation;

        private void OnEnable()
        {
            chickenBehaviour.StateChanged += SwitchAnimation;
        }

        private void SwitchAnimation(ChickenBehaviour.ChickenState state)
        {
            _ = StartCoroutine(WaitOffset(state));
        }

        private void ChangeWatchDir()
        {
            float direction = chickenBehaviour.RandomPoint.x - transform.position.x;
            if (direction < 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }

        private void OnDisable()
        {
            chickenBehaviour.StateChanged -= SwitchAnimation;
        }

        private IEnumerator WaitOffset(ChickenBehaviour.ChickenState state)
        {
            yield return new WaitForSeconds(0.1f);
            switch (state)
            {
                case ChickenBehaviour.ChickenState.Idle:
                    _ = skeletonAnimation.AnimationState.SetAnimation(0, idleAnimation, true);
                    break;
                case ChickenBehaviour.ChickenState.Walk:
                    ChangeWatchDir();
                    _ = skeletonAnimation.AnimationState.SetAnimation(0, walkAnimation, true);
                    break;
                case ChickenBehaviour.ChickenState.Run:
                    ChangeWatchDir();
                    _ = skeletonAnimation.AnimationState.SetAnimation(0, runAnimation, true);
                    break;
            }
        }
    }
}
