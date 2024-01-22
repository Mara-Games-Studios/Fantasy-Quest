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
        public List<Sprite> IconList = new();

        [SerializeField]
        private SpriteRenderer bubbleSprite;

        [SerializeField]
        private SpriteRenderer iconSprite;

        private Tween fadeTween;

        private void Awake()
        {
            iconSprite.sprite = IconList[0];

            Color bubbleColor = bubbleSprite.GetComponent<SpriteRenderer>().color;
            Color iconColor = iconSprite.GetComponent<SpriteRenderer>().color;

            bubbleColor.a = 0f;
            iconColor.a = 0f;

            bubbleSprite.color = bubbleColor;
            iconSprite.color = iconColor;
        }

        private void OnEnable()
        {
            EventSystem.OnTriggerBubble += SwitchFade;
        }

        public void SwitchFade(bool canShow)
        {
            if (canShow)
            {
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

            fadeTween = bubbleSprite.DOFade(endValue, duration);
            fadeTween = iconSprite.DOFade(endValue, duration);
            fadeTween.onComplete += onEnd;
        }

        private void OnDisable()
        {
            EventSystem.OnTriggerBubble -= SwitchFade;
        }
    }
}
