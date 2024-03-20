using Rails;
using Sirenix.OdinInspector;
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
        private Point startPoint;

        private void Start()
        {
            rails.BindBody(transform, startPoint);
        }

        public Coroutine MoveToStartPoint()
        {
            return MoveFromToPoints(rails.CurrentPosition, startPoint.Value);
        }

        public Coroutine MoveToPoint(Vector3 point)
        {
            float endPoint = rails.Path.GetClosestTimeOnPath(point);
            float startPoint = this.startPoint.Value;
            return MoveFromToPoints(startPoint, endPoint);
        }

        private Coroutine MoveFromToPoints(float start, float end)
        {
            float length = rails.GetPathLengthBetweenPoints(start, end);
            return rails.RideBodyByCoroutine(start, end, length / speed);
        }
    }
}
