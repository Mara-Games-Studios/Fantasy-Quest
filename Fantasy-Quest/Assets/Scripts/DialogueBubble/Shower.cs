using System.Collections.Generic;
using DG.Tweening;
using DI.Project.Services;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DialogueBubble
{
    [RequireComponent(typeof(SpriteRenderer))]
    [AddComponentMenu("Scripts/DialogueBubble/DialogueBubble.Shower")]
    internal class Shower : MonoBehaviour
    {
        [SerializeField]
        private bool initialHidden = true;

        [SerializeField]
        private float fadeInDuration = 0.6f;

        [SerializeField]
        private float fadeOutDuration = 0.3f;

        [InfoBox("First for Dialogue, Second for Thought")]
        [SerializeField]
        private List<Sprite> bubbleSprites = new();

        [SerializeField]
        private SpriteRenderer bubbleSpriteRenderer;

        [SerializeField]
        private SpriteRenderer iconSpriteRenderer;

        private Tween fadeTween;

        private void Awake()
        {
            if (initialHidden)
            {
                Color bubbleColor = bubbleSpriteRenderer.color;
                Color iconColor = iconSpriteRenderer.color;

                bubbleColor.a = 0f;
                iconColor.a = 0f;

                bubbleSpriteRenderer.color = bubbleColor;
                iconSpriteRenderer.color = iconColor;
            }
        }

        public void ShowBubble(BubbleConfig settings)
        {
            if (settings.CanShow)
            {
                switch (settings.BubbleType)
                {
                    case Type.Dialogue:
                        bubbleSpriteRenderer.sprite = bubbleSprites[0];
                        break;
                    case Type.Thought:
                        bubbleSpriteRenderer.sprite = bubbleSprites[1];
                        break;
                }
                iconSpriteRenderer.sprite = settings.EmoteIcons[0];
                gameObject.SetActive(true);
                FadeIn(fadeInDuration);
            }
            else
            {
                FadeOut(fadeOutDuration);
            }
        }

        public void FadeIn(float duration)
        {
            Fade(1f, duration, () => { });
        }

        public void FadeOut(float duration)
        {
            Fade(0f, duration, () => gameObject.SetActive(false));
        }

        private void Fade(float endValue, float duration, TweenCallback onEnd)
        {
            fadeTween.Kill(false);

            fadeTween = bubbleSpriteRenderer.DOFade(endValue, duration);
            fadeTween = iconSpriteRenderer.DOFade(endValue, duration);
            fadeTween.onComplete += onEnd;
        }
    }
}
