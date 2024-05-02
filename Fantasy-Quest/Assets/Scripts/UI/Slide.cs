using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    [AddComponentMenu("Scripts/UI/UI.Slideshow")]
    internal class Slide : MonoBehaviour
    {
        [Header("Component")]
        [Required]
        [SerializeField]
        private Image slide;

        [Header("Timing")]
        public float WaitBefore = 0.0f;

        public float HoldTime = 1.0f;

        public float WaitAfter = 0.0f;

        public float FadeTime = 0.5f;

        private Tween fadeTween;

        public UnityEvent BeforeFadeIn;
        public UnityEvent AfterFadeIn;
        public UnityEvent AfterFadeOut;

        private void Start()
        {
            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            slide.color = new Color(slide.color.r, slide.color.g, slide.color.b, 0);
            slide = GetComponent<Image>();
        }

        public void FadeIn()
        {
            BeforeFadeIn?.Invoke();
            Fade(1f, FadeTime, () => AfterFadeIn?.Invoke());
        }

        public void FadeOut()
        {
            Fade(
                0f,
                FadeTime,
                () =>
                {
                    gameObject.SetActive(false);
                    AfterFadeOut?.Invoke();
                }
            );
        }

        private void Fade(float endValue, float duration, TweenCallback onEnd)
        {
            fadeTween.Kill(false);

            fadeTween = slide.DOFade(endValue, duration);
            fadeTween.onComplete += onEnd;
        }

        [Button]
        public void GetComponentImage()
        {
            slide = GetComponent<Image>();
        }
    }
}
