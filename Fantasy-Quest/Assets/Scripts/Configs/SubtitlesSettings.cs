using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace Configs
{
    [CreateAssetMenu(
        fileName = "Subtitles Settings",
        menuName = "Settings/Create Subtitles Settings",
        order = 3
    )]
    internal class SubtitlesSettings : SingletonScriptableObject<SubtitlesSettings>
    {
        [Min(0)]
        [SerializeField]
        private float defaultTimeSpeedForASymbol = 0.5f;
        public float SymbolTimeSpeed => defaultTimeSpeedForASymbol;

        [SerializeField]
        private Ease typingEase = Ease.Linear;
        public Ease TypingEase => typingEase;

        [SerializeField]
        private bool showSubtitles = true;

        public UnityEvent<bool> OnShowSubtitlesChanged;

        public void SetShowSubtitles(bool value)
        {
            showSubtitles = value;
            OnShowSubtitlesChanged?.Invoke(showSubtitles);
        }
    }
}
