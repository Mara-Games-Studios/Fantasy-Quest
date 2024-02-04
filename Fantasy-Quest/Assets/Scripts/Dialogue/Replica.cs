using System;
using Configs;
using UnityEngine;

namespace Dialogue
{
    [Serializable]
    public struct Replica
    {
        private float cachedDuration;

        public string Text;
        public AudioClip Audio;
        public float DelayAfterSaid;
        public float Duration => GetDuration();

        private float GetDuration()
        {
            if (cachedDuration <= 0)
            {
                CalculateDuration();
            }

            return cachedDuration;
        }

        private void CalculateDuration()
        {
            cachedDuration = Audio == null ? Text.Length * SubtitlesSettings.Instance.SymbolTimeSpeed : Audio.length;
        }
    }
}
