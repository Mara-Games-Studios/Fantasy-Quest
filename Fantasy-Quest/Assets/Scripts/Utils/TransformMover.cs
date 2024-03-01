using System.Collections;
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
        private float time;

        [SerializeField]
        private bool isUsingCurve = false;

        [SerializeField]
        [ShowIf(nameof(isUsingCurve))]
        private AnimationCurve curve;

        public UnityEvent MoveFinished;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Move();
            }
        }

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
            _ = StartCoroutine(MoveRoutine(movingBody, to, time, tempCurve));
        }

        private IEnumerator MoveRoutine(
            Transform body,
            Vector3 to,
            float time,
            AnimationCurve curve
        )
        {
            float timer = time;
            while (timer >= 0)
            {
                body.position = Vector3.Lerp(body.position, to, curve.Evaluate(1 - (timer / time)));
                yield return null;
                timer -= Time.deltaTime;
            }
            MoveFinished?.Invoke();
        }
    }
}
