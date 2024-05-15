using Configs;
using DG.Tweening;
using Dialogue;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Subtitles
{
    [AddComponentMenu("Scripts/Subtitles/Subtitles.View")]
    public class View : MonoBehaviour, ISubtitlesView
    {
        [Required]
        [SerializeField]
        private TMP_Text outputTmpText;

        [SerializeField]
        private Image additionalImageToFade;

        [SerializeField]
        private bool useImage = false;

        [SerializeField]
        private bool useSlideshowDelay = false;

        private Tween fadeTween;
        private Tween imageFadeTween;

        private void OnEnable()
        {
            SubtitlesSettings.Instance.OnShowSubtitlesChanged += OnSubtitlesShowChanged;
        }

        private void OnDisable()
        {
            SubtitlesSettings.Instance.OnShowSubtitlesChanged -= OnSubtitlesShowChanged;
        }

        private void OnSubtitlesShowChanged(bool value)
        {
            fadeTween?.Kill(true);
            if (useImage)
            {
                imageFadeTween?.Kill(true);
            }

            int alpha = value ? 1 : 0;
            outputTmpText.alpha = alpha;

            if (useImage)
            {
                Color color = additionalImageToFade.color;
                additionalImageToFade.color = new Color(color.r, color.g, color.b, alpha);
            }
        }

        public void Show(Replica replica)
        {
            DoFade(0);

            if (!SubtitlesSettings.Instance.IsSubtitlesShowing)
            {
                return;
            }

            fadeTween.onComplete += () =>
            {
                fadeTween?.Kill();
                if (useImage)
                {
                    imageFadeTween?.Kill();
                }
                outputTmpText.SetText(replica.Text);
                DoFade(1);
            };
        }

        public void Hide()
        {
            if (!SubtitlesSettings.Instance.IsSubtitlesShowing)
            {
                return;
            }

            fadeTween?.Kill(true);
            if (useImage)
            {
                imageFadeTween?.Kill();
            }
            DoFade(0);
            fadeTween.onComplete += () => outputTmpText.SetText("");
        }

        private void DoFade(float endAlpha)
        {
            if (!SubtitlesSettings.Instance.IsSubtitlesShowing)
            {
                return;
            }

            float duration = useSlideshowDelay
                ? SubtitlesSettings.Instance.TextFadeInSlideshowDuration
                : SubtitlesSettings.Instance.TextFadeDuration;

            fadeTween = outputTmpText.DOFade(endAlpha, duration);
            if (useImage)
            {
                imageFadeTween = additionalImageToFade.DOFade(endAlpha, duration);
            }
        }

        public void UpdateText(string text)
        {
            if (!SubtitlesSettings.Instance.IsSubtitlesShowing)
            {
                return;
            }

            if (outputTmpText.text != "")
            {
                outputTmpText.SetText(text);
            }
        }
    }
}
