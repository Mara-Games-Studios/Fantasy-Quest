using System.Collections;
using PathCreation;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Rails
{
    [AddComponentMenu("Scripts/Rails/Rails.InfinityMoverLeftRight")]
    internal class InfinityMoverLeftRight : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private PathCreator pathCreator;

        [ReadOnly]
        [SerializeField]
        private float currentPosition = 0;

        [Required]
        [SerializeField]
        private Transform body;

        [SerializeField]
        private float travelTime = 2f;

        public UnityEvent OnReachedStart;
        public UnityEvent OnReachedEnd;
        public UnityEvent<float> OnPositionChanged;

        public VertexPath Path => pathCreator.path;

        public float CurrentPosition
        {
            get => currentPosition;
            set => currentPosition = Mathf.Clamp(value, 0.001f, 0.999f);
        }

        [SerializeField]
        private float from = 0f;

        [SerializeField]
        private float to = 1f;

        [SerializeField]
        private float startPosition = 0f;

        private bool isToEnd = true;

        private void Start()
        {
            _ = StartCoroutine(
                RideRoutine(startPosition, to, travelTime, AnimationCurve.Linear(0, 0, 1, 1))
            );
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
                OnPositionChanged?.Invoke(CurrentPosition);
            }
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

            (from, to) = (to, from);

            if (isToEnd)
            {
                OnReachedEnd?.Invoke();
            }
            else
            {
                OnReachedStart?.Invoke();
            }
            isToEnd ^= true;

            _ = StartCoroutine(
                RideRoutine(from, to, travelTime, AnimationCurve.Linear(0, 0, 1, 1))
            );
        }
    }
}
