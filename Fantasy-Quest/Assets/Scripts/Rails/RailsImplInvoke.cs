using Sirenix.OdinInspector;
using UnityEngine;

namespace Rails
{
    [AddComponentMenu("Scripts/Rails/Rails.RailsImplInvoke")]
    internal class RailsImplInvoke : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private RailsImpl railsImpl;

        [SerializeField]
        private bool invokeRideBody = false;

        [ShowIf(nameof(invokeRideBody))]
        [SerializeField]
        private bool byPoints;

        [Range(0f, 1f)]
        [HideIf(nameof(byPoints))]
        [ShowIf(nameof(invokeRideBody))]
        [SerializeField]
        private float start = 0f;

        [Range(0f, 1f)]
        [HideIf(nameof(byPoints))]
        [ShowIf(nameof(invokeRideBody))]
        [SerializeField]
        private float end = 1f;

        [ShowIf(nameof(byPoints))]
        [ShowIf(nameof(invokeRideBody))]
        [SerializeField]
        private Point startPoint;

        [ShowIf(nameof(byPoints))]
        [ShowIf(nameof(invokeRideBody))]
        [SerializeField]
        private Point endPoint;

        [ShowIf(nameof(invokeRideBody))]
        [SerializeField]
        private bool withCurve = false;

        [ShowIf(nameof(invokeRideBody))]
        [ShowIf(nameof(withCurve))]
        private AnimationCurve curve;

        [ShowIf(nameof(invokeRideBody))]
        [SerializeField]
        private float totalTime = 1f;

        public void RideBodyInvoke()
        {
            if (!invokeRideBody)
            {
                return;
            }

            if (byPoints)
            {
                railsImpl.RideBody(start, end, totalTime);
            }
            else
            {
                railsImpl.RideBody(startPoint, endPoint, totalTime);
            }
        }

        public void RideBodyByCurveInvoke()
        {
            if (!invokeRideBody)
            {
                return;
            }

            if (!withCurve)
            {
                return;
            }

            if (byPoints)
            {
                railsImpl.RideBodyByCurve(start, end, curve, totalTime);
            }
            else
            {
                railsImpl.RideBodyByCurve(startPoint, endPoint, curve, totalTime);
            }
        }
    }
}
