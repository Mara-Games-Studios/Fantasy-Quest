using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Subtitles
{
    [AddComponentMenu("Scripts/Subtitles/Subtitles.SubtitlesView")]
    public class SubtitlesView : MonoBehaviour, ISubtitlesView
    {
        [SerializeField]
        private float defaultTimeSpeedForASymbol = 0.5f;
        [SerializeField]
        private TMP_Text outputTmpText;
        [SerializeField]
        private Ease typingEase;

        private Tween typeWriterTween;

        public void Show(
            string text,
            float timeToShow = float.MinValue,
            float delayAfterSaid = float.MinValue
        )
        {
            string curText = "";
            if (timeToShow.Equals(float.MinValue))
            {
                timeToShow = defaultTimeSpeedForASymbol * text.Length;
            }

            typeWriterTween = DOTween
                .To(() => curText, x => curText = x, text, timeToShow)
                .SetEase(typingEase)
                .OnUpdate(() => outputTmpText.SetText(curText));
        }

        public void Hide()
        {
            typeWriterTween?.Kill();
            outputTmpText.SetText("");
        }
    }
}
