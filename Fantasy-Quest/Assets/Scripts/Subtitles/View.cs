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

        private Tween typeWriterTween;

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
            outputTmpText.alpha = value ? 1 : 0;
        }

        public void Show(Replica replica)
        {
            string curText = "";
            typeWriterTween = DOTween
                .To(() => curText, x => curText = x, replica.Text, replica.Duration)
                .SetEase(SubtitlesSettings.Instance.TypingEase)
                .OnUpdate(() => outputTmpText.SetText(curText));
        }

        public void Hide()
        {
            typeWriterTween?.Kill();
            outputTmpText.SetText("");
        }
    }
}
