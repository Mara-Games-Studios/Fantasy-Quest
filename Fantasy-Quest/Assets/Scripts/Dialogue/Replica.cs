using System;
using Configs;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Dialogue
{
    [Serializable]
    public struct Replica
    {
        public string Text;
        public AudioClip Audio;
        public float DelayAfterSaid;

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
