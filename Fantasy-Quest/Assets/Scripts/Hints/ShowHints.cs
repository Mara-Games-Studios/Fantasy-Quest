using System.Collections;
using System.Collections.Generic;
using Common.DI;
using Configs;
using DG.Tweening;
using Interaction;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace Hints
{
    [AddComponentMenu("Scripts/Hints/Hints.ShowHints")]
    internal class ShowHints : InjectingMonoBehaviour
    {
        [Inject]
        private LockerApi lockerSettings;

        [Header("Show Settings")]
        [SerializeField]
        private bool alwaysShowWithoutTimer = false;

        [ShowIf("@!alwaysShowWithoutTimer")]
        [SerializeField]
        private bool firstShowWithoutTimer = true;

        [SerializeField]
        private float fadeInDuration = 0.6f;

        [SerializeField]
        private float fadeOutDuration = 0.3f;

        [SerializeField]
        private List<SpriteRenderer> keyboardSpriteRenderer;

        [SerializeField]
        private bool useCustomWaitTime = false;

        [SerializeField]
        private float customWaitTime = 1f;

        private Coroutine waitRoutine;
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
            if (
                !lockerSettings.Api.IsDialogueBubbleLocked
                && (firstShowWithoutTimer || alwaysShowWithoutTimer)
            )
            {
                if (collision.TryGetComponent(out InteractionImpl _))
                {
                    FadeIn(fadeInDuration);
                    firstShowWithoutTimer = false;
                }
            }
            else if (!lockerSettings.Api.IsDialogueBubbleLocked && !firstShowWithoutTimer)
            {
                if (collision.TryGetComponent(out InteractionImpl _))
                {
                    waitRoutine = StartCoroutine(
                        WaitUntilShowRoutine(
                            useCustomWaitTime ? customWaitTime : HintConfig.Instance.SecondUntilShow
                        )
                    );
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!lockerSettings.Api.IsDialogueBubbleLocked)
            {
                if (collision.TryGetComponent(out InteractionImpl _))
                {
                    if (waitRoutine != null)
                    {
                        StopCoroutine(waitRoutine);
                    }

                    FadeOut(fadeOutDuration);
                }
            }
        }

        private IEnumerator WaitUntilShowRoutine(float duration)
        {
            yield return new WaitForSeconds(duration);
            FadeIn(fadeInDuration);
        }

        public void FadeIn(float duration)
        {
            fadeTween?.Kill();
            Fade(1f, duration, () => { });
        }

        public void FadeOut(float duration)
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
