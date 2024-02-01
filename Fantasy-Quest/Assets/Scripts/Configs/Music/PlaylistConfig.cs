using System.Collections.Generic;
using UnityEngine;

namespace Configs.Music
{
    [CreateAssetMenu(fileName = "Playlist Config", menuName = "Configs/Create Playlist Config")]
    internal class PlaylistConfig : ScriptableObject
    {
        [SerializeField]
        private List<AudioClip> audioClips;
        public IEnumerable<AudioClip> AudioClips => audioClips;
    }
}
