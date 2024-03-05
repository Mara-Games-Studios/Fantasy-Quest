using System.Collections;
using Cat;
using Rails;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Utils
{
    [AddComponentMenu("Scripts/Utils/Utils.TransformMover")]
    internal class TransformMover : MonoBehaviour
    {
        private enum Mode
        {
            Transform,
            Vector3,
            Point
        }

        [Required]
        [SerializeField]
        private Transform movingBody;

        [EnumToggleButtons]
        [SerializeField]
        private Mode mode = Mode.Point;

        [SerializeField]
        [ShowIf(nameof(mode), Value = Mode.Transform)]
        private Transform finalPosition;

        [SerializeField]
        [ShowIf(nameof(mode), Value = Mode.Vector3)]
        private Vector3 finalPositionV3;

        [SerializeField]
        [ShowIf(nameof(mode), Value = Mode.Point)]
        private Point finalPositionPoint;

        [SerializeField]
        private float duration;

        [SerializeField]
        private bool isUsingCurve = false;

        [SerializeField]
        [ShowIf(nameof(isUsingCurve))]
        private AnimationCurve curve;

        public UnityEvent<Vector> MoveStarted;

        public UnityEvent MoveFinished;

        [Button]
        public void Move()
        {
            Vector3 to = mode switch
            {
                Mode.Point => finalPositionPoint.transform.position,
                Mode.Transform => finalPosition.position,
                Mode.Vector3 => finalPositionV3,
                _ => throw new System.ArgumentException()
            };
            AnimationCurve tempCurve = isUsingCurve ? curve : AnimationCurve.Linear(0, 0, 1, 1);

            float direction = to.x - movingBody.position.x;
            if (direction < 0)
            {
                MoveStarted?.Invoke(Vector.Left);
            }
            else
            {
                MoveStarted?.Invoke(Vector.Right);
            }

            _ = StartCoroutine(MoveRoutine(movingBody, to, duration, tempCurve));
        }

        private IEnumerator MoveRoutine(
            Transform body,
            Vector3 to,
            float duration,
            AnimationCurve curve
        )
        {
            Vector3 startPos = body.position;
            float timer = 0;
            while (timer <= duration)
            {
                body.position = Vector3.Lerp(startPos, to, curve.Evaluate(timer / duration));
                yield return null;
                timer += Time.deltaTime;
            }
            MoveFinished?.Invoke();
        }
    }
}
