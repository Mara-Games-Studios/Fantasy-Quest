using System;
using System.Collections;
using Sirenix.OdinInspector;
using Spine.Unity;
using UnityEngine;
using AnimationState = Spine.AnimationState;
using Random = UnityEngine.Random;

namespace Minigames.SquirrelGame
{
    [RequireComponent(typeof(SkeletonAnimation))]
    [AddComponentMenu("Scripts/Minigames/SquirrelGame/Squirrel/Squirrel.AnimationBehaviour")]
    public class AnimationBehaviour : MonoBehaviour
    {
        [Serializable]
        private struct RandomizedValue
        {
            public float MinInclusive;
            public float MaxExclusive;
        }

        [Required]
        [SerializeField]
        private SkeletonAnimation skeletonAnimation;

        [Required]
        [SerializeField]
        private AnimationReferenceAsset idle;

        [Required]
        [SerializeField]
        private AnimationReferenceAsset tailMove;

        [SerializeField]
        private RandomizedValue delayBeforeMakeTailMove;

        [SerializeField]
        private RandomizedValue timeBeforeTailMovement;

        private AnimationState animationState;
        private const int TRACK_INDEX = 1;

        private void Start()
        {
            animationState = skeletonAnimation.AnimationState;
            StartIdle();
            _ = StartCoroutine(MakeTailMove());
        }

        private IEnumerator MakeTailMove()
        {
            float time = Random.Range(
                delayBeforeMakeTailMove.MinInclusive,
                delayBeforeMakeTailMove.MaxExclusive
            );
            yield return new WaitForSeconds(time);

            while (true)
            {
                time = Random.Range(
                    timeBeforeTailMovement.MinInclusive,
                    timeBeforeTailMovement.MaxExclusive
                );
                yield return new WaitForSeconds(time);
                MoveTail();
            }
        }

        [Button]
        private void MoveTail()
        {
            _ = animationState.AddAnimation(TRACK_INDEX, tailMove, false, 0);
            StartIdle();
        }

        [Button]
        private void StartIdle()
        {
            _ = animationState.AddAnimation(TRACK_INDEX, idle, true, 0);
        }
    }
}
