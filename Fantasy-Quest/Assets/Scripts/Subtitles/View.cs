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
            imageFadeTween?.Kill(true);
            outputTmpText.alpha = value ? 1 : 0;
        }

        public void Show(Replica replica)
        {
            DoFade(0);
            fadeTween.onComplete += () =>
            {
                fadeTween?.Kill();
                imageFadeTween?.Kill();
                outputTmpText.SetText(replica.Text);
                DoFade(1);
            };
        }

        public void Hide()
        {
            fadeTween?.Kill(true);
            imageFadeTween?.Kill();
            DoFade(0);
            fadeTween.onComplete += () => outputTmpText.SetText("");
        }

        private void DoFade(float endAlpha)
        {
            if (!SubtitlesSettings.Instance.IsSubtitlesShowing)
            {
                return;
            }

            fadeTween = outputTmpText.DOFade(endAlpha, SubtitlesSettings.Instance.TextFadeDuration);
            imageFadeTween = additionalImageToFade.DOFade(
                endAlpha,
                SubtitlesSettings.Instance.TextFadeDuration
            );
        }
    }
}
