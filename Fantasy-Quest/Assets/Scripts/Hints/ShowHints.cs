using System.Collections.Generic;
using Common.DI;
using Configs;
using DG.Tweening;
using Interaction;
using UnityEngine;
using VContainer;

namespace Hints
{
    [AddComponentMenu("Scripts/Hints/Hints.ShowHints")]
    internal class ShowHints : InjectingMonoBehaviour
    {
        [Inject]
        private LockerApi lockerSettings;

        [SerializeField]
        private bool alwaysShowWithoutTimer = false;

        [SerializeField]
        private bool firstShowWithoutTimer = true;

        [SerializeField]
        private float fadeInDuration = 0.6f;

        [SerializeField]
        private float fadeOutDuration = 0.3f;

        [SerializeField]
        private bool useCustomWaitTime = false;

        [SerializeField]
        private float customWaitTime = 1f;

        [SerializeField]
        private List<SpriteRenderer> keyboardSpriteRenderer;

        private Tween waitTween;
        private List<Tween> tweens = new();

        private float WaitTime =>
            useCustomWaitTime ? customWaitTime : HintConfig.Instance.SecondUntilShow;

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
            if (collision.TryGetComponent(out InteractionImpl _))
            {
                waitTween?.Kill();
                if (!lockerSettings.Api.IsDialogueBubbleLocked)
                {
                    if (firstShowWithoutTimer || alwaysShowWithoutTimer)
                    {
                        Fade(1, fadeInDuration);
                        firstShowWithoutTimer = false;
                    }
                    else
                    {
                        waitTween = DOVirtual.DelayedCall(
                            WaitTime,
                            () => Fade(1, fadeInDuration),
                            false
                        );
                    }
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out InteractionImpl _))
            {
                waitTween?.Kill();
                Fade(0, fadeOutDuration);
            }
        }

        private void Fade(float endValue, float duration)
        {
            tweens.ForEach(t => t.Kill());
            tweens.Clear();
            foreach (SpriteRenderer kbRenderer in keyboardSpriteRenderer)
            {
                tweens.Add(kbRenderer.DOFade(endValue, duration));
            }
        }

        public void TurnOffHints()
        {
            keyboardSpriteRenderer.ForEach(x => x.gameObject.SetActive(false));
        }
    }
}
