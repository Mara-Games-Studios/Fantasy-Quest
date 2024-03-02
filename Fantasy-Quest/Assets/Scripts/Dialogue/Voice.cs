using System;
using System.Diagnostics;
using UnityEngine;

namespace Dialogue
{
    [Serializable]
    public class Voice
    {
        private AudioSource audioSource;

        public Voice(AudioSource audioSource)
        {
            this.audioSource = audioSource;
        }

        public void Say(AudioClip audioClip)
        {
            Silence();
            audioSource.clip = audioClip;
            audioSource.volume = 1f;
            audioSource.Play();
        }

        public void Silence()
        {
            audioSource.Stop();
            audioSource.volume = 0f;
        }

        public void Pause()
        {
            audioSource.Pause();
        }

        public void Resume()
        {
            audioSource.UnPause();
        }
    }
}
