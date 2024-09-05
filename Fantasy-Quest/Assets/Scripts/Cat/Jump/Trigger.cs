using System;
using System.Collections.Generic;
using System.Linq;
using Common.DI;
using Configs;
using Cysharp.Threading.Tasks;
using Interaction;
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
    internal class Trigger : InjectingMonoBehaviour, IJumpUp, IJumpDown
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
        public float JumpDuration => jumpOptions.Duration;

        [Required]
        [SerializeField]
        private Path jumpPathPrefab;

        private Path jumpPath;

        private bool isJumping = false;
        public bool IsJumping => isJumping;
        private List<Action> endJumpsCallbacks = new();
        private List<PreparedJumpZone> preparedJumpZones;

        private void Awake()
        {
            preparedJumpZones = FindObjectsOfType<PreparedJumpZone>(true).ToList();
        }

        private void Start()
        {
            jumpPath = Instantiate(jumpPathPrefab);
        }

        public void AddOneTimeEndJumpCallback(Action callback)
        {
            endJumpsCallbacks.Add(callback);
        }

        public void JumpUp()
        {
            _ = Jump(JumpDirection.Up);
        }

        public void JumpDown()
        {
            _ = Jump(JumpDirection.Down);
        }

        private async UniTaskVoid Jump(JumpDirection jumpDirection)
        {
            if (isJumping)
            {
                return;
            }

            Path.PrepareResult prepareResult;

            Path.PrepareResult resultFromJumpZone = GetPreparedJumpConfig();
            if (resultFromJumpZone.Found)
            {
                prepareResult = resultFromJumpZone;
                prepareResult.CatPosition = catMovement.transform.position;
            }
            else
            {
                prepareResult = jumpPath.DetectJump(
                    jumpDirection,
                    catMovement.transform.position,
                    catMovement.Vector
                );
            }

            if (!prepareResult.Found)
            {
                return;
            }

            jumpPath.PreparePath(prepareResult);

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
            endJumpsCallbacks.ForEach(x => x?.Invoke());
            endJumpsCallbacks.Clear();
        }

        private void SetAnimation()
        {
            _ = catSkeleton.AnimationState.SetEmptyAnimation(0, 0);
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

        public Path.PrepareResult GetPreparedJumpConfig()
        {
            if (catMovement.Rails == null)
            {
                return new Path.PrepareResult() { Found = false };
            }

            foreach (PreparedJumpZone zone in preparedJumpZones)
            {
                Path.PrepareResult result = zone.GetPrepreparedJumpConfig(catMovement.Rails);
                if (result.Found && result.MoveVector == catMovement.Vector)
                {
                    return result;
                }
            }

            return new Path.PrepareResult() { Found = false };
        }
    }
}
