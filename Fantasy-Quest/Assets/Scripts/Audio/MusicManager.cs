﻿using System.Collections.Generic;
using System.Linq;
using Configs.Music;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Audio
{
    [AddComponentMenu("Scripts/Audio/Audio.MusicManager")]
    internal class MusicManager : MonoBehaviour
    {
        [AssetList]
        [SerializeField]
        private PlaylistConfig startPlaylist;

        [ReadOnly]
        [SerializeField]
        private List<AudioClipConfig> currentClips;

        [ReadOnly]
        [SerializeField]
        private List<AudioSource> audioSources = new();

        [SerializeField]
        private bool playOnStart = true;

        private int maxAudioSources = 2;

        [Required]
        [SerializeField]
        private AudioSource audioSourcePrefab;

        private void Awake()
        {
            for (int i = 0; i < maxAudioSources; i++)
            {
                audioSources.Add(Instantiate(audioSourcePrefab, transform));
            }
        }

        private void Start()
        {
            Configs.AudioSettings.Instance.RefreshAudio();
            SwitchPlaylist(startPlaylist, playOnStart);
        }

        [Button]
        public void SwitchPlaylist(PlaylistConfig playlist, bool playImmediately)
        {
            currentClips = playlist.AudioClips.ToList();
            if (!currentClips.Any())
            {
                Debug.LogError("There is no clips in given playlist", playlist);
                return;
            }
            if (playImmediately)
            {
                PlayPlaylist(playlist.Loop);
            }
        }

        [Button]
        private void PlayPlaylist(bool loop = false)
        {
            for (int i = 0; i < currentClips.Count; i++)
            {
                audioSources[i].clip = currentClips[i].AudioClip;
                currentClips[i].AudioSourceConfig.ApplyTo(audioSources[i]);
                audioSources[i].loop = loop;
                audioSources[i].Play();
            }
        }

        [Button]
        public void PauseMusic()
        {
            audioSources.ForEach(s => s.Pause());
        }

        [Button]
        public void ResumeMusic()
        {
            audioSources.ForEach(s => s.UnPause());
        }
    }
}
