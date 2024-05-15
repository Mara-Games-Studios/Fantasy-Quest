using Dialogue;

namespace Subtitles
{
    public interface ISubtitlesView
    {
        public void Show(Replica replica);
        public void Hide();
        public void UpdateText(string text);
    }
}
