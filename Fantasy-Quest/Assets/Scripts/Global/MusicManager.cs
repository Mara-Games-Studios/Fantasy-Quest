using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Global
{
    public enum MusicTheme
    {
        Default,
        Tragic
    }

    [Serializable]
    public struct ClipsByTheme
    {
        public MusicTheme MusicTheme;
        public List<AudioClip> AudioClips;
    }

    [RequireComponent(typeof(AudioSource))]
    [AddComponentMenu("Scripts/Global/Global.MusicManager")]
    internal class MusicManager : MonoBehaviour, ISingleton<MusicManager>
    {
        [SerializeField]
        private AudioSource music;

        [SerializeField]
        private List<ClipsByTheme> playlists;

        [ReadOnly]
        [SerializeField]
        private List<AudioClip> currentClips;

        private void Awake()
        {
            ISingleton<MusicManager>.InitSingleton(this);
        }

        private void Update()
        {
            if (!music.isPlaying)
            {
                ChooseFromPlaylist();
            }
        }

        [Button]
        public void PlayMusic()
        {
            music.Play();
            SwitchPlaylist(MusicTheme.Default, true);
        }

        [Button]
        public void SwitchPlaylist(MusicTheme musicTheme, bool playImmediately)
        {
            currentClips = playlists.FirstOrDefault(x => x.MusicTheme == musicTheme).AudioClips;
            if (playImmediately)
            {
                ChooseFromPlaylist();
            }
            music.loop = false;
        }

        [Button]
        public void PlayConcreteClip(AudioClip clip, bool loop)
        {
            music.clip = clip;
            music.loop = loop;
        }

        [Button]
        private void ChooseFromPlaylist()
        {
            // TODO: Make smooth change
            music.clip = currentClips[UnityEngine.Random.Range(0, currentClips.Count)];
        }
    }
}
