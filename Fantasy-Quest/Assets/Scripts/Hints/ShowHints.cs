using System.Collections.Generic;
using Configs;
using DG.Tweening;
using Interaction;
using UnityEngine;

namespace Hints
{
    [AddComponentMenu("Scripts/Hints/Hints.ShowHints")]
    internal class ShowHints : MonoBehaviour
    {
        [Header("Show Settings")]
        [SerializeField]
        private float fadeInDuration = 0.6f;

        [SerializeField]
        private float fadeOutDuration = 0.3f;

        [SerializeField]
        private List<SpriteRenderer> keyboardSpriteRenderer;

        private Tween fadeTween;

        private void Awake()
        {
            foreach (SpriteRenderer kbRenderer in keyboardSpriteRenderer)
            {
                Color bubbleColor = kbRenderer.color;
                bubbleColor.a = 0f;
                kbRenderer.color = bubbleColor;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!LockerSettings.Instance.IsDialogueBubbleLocked)
            {
                if (collision.TryGetComponent(out InteractionImpl _))
                {
                    FadeIn(fadeInDuration);
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!LockerSettings.Instance.IsDialogueBubbleLocked)
            {
                if (collision.TryGetComponent(out InteractionImpl _))
                {
                    FadeOut(fadeOutDuration);
                }
            }
        }

        private void FadeIn(float duration)
        {
            fadeTween?.Kill();
            Fade(1f, duration, () => { });
        }

        private void FadeOut(float duration)
        {
            fadeTween?.Kill();
            Fade(0f, duration, () => { });
        }

        private void Fade(float endValue, float duration, TweenCallback onEnd)
        {
            foreach (SpriteRenderer kbRenderer in keyboardSpriteRenderer)
            {
                fadeTween = kbRenderer.DOFade(endValue, duration);
                fadeTween.onComplete += onEnd;
            }
        }
    }
}
