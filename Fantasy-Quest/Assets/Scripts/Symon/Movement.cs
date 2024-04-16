using System.Collections;
using Rails;
using Sirenix.OdinInspector;
using Spine.Unity;
using UnityEngine;

namespace Symon
{
    [SelectionBase]
    [AddComponentMenu("Scripts/Symon/Symon.Movement")]
    internal class Movement : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private RailsImpl rails;

        [SerializeField]
        private float speed;

        [SerializeField]
        private SkeletonAnimation skeletonAnimation;

        [SerializeField]
        private AnimationReferenceAsset idle;

        [SerializeField]
        private AnimationReferenceAsset walk;

        [Required]
        [SerializeField]
        private Point startPoint;

        private void Start()
        {
            rails.BindBody(transform, startPoint);
        }

        public void BindOnRails()
        {
            rails.BindBody(transform, startPoint);
        }

        public void UnbindFromRails()
        {
            rails.UnBindBody();
        }

        public IEnumerator MoveToStartPoint()
        {
            _ = skeletonAnimation.AnimationState.SetAnimation(0, walk, true);
            yield return MoveFromToPoint(rails.CurrentPosition, startPoint.Value);
            _ = skeletonAnimation.AnimationState.SetAnimation(0, idle, true);
        }

        public IEnumerator MoveToPoint(Vector3 point)
        {
            skeletonAnimation.skeleton.ScaleX = -1;
            float endPoint = rails.Path.GetClosestTimeOnPath(point);
            _ = skeletonAnimation.AnimationState.SetAnimation(0, walk, true);
            yield return MoveFromToPoint(startPoint.Value, endPoint);
            skeletonAnimation.skeleton.ScaleX = 1;
            _ = skeletonAnimation.AnimationState.SetAnimation(0, idle, true);
        }

        private IEnumerator MoveFromToPoint(float start, float end)
        {
            float length = rails.GetPathLengthBetweenPoints(start, end);
            yield return rails.RideBodyByCoroutine(start, end, length / speed);
        }
    }
}
