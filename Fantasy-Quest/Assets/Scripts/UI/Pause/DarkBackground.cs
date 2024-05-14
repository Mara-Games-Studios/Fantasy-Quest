using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Pause
{
    [AddComponentMenu("Scripts/UI/Pause.sDarkBackground")]
    public class DarkBackground : MonoBehaviour
    {
        [SerializeField]
        private Image image;

        [SerializeField]
        private float invisibleThreshold = 1f;

        [SerializeField]
        private float visibleThreshold = 0.5f;

        [SerializeField]
        private float duration = 1f;

        [SerializeField]
        private Ease ease;

        private Material material;
        private const string THRESHOLD_KEY = "_Threshold";
        private Tween tween;
        private float currentThreshold;

        private void Awake()
        {
            material = image.material;
            currentThreshold = invisibleThreshold;
            image.gameObject.SetActive(false);
            material.SetFloat(THRESHOLD_KEY, currentThreshold);
        }

        [Button]
        public void Show()
        {
            if (material.GetFloat(THRESHOLD_KEY) == visibleThreshold)
            {
                return;
            }

            image.gameObject.SetActive(true);
            tween?.Kill();

            tween = DOTween
                .To(
                    () => currentThreshold,
                    x =>
                    {
                        currentThreshold = x;
                        material.SetFloat(THRESHOLD_KEY, currentThreshold);
                    },
                    visibleThreshold,
                    duration
                )
                .SetEase(ease)
                .SetUpdate(true);
        }

        [Button]
        public void Hide(Action doAfter)
        {
            if (material.GetFloat(THRESHOLD_KEY) == invisibleThreshold)
            {
                return;
            }

            tween?.Kill();

            tween = DOTween
                .To(
                    () => currentThreshold,
                    x =>
                    {
                        currentThreshold = x;
                        material.SetFloat(THRESHOLD_KEY, currentThreshold);
                    },
                    invisibleThreshold,
                    duration
                )
                .SetEase(ease)
                .SetUpdate(true)
                .OnComplete(() =>
                {
                    image.gameObject.SetActive(false);
                    doAfter?.Invoke();
                });
        }
    }
}
