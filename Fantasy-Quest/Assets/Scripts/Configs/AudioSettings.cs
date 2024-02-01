using UnityEngine;
using UnityEngine.Audio;

namespace Configs
{
    [CreateAssetMenu(
        fileName = "Audio Settings",
        menuName = "Settings/Create Audio Settings",
        order = 2
    )]
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

        private const string music_volume_label = "Music";
        private const string sounds_volume_label = "Sounds";

        private void SetMusicVolume(float value)
        {
            _ = audioMixer.SetFloat(music_volume_label, CalculateVolume(value));
        }

        private void SetSoundsVolume(float value)
        {
            _ = audioMixer.SetFloat(sounds_volume_label, CalculateVolume(value));
        }

        private const float min_db = -80;
        private const float max_db = 0;
        private const float min_threshold = 0.05f;

        private float CalculateVolume(float value)
        {
            return value <= min_threshold ? min_db : Mathf.Lerp(min_db, max_db, value);
        }

        public void RefreshAudio()
        {
            SetMusicVolume(MusicValue);
            SetSoundsVolume(SoundsValue);
        }
    }
}
