using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace SubtitlesSystem
{
    [RequireComponent(typeof(SubtitlesDisplay))]
    [AddComponentMenu("Scripts/SubtitlesSystem/SubtitlesSystem.TextTween")]
    public class TextTween : MonoBehaviour, ITextTween
    {
        [SerializeField]
        private TMP_Text outputTmpText;

        [SerializeField]
        private float timeSpeedForCharacter = 0.5f;

        [SerializeField]
        private float timeDelayBeforeVanish = 2f;

        [SerializeField]
        [Tooltip("You can find eases meaning by googling 'ease types unity', then go to pictures")]
        private Ease typingEase;

        private Tween typeWriterTween;
        private Coroutine waitCoroutine;
        public event Action OnTextHiding;

        public void Show(string text)
        {
            Hide();

            string curText = "";
            var time = timeSpeedForCharacter * text.Length;
            typeWriterTween = DOTween
                .To(() => curText, x => curText = x, text, time)
                .SetEase(typingEase)
                .OnUpdate(() => outputTmpText.SetText(curText))
                .OnComplete(() => waitCoroutine = StartCoroutine(Wait()));
        }

        public void Hide()
        {
            if (waitCoroutine != null)
            {
                StopCoroutine(waitCoroutine);
                waitCoroutine = null;
            }

            if (typeWriterTween != null)
            {
                typeWriterTween.Kill();
            }
            outputTmpText.SetText("");
        }

        private IEnumerator Wait()
        {
            yield return new WaitForSeconds(timeDelayBeforeVanish);
            OnTextHiding?.Invoke();
        }
    }
}
