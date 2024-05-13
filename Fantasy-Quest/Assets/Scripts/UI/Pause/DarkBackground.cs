using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Pause
{
    public class DarkBackground : MonoBehaviour
    {
        [SerializeField]
        private Image image;

        [SerializeField]
        private float minThreshold = 1f;

        [SerializeField]
        private float maxThreshold = 0.5f;

        [SerializeField]
        private float duration = 2f;

        [SerializeField]
        private Ease ease;

        private Material material;
        private const string THRESHOLD_KEY = "_Threshold";
        private Tween tween;
        private float currentThreshold;

        private void Awake()
        {
            material = image.material;
            currentThreshold = minThreshold;
            material.SetFloat(THRESHOLD_KEY, currentThreshold);
        }

        [Button]
        public void Show()
        {
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
                    maxThreshold,
                    duration
                )
                .SetEase(ease)
                .SetUpdate(true);
        }

        [Button]
        public void Hide()
        {
            tween?.Kill();

            tween = DOTween
                .To(
                    () => currentThreshold,
                    x =>
                    {
                        currentThreshold = x;
                        material.SetFloat(THRESHOLD_KEY, currentThreshold);
                    },
                    minThreshold,
                    duration
                )
                .SetEase(ease)
                .SetUpdate(true)
                .OnComplete(() => image.gameObject.SetActive(false));
        }
    }
}
