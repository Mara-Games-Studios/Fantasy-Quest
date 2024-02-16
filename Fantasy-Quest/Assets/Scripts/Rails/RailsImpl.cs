using System.Collections;
using Common;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Rails
{
    [RequireComponent(typeof(BezierCurve))]
    [AddComponentMenu("Scripts/Scripts/Rails/Rails")]
    internal class RailsImpl : MonoBehaviour
    {
        [ReadOnly]
        [SerializeField]
        private BezierCurve curve;
        public BezierCurve Curve => curve;

        [ReadOnly]
        [SerializeField]
        private Transform body;

        [ReadOnly]
        [SerializeField]
        private float currentPosition;

        private Coroutine rideCoroutine;

        private void OnValidate()
        {
            curve = GetComponent<BezierCurve>();
        }

        private void Update()
        {
            if (body != null)
            {
                body.position = Vector3.Lerp(
                    body.position,
                    curve.GetPointAt(currentPosition),
                    Configs.RailsSettings.Instance.MagnetSpeed * Time.deltaTime
                );
            }
        }

        private void OnDrawGizmos()
        {
            if (body == null)
            {
                return;
            }
            Gizmos.DrawLine(curve.GetPointAt(currentPosition), body.position);
        }

        [Title("Debug buttons for testing")]
        [Button(Style = ButtonStyle.Box)]
        public void RideBody(float start, float end, float time)
        {
            _ = this.KillCoroutine(rideCoroutine);
            rideCoroutine = StartCoroutine(
                RideRoutine(start, end, time, AnimationCurve.Linear(0, 0, 1, 1))
            );
        }

        [Button(Style = ButtonStyle.Box)]
        public void RideBody(Point start, Point end, float time)
        {
            RideBody(start.Value, end.Value, time);
        }

        [Button(Style = ButtonStyle.Box)]
        public void RideBodyByCurve(float start, float end, AnimationCurve curve, float time)
        {
            _ = this.KillCoroutine(rideCoroutine);
            rideCoroutine = StartCoroutine(RideRoutine(start, end, time, curve));
        }

        [Button(Style = ButtonStyle.Box)]
        public void RideBodyByCurve(Point start, Point end, AnimationCurve curve, float time)
        {
            RideBodyByCurve(start.Value, end.Value, curve, time);
        }

        private IEnumerator RideRoutine(float start, float end, float time, AnimationCurve curve)
        {
            float timer = 0;
            while (timer <= time)
            {
                currentPosition = Mathf.Lerp(start, end, curve.Evaluate(timer / time));
                timer += Time.deltaTime;
                yield return null;
            }
        }

        [Button(Style = ButtonStyle.Box)]
        public void MoveBody(float length)
        {
            Debug.Log($"Len {curve.length} and {length}, current {currentPosition}");
            currentPosition = Mathf.Clamp01(currentPosition + (length / curve.length));
        }

        [Button(Style = ButtonStyle.Box)]
        public void BindBody(Transform transform, float point)
        {
            body = transform;
            currentPosition = point;
        }

        public void BindBody(Transform transform, Point point)
        {
            BindBody(transform, point.Value);
        }

        [Button(Style = ButtonStyle.Box)]
        public void UnBindBody()
        {
            body = null;
        }
    }
}
