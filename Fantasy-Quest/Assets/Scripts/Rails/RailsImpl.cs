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

        [SerializeField]
        private float magnetSpeed = 0.5f;

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
                    magnetSpeed * Time.deltaTime
                );
            }
        }

        [Title("Debug buttons for testing")]
        [Button(Style = ButtonStyle.Box)]
        public void RideBody(float start, float end, float time)
        {
            _ = this.KillCoroutine(rideCoroutine);
            rideCoroutine = StartCoroutine(RideRoutine(start, end, time));
        }

        [Button(Style = ButtonStyle.Box)]
        public void RideBody(Point start, Point end, float time)
        {
            _ = this.KillCoroutine(rideCoroutine);
            rideCoroutine = StartCoroutine(RideRoutine(start.Value, end.Value, time));
        }

        private IEnumerator RideRoutine(float start, float end, float time)
        {
            float timer = 0;
            while (timer <= time)
            {
                currentPosition = Mathf.Lerp(start, end, timer / time);
                timer += Time.deltaTime;
                yield return null;
            }
        }

        [Button(Style = ButtonStyle.Box)]
        public void MoveBody(float length)
        {
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
            body = transform;
            currentPosition = point.Value;
        }

        [Button(Style = ButtonStyle.Box)]
        public void UnBindBody()
        {
            body = null;
        }
    }
}
