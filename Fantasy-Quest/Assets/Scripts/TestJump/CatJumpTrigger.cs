using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cat;
using Configs;
using Rails;
using Sirenix.OdinInspector;
using Spine;
using Spine.Unity;
using UnityEngine;

namespace TestJump
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

    [AddComponentMenu("Scripts/TestJump/TestJump.CatJumpTrigger")]
    internal class CatJumpTrigger : MonoBehaviour, ISimpleJumpTrigger
    {
        [Required]
        [SerializeField]
        private SkeletonAnimation catSkeleton;

        [SerializeField]
        private bool snapEndPointOfJumpPath;

        [Required]
        [SerializeField]
        private Movement catMovement;

        [SerializeField]
        private List<JumpSection> jumpSections;

        [Required]
        [SerializeField]
        private Point startPoint;

        private bool isJumping = false;

        public void Jump()
        {
            _ = StartCoroutine(JumpRoutine());
        }

        private void Start()
        {
            LockerSettings.Instance.UnlockAll();
        }

        private void Update()
        {
            if (isJumping)
            {
                return;
            }

            startPoint.Rails.transform.SetPositionAndRotation(
                catMovement.transform.position,
                catMovement.Vector is Vector.Right
                    ? Quaternion.identity
                    : Quaternion.Euler(new Vector3(0, 180, 0))
            );
        }

        private IEnumerator JumpRoutine()
        {
            isJumping = true;
            (float time, RailsImpl rail) = FindEndRails();

            if (snapEndPointOfJumpPath)
            {
                Vector3 point = rail.Path.GetPointAtTime(time);
                point = startPoint.Rails.transform.InverseTransformPoint(point);
                startPoint.Rails.BezierPath.SetPoint(
                    startPoint.Rails.BezierPath.NumPoints - 1,
                    point
                );
                startPoint.Rails.PathCreator.TriggerPathUpdate();
            }

            float previousTimeScale = catSkeleton.timeScale;

            LockerSettings.Instance.LockAll();
            catMovement.RemoveFromRails();
            catMovement.SetOnRails(startPoint);
            Point lastPoint = startPoint;
            SetAnimations();
            foreach (JumpSection section in jumpSections)
            {
                startPoint.Rails.RideBodyByCurve(
                    lastPoint,
                    section.RailPoint,
                    section.MoveCurve,
                    section.Duration
                );
                yield return new WaitForSeconds(section.Duration);
                lastPoint = section.RailPoint;
            }

            catMovement.RemoveFromRails();
            catMovement.SetOnRails(rail, time);
            LockerSettings.Instance.UnlockAll();
            catSkeleton.timeScale = previousTimeScale;
            isJumping = false;
        }

        private (float time, RailsImpl rail) FindEndRails()
        {
            Vector3 targetPosition = startPoint.Rails.Path.GetPointAtTime(0.999f);

            List<RailsImpl> rails = FindObjectsOfType<RailsImpl>()
                .Where(x => x != startPoint.Rails)
                .ToList();
            (Vector3 point, RailsImpl rail) result = (
                rails.First().Path.GetClosestPointOnPath(targetPosition),
                rails.First()
            );
            rails.RemoveAt(0);
            foreach (RailsImpl rail in rails)
            {
                Vector3 newPoint = rail.Path.GetClosestPointOnPath(targetPosition);
                if (
                    Vector3.Distance(result.point, targetPosition)
                    > Vector3.Distance(newPoint, targetPosition)
                )
                {
                    result.point = newPoint;
                    result.rail = rail;
                }
            }
            return (result.rail.Path.GetClosestTimeOnPath(result.point), result.rail);
        }

        private void SetAnimations()
        {
            _ = catSkeleton.AnimationState.SetEmptyAnimation(0, 0.1f);
            foreach (JumpSection section in jumpSections)
            {
                TrackEntry entry = catSkeleton.AnimationState.AddAnimation(
                    0,
                    section.Animation.Animation,
                    false,
                    0
                );
                entry.TimeScale = section.Animation.Animation.Duration / section.Duration;
                entry.MixDuration = section.MixDuration;
            }
        }
    }
}
