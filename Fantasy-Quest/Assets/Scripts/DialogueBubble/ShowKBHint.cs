using DG.Tweening;
using UnityEngine;

namespace DialogueBubble
{
    [AddComponentMenu("Scripts/DialogueBubble/DialogueBubble.ShowKBHint")]
    internal class ShowKBHint : MonoBehaviour, IShowBubble
    {
        private static int id = 0;

        [Header("Show Settings")]
        [SerializeField]
        private float fadeInDuration = 1f;

        [SerializeField]
        private float fadeOutDuration = 0.3f;

        [SerializeField]
        private SpriteRenderer keyboardSpriteRenderer;

        private void Awake()
        {
            Color bubbleColor = keyboardSpriteRenderer.GetComponent<SpriteRenderer>().color;
            bubbleColor.a = 0f;
            keyboardSpriteRenderer.color = bubbleColor;
        }

        private void OnEnable()
        {
            id += 1;
        }

        public void SwitchShow(BubbleSettings settings)
        {
            if (settings.CanShow)
            {
                gameObject.SetActive(true);
                switch (settings.BubbleType)
                {
                    case Type.OneButton:
                        keyboardSpriteRenderer.sprite = settings.KBicons[0];
                        break;
                    case Type.TwoButtons:
                        keyboardSpriteRenderer.sprite = settings.KBicons[id - 1];
                        break;
                }

                FadeIn(fadeInDuration);
            }
            else
            {
                FadeOut(fadeOutDuration);
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
            Tween fadeTween;
            fadeTween = keyboardSpriteRenderer.DOFade(endValue, duration);

            fadeTween.onComplete += onEnd;
        }

        private void OnDisable()
        {
            id -= 1;
        }
    }
}
