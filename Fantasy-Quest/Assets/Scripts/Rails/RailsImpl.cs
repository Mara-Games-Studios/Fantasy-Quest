using System.Collections;
using Common;
using PathCreation;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Rails
{
    [RequireComponent(typeof(PathCreator))]
    [AddComponentMenu("Scripts/Rails/Rails")]
    internal class RailsImpl : MonoBehaviour
    {
        [ReadOnly]
        [SerializeField]
        private PathCreator pathCreator;
        public VertexPath Path => pathCreator.path;

        [ReadOnly]
        [SerializeField]
        private Transform body;

        [ReadOnly]
        [SerializeField]
        private float currentPosition;

        public float CurrentPosition
        {
            get => currentPosition;
            set => currentPosition = Mathf.Clamp(value, 0.01f, 0.99f);
        }

        private Coroutine rideCoroutine;

        private void OnValidate()
        {
            pathCreator = GetComponent<PathCreator>();
        }

        private void Update()
        {
            if (body != null)
            {
                body.position = Vector3.Lerp(
                    body.position,
                    Path.GetPointAtTime(CurrentPosition),
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
            Gizmos.DrawLine(Path.GetPointAtTime(CurrentPosition), body.position);
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

        public Coroutine RideBodyByCoroutine(float start, float end, float time)
        {
            return StartCoroutine(RideRoutine(start, end, time, AnimationCurve.Linear(0, 0, 1, 1)));
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
                CurrentPosition = Mathf.Lerp(start, end, curve.Evaluate(timer / time));

                timer += Time.deltaTime;
                yield return null;
            }
        }

        [Button(Style = ButtonStyle.Box)]
        public void MoveBody(float length)
        {
            CurrentPosition = MathfTools.Clamp01UpperExclusive(
                CurrentPosition + (length / Path.length)
            );
        }

        [Button(Style = ButtonStyle.Box)]
        public void BindBody(Transform transform, float point)
        {
            body = transform;
            CurrentPosition = point;
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

        public float GetPathLengthBetweenPoints(float start, float end)
        {
            return Mathf.Abs(Path.length * (end - start));
        }
    }
}
