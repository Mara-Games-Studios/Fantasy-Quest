using System.Collections;
using System.Diagnostics;
using Sirenix.OdinInspector;
using Spine;
using Spine.Unity;
using UnityEngine;
using AnimationState = Spine.AnimationState;

namespace Minigames.SquirrelGame
{
    [RequireComponent(typeof(SkeletonAnimation))]
    [AddComponentMenu("Scripts/Minigames/SquirrelGame/SquirrelGame.SquirrelAnimationBehaviour")]
    public class SquirrelAnimationBehaviour : MonoBehaviour
    {
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
        private float delayBeforeMakeTailMove = 1f;
        
        [SerializeField] 
        private float timeBeforeTailMovement = 5f;
        
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
            yield return new WaitForSeconds(delayBeforeMakeTailMove);
            
            while (true)
            {
                yield return new WaitForSeconds(timeBeforeTailMovement);
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
