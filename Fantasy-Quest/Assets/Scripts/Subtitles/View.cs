using Configs;
using DG.Tweening;
using Dialogue;
using TMPro;
using UnityEngine;

namespace Subtitles
{
    [AddComponentMenu("Scripts/Subtitles/Subtitles.View")]
    public class View : MonoBehaviour, ISubtitlesView
    {
        [SerializeField]
        private TMP_Text outputTmpText;

        private Tween fadeTween;

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
            outputTmpText.alpha = value ? 1 : 0;
        }

        public void Show(Replica replica)
        {
            fadeTween?.Kill(true);
            outputTmpText.SetText(replica.Text);
            DoFade(1);
        }

        public void Hide()
        {
            fadeTween?.Kill(true);
            DoFade(0);
            fadeTween.onComplete += () => outputTmpText.SetText("");
        }

        private void DoFade(float endAlpha)
        {
            if (!SubtitlesSettings.Instance.ShowSubtitles)
            {
                return;
            }
            fadeTween = DOTween.ToAlpha(
                () => outputTmpText.color,
                (x) => outputTmpText.color = x,
                endAlpha,
                SubtitlesSettings.Instance.TextFadeDuration
            );
        }
    }
}
