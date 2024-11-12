using System;
using Common.DI;
using Configs;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Hints;
using Interaction;
using Sirenix.OdinInspector;
using Spine.Unity;
using UnityEngine;
using UnityEngine.Events;
using VContainer;

namespace LevelSpecific.House
{
    [AddComponentMenu("Scripts/LevelSpecific/House/LevelSpecific.House.Knot")]
    internal class Knot : InjectingMonoBehaviour, IInteractable
    {
        [Inject]
        private LockerApi lockerSettings;

        [SerializeField]
        [Required]
        private Cat.Movement catMovement;

        [Required]
        [SerializeField]
        private ShowHints hintsShower;

        [SerializeField]
        [Required]
        private SkeletonAnimation knotSkeletonAnimation;

        [SerializeField]
        [Required]
        private SkeletonAnimation catSkeletonAnimation;

        [SerializeField]
        private AnimationReferenceAsset catBallAnimation;

        [SerializeField]
        private float catBallAnimationTimeScale = 3f;

        [SerializeField]
        private float knotMoveDuration = 2f;

        [SerializeField]
        private float knotMoveDistance = 4f;

        [SerializeField]
        private AnimationCurve pushCurve;

        [SerializeField]
        private AnimationCurve bounceCurve;

        [SerializeField]
        private float knotStartPosition;

        [SerializeField]
        private float knotEndPosition;

        [SpineAnimation]
        [SerializeField]
        private string moveLeftKnotAnimationName;

        [SpineAnimation]
        [SerializeField]
        private string moveRightKnotAnimationName;

        [SerializeField]
        private float ballAnimationDelay;

        private bool isKnotMoving = false;
        private bool canMoveRight = false;
        private bool canMoveLeft = false;
        private float KnotPosition => transform.position.x;

        public UnityEvent OnKnotHinted;

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out InteractionImpl _))
            {
                canMoveRight = false;
                canMoveLeft = false;
                float catPosition = catMovement.transform.position.x;

                if (catMovement.Vector == Cat.Vector.Right && KnotPosition - catPosition > 0)
                {
                    canMoveRight = true;
                    hintsShower.ShowingHints(!isKnotMoving);
                }
                else if (catMovement.Vector == Cat.Vector.Left && KnotPosition - catPosition < 0)
                {
                    canMoveLeft = true;
                    hintsShower.ShowingHints(!isKnotMoving);
                }
                else
                {
                    hintsShower.ShowingHints(false);
                }
            }
        }

        public void Interact()
        {
            if (canMoveRight)
            {
                _ = PlayCatAnimation();
                _ = PlayKnotAnimation(moveRightKnotAnimationName, knotMoveDistance, true);
            }
            else if (canMoveLeft)
            {
                _ = PlayCatAnimation();
                _ = PlayKnotAnimation(moveLeftKnotAnimationName, -knotMoveDistance, true);
            }
        }

        private void CallWallBounce()
        {
            if (KnotPosition < knotStartPosition)
            {
                _ = PlayKnotAnimation(moveRightKnotAnimationName, knotMoveDistance, false);
            }
            else if (KnotPosition > knotEndPosition)
            {
                _ = PlayKnotAnimation(moveLeftKnotAnimationName, -knotMoveDistance, false);
            }
        }

        private async UniTask PlayCatAnimation()
        {
            lockerSettings.Api.LockAll();
            _ = catSkeletonAnimation.AnimationState.SetAnimation(0, catBallAnimation, false);
            float catTimeScale = catSkeletonAnimation.timeScale;
            catSkeletonAnimation.timeScale = catBallAnimationTimeScale;

            TimeSpan delay = TimeSpan.FromSeconds(
                catBallAnimation.Animation.Duration / catBallAnimationTimeScale
            );
            await UniTask.Delay(delay);

            lockerSettings.Api.UnlockAll();
            catSkeletonAnimation.timeScale = catTimeScale;
        }

        private async UniTask PlayKnotAnimation(
            string animationName,
            float moveDistance,
            bool isDelayed
        )
        {
            if (isDelayed)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(ballAnimationDelay));
            }

            OnKnotHinted?.Invoke();
            isKnotMoving = true;
            _ = knotSkeletonAnimation.AnimationState.SetAnimation(0, animationName, true);

            await transform
                .DOMoveX(KnotPosition + moveDistance, knotMoveDuration)
                .SetEase(pushCurve);

            _ = knotSkeletonAnimation.AnimationState.SetEmptyAnimation(0, 0);
            isKnotMoving = false;
            CallWallBounce();
        }
    }
}
