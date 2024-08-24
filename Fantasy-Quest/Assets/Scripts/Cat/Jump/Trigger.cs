using System;
using Common.DI;
using Configs;
using Cysharp.Threading.Tasks;
using Rails;
using Sirenix.OdinInspector;
using Spine;
using Spine.Unity;
using UnityEngine;
using VContainer;

namespace Cat.Jump
{
    [Serializable]
    internal struct JumpOptions
    {
        public float Duration;
        public float MixDuration;
        public AnimationCurve MoveCurve;
        public AnimationReferenceAsset Animation;
    }

    [AddComponentMenu("Scripts/Cat/Jump/Cat.Jump.Trigger")]
    internal class Trigger : InjectingMonoBehaviour, IJumpTrigger
    {
        [Inject]
        private LockerApi lockerSettings;

        [RequiredIn(PrefabKind.InstanceInScene)]
        [SerializeField]
        private SkeletonAnimation catSkeleton;

        [RequiredIn(PrefabKind.InstanceInScene)]
        [SerializeField]
        private Movement catMovement;

        [SerializeField]
        private JumpOptions jumpOptions;

        [Required]
        [SerializeField]
        private Path jumpPathPrefab;

        private Path jumpPath;

        private JumpDirection jumpDirection;
        private bool isJumping = false;

        private void Start()
        {
            jumpPath = Instantiate(jumpPathPrefab);
        }

        public void JumpUp()
        {
            if (isJumping)
            {
                return;
            }
            jumpDirection = JumpDirection.Up;
            _ = Jump();
        }

        public void JumpDown()
        {
            if (isJumping)
            {
                return;
            }
            jumpDirection = JumpDirection.Down;
            _ = Jump();
        }

        private async UniTaskVoid Jump()
        {
            Path.PrepareResult prepareResult = jumpPath.PreparePath(
                jumpDirection,
                catMovement.transform.position,
                catMovement.Vector
            );
            if (!prepareResult.Found)
            {
                return;
            }
            isJumping = true;
            float previousTimeScale = catSkeleton.timeScale;

            lockerSettings.Api.LockAll(this);
            catMovement.RemoveFromRails();
            catMovement.SetOnRails(jumpPath.Rails, RailsImpl.MIN_TIME);
            SetAnimation();

            jumpPath.Rails.RideBodyByCurve(
                RailsImpl.MIN_TIME,
                RailsImpl.MAX_TIME,
                jumpOptions.MoveCurve,
                jumpOptions.Duration
            );
            await UniTask.Delay(TimeSpan.FromSeconds(jumpOptions.Duration));

            catMovement.RemoveFromRails();
            catMovement.SetOnRails(
                prepareResult.DestinationRails,
                prepareResult.DestinationRailsTime
            );
            lockerSettings.Api.UnlockAll(this);
            catSkeleton.timeScale = previousTimeScale;
            jumpPath.StashPath();
            isJumping = false;
        }

        private void SetAnimation()
        {
            _ = catSkeleton.AnimationState.SetEmptyAnimation(0, 0.1f);
            TrackEntry entry = catSkeleton.AnimationState.AddAnimation(
                0,
                jumpOptions.Animation.Animation,
                false,
                0
            );
            entry.TimeScale = jumpOptions.Animation.Animation.Duration / jumpOptions.Duration;
            entry.MixDuration = jumpOptions.MixDuration;
        }

        private void Update()
        {
            transform.position = catMovement.transform.position;
        }
    }
}
