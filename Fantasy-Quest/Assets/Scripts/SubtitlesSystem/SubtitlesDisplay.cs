using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SubtitlesSystem
{
    [RequireComponent(typeof(TextTween))]
    [AddComponentMenu("Scripts/SubtitlesSystem/SubtitlesSystem.SubtitlesDisplay")]
    public class SubtitlesDisplay : MonoBehaviour
    {
        private bool isShowing;
        private ITextTween iTextDisplay;
        private Queue<string> subtitlesToShow;

        private void Awake()
        {
            iTextDisplay = GetComponent<ITextTween>();
            subtitlesToShow = new Queue<string>();
        }

        private void OnEnable()
        {
            iTextDisplay.OnTextHiding += ShowNext;
        }

        private void OnDisable()
        {
            iTextDisplay.OnTextHiding -= ShowNext;
        }

        public void Show(List<string> dialogue)
        {
            foreach (var text in dialogue)
            {
                Show(text);
            }
        }

        private void Show(string text)
        {
            if (isShowing)
            {
                subtitlesToShow.Enqueue(text);
            }
            else
            {
                isShowing = true;
                iTextDisplay.Show(text);
            }
        }

        public void Hide()
        {
            subtitlesToShow.Clear();
            iTextDisplay.Hide();
        }

        private void ShowNext()
        {
            if (subtitlesToShow.Any())
            {
                isShowing = true;
                iTextDisplay.Show(subtitlesToShow.Dequeue());
            }
            else
            {
                isShowing = false;
                iTextDisplay.Hide();
            }
        }
    }
}
