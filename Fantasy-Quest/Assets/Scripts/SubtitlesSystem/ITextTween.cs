using System;

namespace SubtitlesSystem
{
    public interface ITextTween
    {
        public event Action OnTextHiding;
        public void Show(string text);
        public void Hide();
    }
}
