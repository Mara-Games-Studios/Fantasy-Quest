using Cysharp.Threading.Tasks;
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

        [Required]
        [SerializeField]
        private SkeletonAnimation skeletonAnimation;

        [Required]
        [SerializeField]
        private AnimationReferenceAsset idle;

        [Required]
        [SerializeField]
        private AnimationReferenceAsset thinking;

        [Required]
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

        public async UniTask MoveToPoint(Vector3 point)
        {
            skeletonAnimation.skeleton.ScaleX = -1;
            float endPoint = rails.Path.GetClosestTimeOnPath(point);
            _ = skeletonAnimation.AnimationState.SetAnimation(0, walk, true);
            await MoveFromToPoint(startPoint.Value, endPoint);
            skeletonAnimation.skeleton.ScaleX = 1;
            _ = skeletonAnimation.AnimationState.SetAnimation(0, idle, true);
        }

        private async UniTask MoveFromToPoint(float start, float end)
        {
            float length = rails.GetPathLengthBetweenPoints(start, end);
            await rails.RideBodyTask(start, end, length / speed);
        }
    }
}
