using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DI.Project.Services
{
    [AddComponentMenu("Scripts/DI/Project/Services/DI.Project.Services.SoundsManager")]
    public class SoundsManager : MonoBehaviour
    {
        [Serializable]
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

        [Required]
        [SerializeField]
        private AudioSource audioSourcePrefab;

        private List<AudioSourcePoint> audioSources = new();

        public void Awake()
        {
            SceneManager.activeSceneChanged += SceneManagerActiveSceneChanged;
        }

        private void SceneManagerActiveSceneChanged(
            UnityEngine.SceneManagement.Scene oldScene,
            UnityEngine.SceneManagement.Scene newScene
        )
        {
            audioSources.ForEach(x => Destroy(x.AudioSource.gameObject));
            audioSources.Clear();
        }

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
