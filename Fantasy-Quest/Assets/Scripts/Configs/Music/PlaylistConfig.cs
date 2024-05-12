using System;
using System.Collections.Generic;
using UnityEngine;

namespace Configs.Music
{
    [Serializable]
    public struct AudioClipConfig
    {
        public AudioClip AudioClip;
    }

    [CreateAssetMenu(fileName = "Playlist Config", menuName = "Configs/Create Playlist Config")]
    internal class PlaylistConfig : ScriptableObject
    {
        [SerializeField]
        private List<AudioClipConfig> audioClips;
        public List<AudioClipConfig> AudioClips => audioClips;
    }
}
