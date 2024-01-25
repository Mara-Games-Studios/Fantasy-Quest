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

        public float GetDuration()
        {
            if (cachedDuration <= 0)
            {
                CalculateDuration();
            }

            return cachedDuration;
        }

        private void CalculateDuration()
        {
            float length = Audio == null ? Text.Length : Audio.length;
            cachedDuration = length * SubtitlesSettings.Instance.SymbolTimeSpeed;
        }
    }
}
