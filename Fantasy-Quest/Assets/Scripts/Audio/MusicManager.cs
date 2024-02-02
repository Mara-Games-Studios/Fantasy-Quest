using System.Collections.Generic;
using System.Linq;
using Common;
using Configs.Music;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Audio
{
    [RequireComponent(typeof(AudioSource))]
    [AddComponentMenu("Scripts/Audio/Audio.MusicManager")]
    internal class MusicManager : MonoBehaviour, ISceneSingleton<MusicManager>
    {
        [AssetList]
        [SerializeField]
        private PlaylistConfig startPlaylist;

        [ReadOnly]
        [SerializeField]
        private List<AudioClip> currentClips;

        [ReadOnly]
        [SerializeField]
        private bool isOnPause = false;

        private AudioSource musicSource;

        private void Awake()
        {
            this.InitSingleton();
            musicSource = GetComponent<AudioSource>();
            Debug.Assert(musicSource != null, "Music Source is NULL", musicSource);
        }

        private void Start()
        {
            Configs.AudioSettings.Instance.RefreshAudio();
            SwitchPlaylist(startPlaylist, true);
        }

        private void Update()
        {
            if (!musicSource.isPlaying && !isOnPause)
            {
                ChooseFromPlaylist();
            }
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
                ChooseFromPlaylist();
            }
            musicSource.loop = false;
        }

        [Button]
        public void PlayConcreteClip(AudioClip clip, bool loop)
        {
            musicSource.clip = clip;
            musicSource.loop = loop;
            musicSource.Play();
        }

        [Button]
        private void ChooseFromPlaylist()
        {
            musicSource.clip = currentClips[Random.Range(0, currentClips.Count)];
            musicSource.Play();
        }

        public void MigrateSingleton(MusicManager instance)
        {
            instance.PauseMusic();
        }

        public void PauseMusic()
        {
            musicSource.Pause();
            isOnPause = true;
        }

        public void ResumeMusic()
        {
            musicSource.UnPause();
            isOnPause = false;
        }
    }
}
