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
        public const float MIN_TIME = 0.0001f;
        public const float MAX_TIME = 0.9999f;

        [ReadOnly]
        [SerializeField]
        private PathCreator pathCreator;
        public VertexPath Path => pathCreator.path;
        public BezierPath BezierPath => pathCreator.bezierPath;
        public PathCreator PathCreator => pathCreator;

        [ReadOnly]
        [SerializeField]
        private Transform body;

        [ReadOnly]
        [SerializeField]
        private float currentPosition;

        public float CurrentPosition
        {
            get => currentPosition;
            set => currentPosition = Mathf.Clamp(value, MIN_TIME, MAX_TIME);
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

        public Vector2 GetClosestPointOnPath(Vector2 point)
        {
            float time = Path.GetClosestTimeOnPath(point);
            return Path.GetPointAtTime(Mathf.Clamp(time, MIN_TIME, MAX_TIME));
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
            float clamped = Mathf.Clamp01(CurrentPosition + (length / Path.length));
            if (clamped == 1)
            {
                clamped = 0.99999f;
            }
            CurrentPosition = clamped;
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
