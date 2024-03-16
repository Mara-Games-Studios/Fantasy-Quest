using Sirenix.OdinInspector;
using UnityEngine;

namespace Rails
{
    [AddComponentMenu("Scripts/Rails/Rails.RailsImplInvoke")]
    internal class RailsImplInvoke : MonoBehaviour
    {
        [RequiredIn(PrefabKind.PrefabInstanceAndNonPrefabInstance)]
        [SerializeField]
        private RailsImpl railsImpl;

        [SerializeField]
        private bool byPoints;

        [Range(0f, 1f)]
        [HideIf(nameof(byPoints))]
        [SerializeField]
        private float start = 0f;

        [Range(0f, 1f)]
        [HideIf(nameof(byPoints))]
        [SerializeField]
        private float end = 1f;

        [ShowIf(nameof(byPoints))]
        [SerializeField]
        private Point startPoint;

        [ShowIf(nameof(byPoints))]
        [SerializeField]
        private Point endPoint;

        [SerializeField]
        private bool withCurve = false;

        [ShowIf(nameof(withCurve))]
        [SerializeField]
        private AnimationCurve curve;

        [SerializeField]
        private float totalTime = 1f;

        public void RideBodyInvoke()
        {
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

        public float GetDuration()
        {
            return totalTime;
        }
    }
}
