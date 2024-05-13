using System;
using System.Collections.Generic;
using Audio;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Configs.Music
{
    [Serializable]
    public class AudioClipConfig
    {
        [Required]
        public AudioClip AudioClip;

        public AudioSourceConfig AudioSourceConfig;
    }

    [CreateAssetMenu(fileName = "Playlist Config", menuName = "Configs/Create Playlist Config")]
    internal class PlaylistConfig : ScriptableObject
    {
        [SerializeField]
        private List<AudioClipConfig> audioClips;
        public List<AudioClipConfig> AudioClips => audioClips;
    }
}
