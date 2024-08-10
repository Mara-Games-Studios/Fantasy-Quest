using System.Collections;
using Common.DI;
using DI.Project.Services;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace Audio
{
    [AddComponentMenu("Scripts/Audio/Audio.SoundPlayer")]
    public class SoundPlayer : InjectingMonoBehaviour
    {
        [Inject]
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

        private void Start()
        {
            audioSource = soundsManager.CreateSource(AudioClip.name, IgnorePause);
            audioSource.clip = AudioClip;
            audioSource.loop = Loop;
            AudioSourceConfig.ApplyTo(audioSource);
            if (CreateOnThisPosition)
            {
                audioSource.transform.position = transform.position;
            }

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

        private IEnumerator PlayClipDelayedRoutine(float delay)
        {
            yield return new WaitForSeconds(delay);
            audioSource.Play();
        }

        [Button]
        public void PlayClipDelayed(float delay)
        {
            _ = StartCoroutine(PlayClipDelayedRoutine(delay));
        }

        [Button]
        public void StopClip()
        {
            audioSource.Stop();
        }
    }
}
