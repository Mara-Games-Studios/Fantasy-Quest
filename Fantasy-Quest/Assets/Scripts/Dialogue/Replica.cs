using System;
using Configs;
using UnityEngine;

namespace Dialogue
{
    [Serializable]
    public struct Replica
    {
        private float duration;
        
        public string Text;
        public AudioClip Audio;
        public float DelayAfterSaid;
        public float Duration => GetDuration();

        public float GetDuration()
        {
            if (duration <= 0)
            {
                SetDuration();
            }

            return duration;
        }
        
        private void SetDuration()
        {
            float length = Audio == null ? Text.Length : Audio.length;
            duration = length * SubtitlesSettings.Instance.SymbolTimeSpeed;
        }
    }
}
