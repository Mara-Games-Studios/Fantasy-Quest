using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Configs.Music
{
    [Serializable]
    [InlineProperty]
    internal class PlaylistHolder
    {
        [AssetList]
        [SerializeField]
        private PlaylistConfig playlistConfig;

        public PlaylistConfig Value => playlistConfig;
    }
}
