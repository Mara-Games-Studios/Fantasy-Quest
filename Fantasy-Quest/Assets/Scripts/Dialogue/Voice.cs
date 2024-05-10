using System;
using Audio;
using UnityEngine;

namespace Dialogue
{
    [Serializable]
    public class Voice
    {
        private AudioSource audioSource;

        public Voice(SoundsManager soundsManager)
        {
            audioSource = soundsManager.CreateSource();
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
