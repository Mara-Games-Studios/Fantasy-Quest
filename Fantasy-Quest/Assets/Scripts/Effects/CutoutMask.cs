using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Effects.Screen
{
    [ExecuteAlways]
    [AddComponentMenu("Scripts/Effects/Screen/Effects.Screen.Disappearing")]
    internal class CutoutMask : MonoBehaviour
    {
        [Serializable]
        private struct PositionAndSize
        {
            public Vector2 Position;
            public Vector2 Size;
        }

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

        public UnityEvent OnCutoutEnds;

        private RectTransform rectTransform;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        private void Update()
        {
            maskRectTransform.localPosition = -transform.localPosition;
        }

        [Button]
        public void DoCutout()
        {
            _ = StartCoroutine(CutoutRoutine());
        }

        private IEnumerator CutoutRoutine()
        {
            float timer = 0;
            while (timer <= duration)
            {
                timer += Time.deltaTime;
                rectTransform.localPosition = Vector2.Lerp(
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
            OnCutoutEnds?.Invoke();
        }
    }
}
