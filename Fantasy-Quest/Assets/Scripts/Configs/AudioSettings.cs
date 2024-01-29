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

        private void SetMusicVolume(float value)
        {
            Debug.Log(CalculateVolume(value));
            _ = audioMixer.SetFloat("Music", CalculateVolume(value));
        }

        private void SetSoundsVolume(float value)
        {
            _ = audioMixer.SetFloat("Sounds", CalculateVolume(value));
        }

        private float CalculateVolume(float value)
        {
            return value == 0 ? -40 : Mathf.Log10(value) * 20;
        }

        public void RefreshAudio()
        {
            SetMusicVolume(MusicValue);
            SetSoundsVolume(SoundsValue);
        }
    }
}
