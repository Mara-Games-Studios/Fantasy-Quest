using Configs.Music;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Audio
{
    [AddComponentMenu("Scripts/Audio/Audio.MusicPlayer")]
    internal class MusicPlayer : MonoBehaviour
    {
        [Required]
        [AssetList]
        [SerializeField]
        private PlaylistConfig playlist;

        [SerializeField]
        private bool playImmediately;

        [Required]
        [SerializeField]
        private MusicManager musicManager;

        [Button]
        public void Play()
        {
            musicManager.SwitchPlaylist(playlist, playImmediately);
        }
    }
}
