using Sirenix.OdinInspector;
using UnityEngine;

namespace Audio
{
    [AddComponentMenu("Scripts/Audio/Audio.SoundPlayer")]
    internal class SoundPlayer : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private SoundsManager soundsManager;

        [Required]
        [SerializeField]
        private AudioClip audioClip;

        [SerializeField]
        private bool loop = false;

        [SerializeField]
        private bool playOnStart = false;

        [SerializeField]
        private bool ignorePause = false;

        private AudioSource audioSource;

        private void Awake()
        {
            audioSource = soundsManager.CreateSource(ignorePause);
            audioSource.loop = loop;
            audioSource.clip = audioClip;
        }

        private void Start()
        {
            if (playOnStart)
            {
                PlayClip();
            }
        }

        [Button]
        public void PlayClip()
        {
            audioSource.Play();
        }
    }
}
