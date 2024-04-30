using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Effects.Screen
{
    [AddComponentMenu("Scripts/Effects/Screen/Effects.Screen.Disappearing")]
    internal class CutoutMask : MonoBehaviour, IEffect
    {
        [Serializable]
        private struct PositionAndSize
        {
            public Vector2 Position;
            public Vector2 Size;
        }

        [SerializeField]
        private bool disableOnStart = true;

        [SerializeField]
        private RectTransform maskRectTransform;

        [SerializeField]
        private PositionAndSize startProperties;

        [SerializeField]
        private PositionAndSize endProperties;

        [SerializeField]
        private float duration = 1.0f;

        [SerializeField]
        private AnimationCurve curve = AnimationCurve.Linear(0, 0, 1, 1);

        private RectTransform rectTransform;
        private new UnityEngine.Camera camera;

        public event Action OnEffectEnded;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            camera = FindAnyObjectByType<UnityEngine.Camera>();
        }

        private void Start()
        {
            if (disableOnStart)
            {
                gameObject.SetActive(false);
            }
        }

        private void Update()
        {
            maskRectTransform.localPosition = -transform.localPosition;
        }

        private IEnumerator CutoutRoutine()
        {
            float timer = 0;
            while (timer <= duration)
            {
                timer += Time.deltaTime;
                rectTransform.position = Vector2.Lerp(
                    startProperties.Position,
                    endProperties.Position,
                    curve.Evaluate(timer / duration)
                );
                rectTransform.sizeDelta = Vector2.Lerp(
                    startProperties.Size,
                    endProperties.Size,
                    curve.Evaluate(timer / duration)
                );
                yield return null;
            }
            OnEffectEnded?.Invoke();
        }

        public void SetDestinationPoint(Transform transform)
        {
            startProperties.Position = camera.WorldToScreenPoint(transform.position);
            Debug.Log(startProperties.Position);
            endProperties.Position = startProperties.Position;
        }

        [Button]
        public void DoEffect()
        {
            gameObject.SetActive(true);
            _ = StartCoroutine(CutoutRoutine());
        }

        [Button]
        public void RefreshEffect()
        {
            gameObject.SetActive(false);
        }
    }
}
