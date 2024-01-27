using DG.Tweening;
using UnityEngine;

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

        [SerializeField]
        private Ease typingEase = Ease.Linear;

        public float SymbolTimeSpeed => defaultTimeSpeedForASymbol;
        public Ease TypingEase => typingEase;
    }
}
