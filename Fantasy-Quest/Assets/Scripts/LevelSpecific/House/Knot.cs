using System;
using Common.DI;
using Configs;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Interaction;
using Sirenix.OdinInspector;
using Spine.Unity;
using UnityEngine;
using UnityEngine.Events;
using VContainer;

namespace LevelSpecific.House
{
    [RequireComponent(typeof(Animator))]
    [AddComponentMenu("Scripts/LevelSpecific/House/LevelSpecific.House.Knot")]
    internal class Knot : InjectingMonoBehaviour, IInteractable
    {
        [Inject]
        private LockerApi lockerSettings;

        [SerializeField]
        [Required]
        private Cat.Movement catMovement;

        [SerializeField]
        [Required]
        private Animator animator;

        [SerializeField]
        private int animationLayer;

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

        private bool isKnotMoving = false;

        private float catTimeScale;
        private float knotPosition;

        public UnityEvent OnKnotHinted;

        public void Interact()
        {
            knotPosition = gameObject.transform.position.x;
            if (!isKnotMoving) // && CanPushForward())
            {
                float catPosition = catMovement.gameObject.transform.position.x;
                if (catMovement.Vector == Cat.Vector.Right && knotPosition - catPosition > 0)
                {
                    _ = PlayCatAnimation();
                    _ = PlayKnotAnimation("MoveRight", knotMoveDistance, pushCurve);
                }
                else if (catMovement.Vector == Cat.Vector.Left && knotPosition - catPosition < 0)
                {
                    _ = PlayCatAnimation();
                    _ = PlayKnotAnimation("MoveLeft", -knotMoveDistance, pushCurve);
                }
            }
        }

        private void CallWallBounce()
        {
            knotPosition = gameObject.transform.position.x;
            if (knotPosition < knotStartPosition)
            {
                _ = PlayKnotAnimation("MoveRight", knotMoveDistance, bounceCurve);
            }
            else if (knotPosition > knotEndPosition)
            {
                _ = PlayKnotAnimation("MoveLeft", -knotMoveDistance, bounceCurve);
            }
        }

        private async UniTask PlayCatAnimation()
        {
            lockerSettings.Api.LockAll();
            _ = catSkeletonAnimation.AnimationState.SetAnimation(0, catBallAnimation, false);
            catTimeScale = catSkeletonAnimation.timeScale;
            catSkeletonAnimation.timeScale = catBallAnimationTimeScale;

            await UniTask.Delay(
                TimeSpan.FromSeconds(
                    catBallAnimation.Animation.Duration / catBallAnimationTimeScale
                )
            );

            lockerSettings.Api.UnlockAll();
            catSkeletonAnimation.timeScale = catTimeScale;

            return;
        }

        private async UniTask PlayKnotAnimation(
            string animationName,
            float moveDistance,
            AnimationCurve curve
        )
        {
            OnKnotHinted?.Invoke();
            isKnotMoving = true;
            knotSkeletonAnimation.enabled = true;
            knotSkeletonAnimation.AnimationName = animationName;
            _ = transform.DOMoveX(knotPosition + moveDistance, knotMoveDuration).SetEase(curve);

            await UniTask.Delay(TimeSpan.FromSeconds(knotMoveDuration));

            knotSkeletonAnimation.AnimationName = null;
            isKnotMoving = false;
            CallWallBounce();

            return;
        }
    }
}
