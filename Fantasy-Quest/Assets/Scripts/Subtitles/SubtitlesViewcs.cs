using DG.Tweening;
using TMPro;
using UnityEngine;

namespace SubtitlesSystem
{
    [AddComponentMenu("Scripts/SubtitlesSystem/SubtitlesSystem.SubtitlesView")]
    public class SubtitlesView : MonoBehaviour, ISubtitlesView
    {
        [Header("Default settings")]
        [SerializeField]
        private float defaultTimeSpeedForASymbol = 0.5f;

        [Header("Must have settings")]
        [SerializeField]
        private TMP_Text outputTmpText;

        [SerializeField]
        [Tooltip("You can find eases meaning by googling 'ease types unity', then go to pictures")]
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
            if (typeWriterTween != null)
            {
                typeWriterTween.Kill();
            }
            outputTmpText.SetText("");
        }
    }
}
