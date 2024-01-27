using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(
        fileName = "Audio Settings",
        menuName = "Settings/Create Audio Settings",
        order = 2
    )]
    internal class AudioSettings : SingletonScriptableObject<AudioSettings>
    {
        [Range(0, 1)]
        [SerializeField]
        private float musicValue;

        public float MusicValue
        {
            get => musicValue;
            set => musicValue = value;
        }

        [Range(0, 1)]
        [SerializeField]
        private float volumeValue;

        public float VolumeValue
        {
            get => volumeValue;
            set => volumeValue = value;
        }
    }
}
