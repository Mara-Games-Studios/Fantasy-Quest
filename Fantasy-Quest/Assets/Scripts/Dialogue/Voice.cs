using System;
using DI.Project.Services;
using UnityEngine;

namespace Dialogue
{
    [Serializable]
    public class Voice
    {
        private AudioSource audioSource;

        public Voice(SoundsManager soundsManager, string creatorName)
        {
            audioSource = soundsManager.CreateSource(creatorName);
        }

        public void Say(Replica audioClip)
        {
            Silence();
            audioSource.clip = audioClip.Audio;
            audioClip.AudioSourceConfig.ApplyTo(audioSource);
            audioSource.Play();
        }

        public void Silence()
        {
            audioSource.Stop();
            audioSource.volume = 0f;
        }
    }
}
