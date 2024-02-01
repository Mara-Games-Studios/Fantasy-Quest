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
        [SerializeField]
        private PlaylistHolder startPlaylist;

        [ReadOnly]
        [SerializeField]
        private List<AudioClip> currentClips;

        private AudioSource musicSource;

        private void Awake()
        {
            this.InitSingleton();
            musicSource = GetComponent<AudioSource>();
            Debug.Assert(musicSource != null, "Music Source is NULL", musicSource);
            SwitchPlaylist(startPlaylist.Value, true);
            musicSource.Play();
        }

        private void Update()
        {
            if (!musicSource.isPlaying)
            {
                ChooseFromPlaylist();
            }
        }

        [Button]
        public void SwitchPlaylist(PlaylistConfig playlist, bool playImmediately)
        {
            currentClips = playlist.AudioClips.ToList();
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
        }

        [Button]
        private void ChooseFromPlaylist()
        {
            musicSource.clip = currentClips[Random.Range(0, currentClips.Count)];
        }

        public void MigrateSingleton(MusicManager instance) { }
    }
}
