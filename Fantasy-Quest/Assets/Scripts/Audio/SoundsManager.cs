using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Audio
{
    [AddComponentMenu("Scripts/Audio/Audio.SoundsManager")]
    public class SoundsManager : MonoBehaviour
    {
        private struct AudioSourcePoint
        {
            public AudioSource AudioSource;
            public bool IsOnPause;
            public bool IgnorePause;
            public bool PlayingPreviously;

            public void Pause()
            {
                PlayingPreviously = AudioSource.isPlaying;
                AudioSource.Pause();
                IsOnPause = true;
            }

            public void UnPause()
            {
                if (PlayingPreviously)
                {
                    AudioSource.UnPause();
                }
                IsOnPause = false;
            }
        }

        [ReadOnly]
        [SerializeField]
        private List<AudioSourcePoint> audioSources = new();

        [Required]
        [SerializeField]
        private AudioSource audioSourcePrefab;

        public AudioSource CreateSource(string name, bool ignorePause = false)
        {
            AudioSource newAudioSource = Instantiate(audioSourcePrefab, transform);
            newAudioSource.gameObject.name = "Sound Audio source - " + name;
            AudioSourcePoint sourcePoint =
                new()
                {
                    AudioSource = newAudioSource,
                    IsOnPause = false,
                    IgnorePause = ignorePause,
                    PlayingPreviously = true
                };
            audioSources.Add(sourcePoint);
            return newAudioSource;
        }

        [Button]
        public void PauseSound()
        {
            audioSources.ForEach(s => s.Pause());
        }

        [Button]
        public void ResumeSound()
        {
            audioSources.ForEach(s => s.UnPause());
        }
    }
}
