using System;
using Audio;
using Configs;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Dialogue
{
    [Serializable]
    public class Replica
    {
        [InfoBox("@" + nameof(InfoDuration))]
        [TextArea]
        public string Text;

        [RequiredIn(PrefabKind.PrefabInstanceAndNonPrefabInstance)]
        public AudioClip Audio;

        public AudioSourceConfig AudioSourceConfig;
        public float DelayBeforeSaid;
        public float DelayAfterSaid;

        private string InfoDuration => $"Voice duration: {Duration}";

        [ReadOnly]
        [SerializeField]
        private float cachedDuration;
        public float Duration
        {
            get
            {
                if (cachedDuration <= 0)
                {
                    cachedDuration = CalculateDuration();
                }
                return cachedDuration;
            }
        }

        private float CalculateDuration()
        {
            if (Audio == null)
            {
                return Text.Length * SubtitlesSettings.Instance.SymbolTimeSpeed;
            }
            return Audio.length;
        }
    }
}
