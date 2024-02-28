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
        private bool showSubtitles = true;
        public bool ShowSubtitles => showSubtitles;

        public event Action<bool> OnShowSubtitlesChanged;

        [MinValue(0)]
        [SerializeField]
        private float textFadeDuration = 0.2f;
        public float TextFadeDuration => textFadeDuration;

        public void SetShowSubtitles(bool value)
        {
            showSubtitles = value;
            OnShowSubtitlesChanged?.Invoke(showSubtitles);
        }
    }
}
