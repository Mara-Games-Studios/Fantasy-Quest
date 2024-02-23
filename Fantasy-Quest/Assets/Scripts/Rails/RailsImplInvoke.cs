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

        private bool InvokeAndByPoints => invokeRideBody && byPoints;
        private bool InvokeAndNotByPoints => invokeRideBody && !byPoints;

        [Range(0f, 1f)]
        [ShowIf(nameof(InvokeAndNotByPoints))]
        [SerializeField]
        private float start = 0f;

        [Range(0f, 1f)]
        [ShowIf(nameof(InvokeAndNotByPoints))]
        [SerializeField]
        private float end = 1f;

        [ShowIf(nameof(InvokeAndByPoints))]
        [SerializeField]
        private Point startPoint;

        [ShowIf(nameof(InvokeAndByPoints))]
        [SerializeField]
        private Point endPoint;

        [ShowIf(nameof(invokeRideBody))]
        [SerializeField]
        private bool withCurve = false;

        private bool InvokeAndWithCurve => invokeRideBody && withCurve;

        [ShowIf(nameof(InvokeAndWithCurve))]
        [SerializeField]
        private AnimationCurve curve;

        [ShowIf(nameof(invokeRideBody))]
        [SerializeField]
        private float totalTime = 1f;

        public void RideBodyInvoke()
        {
            if (!invokeRideBody)
            {
                Debug.LogError(
                    $"Try to invoke {nameof(RideBodyInvoke)} while {nameof(invokeRideBody)} is false.",
                    gameObject
                );
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
                Debug.LogError(
                    $"Try to invoke {nameof(RideBodyByCurveInvoke)} while {nameof(invokeRideBody)} is false.",
                    gameObject
                );
                return;
            }

            if (!withCurve)
            {
                Debug.LogError(
                    $"Try to invoke {nameof(RideBodyByCurveInvoke)} while {nameof(withCurve)} is false.",
                    gameObject
                );
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
