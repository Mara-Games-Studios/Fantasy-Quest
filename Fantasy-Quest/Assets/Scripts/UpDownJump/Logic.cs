using System;
using Cat;
using Configs;
using Cysharp.Threading.Tasks;
using Rails;
using Sirenix.OdinInspector;
using Spine;
using Spine.Unity;
using UnityEngine;

namespace UpDownJump
{
    [Serializable]
    internal struct JumpSection
    {
        public float Duration;
        public Point RailPoint;
        public float MixDuration;
        public AnimationCurve MoveCurve;
        public AnimationReferenceAsset Animation;
    }

    [AddComponentMenu("Scripts/UpDownJump/UpDownJump.Logic")]
    internal class Logic : MonoBehaviour, IUpDownJump
    {
        [Required]
        [SerializeField]
        private SkeletonAnimation catSkeleton;

        [Required]
        [SerializeField]
        private Movement catMovement;

        [SerializeField]
        private JumpSection jumpSection;

        [SerializeField]
        private JumpPath jumpPath;

        [SerializeField]
        private JumpPath debugUpJumpPath;

        [SerializeField]
        private JumpPath debugDownJumpPath;

        private JumpDirection jumpDirection;
        private bool isJumping = false;

        private void Start()
        {
            LockerSettings.Instance.UnlockAll();
        }

        private void Update()
        {
            ShowDebugJumpPaths();
        }

        private void ShowDebugJumpPaths()
        {
            JumpPath.PrepareResult upResult = debugUpJumpPath.PreparePath(
                JumpDirection.Up,
                catMovement.transform.position,
                catMovement.Vector
            );
            if (!upResult.Found)
            {
                debugUpJumpPath.StashPath();
            }

            JumpPath.PrepareResult downResult = debugDownJumpPath.PreparePath(
                JumpDirection.Down,
                catMovement.transform.position,
                catMovement.Vector
            );
            if (!downResult.Found)
            {
                debugDownJumpPath.StashPath();
            }
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

        public void JumpUp()
        {
            if (isJumping)
            {
                return;
            }
            jumpDirection = JumpDirection.Up;
            _ = Jump();
        }

        private async UniTaskVoid Jump()
        {
            JumpPath.PrepareResult prepareResult = jumpPath.PreparePath(
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

            LockerSettings.Instance.LockAll();
            catMovement.RemoveFromRails();
            catMovement.SetOnRails(jumpPath.StartPoint);
            SetAnimation();

            jumpPath.Rails.RideBodyByCurve(
                jumpPath.StartPoint,
                jumpSection.RailPoint,
                jumpSection.MoveCurve,
                jumpSection.Duration
            );
            await UniTask.Delay(TimeSpan.FromSeconds(jumpSection.Duration));

            catMovement.RemoveFromRails();
            catMovement.SetOnRails(
                prepareResult.DestinationRails,
                prepareResult.DestinationRailsTime
            );
            LockerSettings.Instance.UnlockAll();
            catSkeleton.timeScale = previousTimeScale;
            jumpPath.StashPath();
            isJumping = false;
        }

        private void SetAnimation()
        {
            _ = catSkeleton.AnimationState.SetEmptyAnimation(0, 0.1f);
            TrackEntry entry = catSkeleton.AnimationState.AddAnimation(
                0,
                jumpSection.Animation.Animation,
                false,
                0
            );
            entry.TimeScale = jumpSection.Animation.Animation.Duration / jumpSection.Duration;
            entry.MixDuration = jumpSection.MixDuration;
        }
    }
}
