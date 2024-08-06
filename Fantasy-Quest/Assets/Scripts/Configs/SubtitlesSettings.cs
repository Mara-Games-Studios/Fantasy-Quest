using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(
        fileName = "Subtitles Settings",
        menuName = "Settings/Create Subtitles Settings"
    )]
    internal class SubtitlesSettings : SingletonScriptableObject<SubtitlesSettings>
    {
        [MinValue(0)]
        [SerializeField]
        private float timeSpeedForASymbol = 0.5f;
        public float SymbolTimeSpeed => timeSpeedForASymbol;

        [SerializeField]
        private bool isSubtitlesShowing = true;
        public bool IsSubtitlesShowing => isSubtitlesShowing;

        public event Action<bool> OnShowSubtitlesChanged;

        [MinValue(0)]
        [SerializeField]
        private float textFadeDuration = 0.2f;
        public float TextFadeDuration => textFadeDuration;

        [MinValue(0)]
        [SerializeField]
        private float textFadeInSlideshowDuration = 1f;
        public float TextFadeInSlideshowDuration => textFadeInSlideshowDuration;

        [MinValue(0)]
        [SerializeField]
        private float fadeHintDuration = 1f;
        public float FadeHintDuration => fadeHintDuration;

        [MinValue(0)]
        [SerializeField]
        private float subtitlesHintShowDuration = 2f;
        public float SubtitlesHintShowDuration => subtitlesHintShowDuration;

        public void SetShowSubtitles(bool value)
        {
            isSubtitlesShowing = value;
            OnShowSubtitlesChanged?.Invoke(isSubtitlesShowing);
        }
    }
}
