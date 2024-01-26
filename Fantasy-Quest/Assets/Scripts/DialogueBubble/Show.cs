using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace DialogueBubble
{
    [RequireComponent(typeof(SpriteRenderer))]
    [AddComponentMenu("Scripts/DialogueBubble/DialogueBubble.Show")]
    internal class Show : MonoBehaviour
    {
        [Header("Show Settings")]
        [SerializeField]
        private float duration = 1f;

        [Header("Sprites/Renderers")]
        [SerializeField]
        private List<Sprite> bubbleSprites = new();

        //[SerializeField]
        //private List<Sprite> iconSprites = new();

        [SerializeField]
        private SpriteRenderer bubbleSpriteRenderer;

        [SerializeField]
        private SpriteRenderer iconSpriteRenderer;

        private Tween fadeTween;

        private void Awake()
        {
            //iconSpriteRenderer.sprite = iconSprites[0];

            Color bubbleColor = bubbleSpriteRenderer.GetComponent<SpriteRenderer>().color;
            Color iconColor = iconSpriteRenderer.GetComponent<SpriteRenderer>().color;

            bubbleColor.a = 0f;
            iconColor.a = 0f;

            bubbleSpriteRenderer.color = bubbleColor;
            iconSpriteRenderer.color = iconColor;
        }

        private void OnEnable()
        {
            EventSystem.OnTriggerBubble += SwitchFade;
        }

        public void SwitchFade(BubbleSettings settings)
        {
            if (settings.CanShow)
            {
                bubbleSpriteRenderer.sprite = settings.IsEmote
                    ? bubbleSprites[0]
                    : bubbleSprites[1];
                iconSpriteRenderer.sprite = settings.Icon;
                gameObject.SetActive(true);
                FadeIn(duration);
            }
            else
            {
                FadeOut(duration);
            }
        }

        private void FadeIn(float duration)
        {
            Fade(1f, duration, () => { });
        }

        private void FadeOut(float duration)
        {
            Fade(0f, duration, () => gameObject.SetActive(false));
        }

        private void Fade(float endValue, float duration, TweenCallback onEnd)
        {
            fadeTween?.Kill(false);

            fadeTween = bubbleSpriteRenderer.DOFade(endValue, duration);
            fadeTween = iconSpriteRenderer.DOFade(endValue, duration);
            fadeTween.onComplete += onEnd;
        }

        private void OnDisable()
        {
            EventSystem.OnTriggerBubble -= SwitchFade;
        }
    }
}
