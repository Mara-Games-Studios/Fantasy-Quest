using Sirenix.OdinInspector;
using UnityEngine;

namespace Audio
{
    [AddComponentMenu("Scripts/Audio/Audio.SoundPlayer")]
    internal class SoundPlayer : MonoBehaviour
    {
        [RequiredIn(PrefabKind.PrefabInstanceAndNonPrefabInstance)]
        [SerializeField]
        private SoundsManager soundsManager;

        [RequiredIn(PrefabKind.PrefabInstanceAndNonPrefabInstance)]
        [SerializeField]
        private AudioClip audioClip;

        [SerializeField]
        private AudioSourceConfig audioSourceConfig;

        [SerializeField]
        private bool loop = false;

        [SerializeField]
        private bool playOnStart = false;

        [SerializeField]
        private bool ignorePause = false;

        [SerializeField]
        private bool createOnThisPosition = false;

        [ReadOnly]
        [SerializeField]
        private AudioSource audioSource;
        public AudioSource AudioSource => audioSource;

        public AudioClip AudioClip
        {
            get => audioClip;
            set => audioClip = value;
        }
        public AudioSourceConfig AudioSourceConfig
        {
            get => audioSourceConfig;
            set => audioSourceConfig = value;
        }
        public bool Loop
        {
            get => loop;
            set => loop = value;
        }
        public bool PlayOnStart
        {
            get => playOnStart;
            set => playOnStart = value;
        }
        public bool IgnorePause
        {
            get => ignorePause;
            set => ignorePause = value;
        }
        public bool CreateOnThisPosition
        {
            get => createOnThisPosition;
            set => createOnThisPosition = value;
        }

        private void Awake()
        {
            audioSource = soundsManager.CreateSource(AudioClip.name, IgnorePause);
            audioSource.clip = AudioClip;
            audioSource.loop = Loop;
            AudioSourceConfig.ApplyTo(audioSource);
            if (CreateOnThisPosition)
            {
                audioSource.transform.position = transform.position;
            }
        }

        private void Start()
        {
            if (PlayOnStart)
            {
                PlayClip();
            }
        }

        [Button]
        public void PlayClip()
        {
            audioSource.Play();
        }

        [Button]
        public void PlayClipDelayed(ulong delay)
        {
            audioSource.Play(delay);
        }

        [Button]
        public void StopClip()
        {
            audioSource.Stop();
        }
    }
}
