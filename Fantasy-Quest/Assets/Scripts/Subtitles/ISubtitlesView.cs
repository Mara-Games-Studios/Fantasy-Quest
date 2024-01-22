namespace SubtitlesSystem
{
    public interface ISubtitlesView
    {
        public void Show(string text, float timeToShow, float delayAfterSaid);
        public void Hide();
    }
}
