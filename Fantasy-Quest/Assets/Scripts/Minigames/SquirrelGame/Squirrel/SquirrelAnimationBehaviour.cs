using System;
using System.Collections;
using System.Diagnostics;
using Sirenix.OdinInspector;
using Spine;
using Spine.Unity;
using UnityEngine;
using AnimationState = Spine.AnimationState;
using Random = UnityEngine.Random;

namespace Minigames.SquirrelGame
{
    [RequireComponent(typeof(SkeletonAnimation))]
    [AddComponentMenu("Scripts/Minigames/SquirrelGame/Squirrel/Squirrel.SquirrelAnimationBehaviour")]
    public class SquirrelAnimationBehaviour : MonoBehaviour
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
            StartCoroutine(MakeTailMove());
        }

        private IEnumerator MakeTailMove()
        {
            var time = Random.Range(delayBeforeMakeTailMove.MinInclusive, delayBeforeMakeTailMove.MaxExclusive);
            yield return new WaitForSeconds(time);
            
            while (true)
            {
                time = Random.Range(timeBeforeTailMovement.MinInclusive, timeBeforeTailMovement.MaxExclusive);
                yield return new WaitForSeconds(time);
                MoveTail();
            }
        }

        [Button]
        private void MoveTail()
        {
            animationState.AddAnimation(TRACK_INDEX, tailMove, false, 0);
            StartIdle();
        }

        [Button]
        private void StartIdle()
        {
            animationState.AddAnimation(TRACK_INDEX, idle, true, 0);
        }
    }
}
