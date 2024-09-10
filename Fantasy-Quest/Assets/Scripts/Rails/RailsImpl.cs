using System.Threading;
using Cysharp.Threading.Tasks;
using PathCreation;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Rails
{
    [RequireComponent(typeof(PathCreator))]
    [AddComponentMenu("Scripts/Rails/Rails")]
    internal class RailsImpl : MonoBehaviour
    {
        public const float MIN_TIME = 0.001f;
        public const float MAX_TIME = 0.999f;

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

        private CancellationTokenSource tokenSource = new();

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

        public float GetClosestTimeOnPath(Vector2 point)
        {
            float time = Path.GetClosestTimeOnPath(point);
            return Mathf.Clamp(time, MIN_TIME, MAX_TIME);
        }

        [Button(Style = ButtonStyle.Box)]
        public void RideBody(float start, float end, float time)
        {
            tokenSource.Cancel();
            tokenSource = new();
            _ = RideTask(start, end, time, AnimationCurve.Linear(0, 0, 1, 1));
        }

        public UniTask RideBodyTask(float start, float end, float time)
        {
            return RideTask(start, end, time, AnimationCurve.Linear(0, 0, 1, 1));
        }

        [Button(Style = ButtonStyle.Box)]
        public void RideBody(Point start, Point end, float time)
        {
            RideBody(start.Value, end.Value, time);
        }

        [Button(Style = ButtonStyle.Box)]
        public void RideBodyByCurve(float start, float end, AnimationCurve curve, float time)
        {
            tokenSource.Cancel();
            tokenSource = new();
            _ = RideTask(start, end, time, curve);
        }

        [Button(Style = ButtonStyle.Box)]
        public void RideBodyByCurve(Point start, Point end, AnimationCurve curve, float time)
        {
            RideBodyByCurve(start.Value, end.Value, curve, time);
        }

        private async UniTask RideTask(float start, float end, float time, AnimationCurve curve)
        {
            float timer = 0;
            while (timer < time)
            {
                CurrentPosition = Mathf.Lerp(start, end, curve.Evaluate(timer / time));
                timer += Time.deltaTime;
                await UniTask.Yield(PlayerLoopTiming.Update, tokenSource.Token);
            }
        }

        [Button(Style = ButtonStyle.Box)]
        public void MoveBody(float length)
        {
            CurrentPosition += length / Path.length;
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

        private void OnDestroy()
        {
            tokenSource.Cancel();
            tokenSource.Dispose();
        }
    }
}
