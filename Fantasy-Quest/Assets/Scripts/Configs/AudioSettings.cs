using UnityEngine;
using UnityEngine.Audio;

namespace Configs
{
    [CreateAssetMenu(fileName = "Audio Settings", menuName = "Settings/Create Audio Settings")]
    internal class AudioSettings : SingletonScriptableObject<AudioSettings>
    {
        [SerializeField]
        private AudioMixer audioMixer;

        [Range(0, 1)]
        [SerializeField]
        private float musicValue;

        public float MusicValue
        {
            get => musicValue;
            set
            {
                SetMusicVolume(value);
                musicValue = value;
            }
        }

        [Range(0, 1)]
        [SerializeField]
        private float soundsValue;

        public float SoundsValue
        {
            get => soundsValue;
            set
            {
                SetSoundsVolume(value);
                soundsValue = value;
            }
        }

        private const string MUSIC_VOLUME_LABEL = "Music";
        private const string SOUND_VOLUME_LABEL = "Sounds";

        private void SetMusicVolume(float value)
        {
            _ = audioMixer.SetFloat(MUSIC_VOLUME_LABEL, CalculateVolume(value));
        }

        private void SetSoundsVolume(float value)
        {
            _ = audioMixer.SetFloat(SOUND_VOLUME_LABEL, CalculateVolume(value));
        }

        private const float MIN_DB = -80;
        private const float MAX_DB = 20;

        private float CalculateVolume(float value)
        {
            return Mathf.Clamp(20 * Mathf.Log10(value), MIN_DB, MAX_DB);
        }

        public void RefreshAudio()
        {
            SetMusicVolume(MusicValue);
            SetSoundsVolume(SoundsValue);
        }
    }
}
