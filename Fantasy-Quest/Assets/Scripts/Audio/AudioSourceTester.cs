using Sirenix.OdinInspector;
using UnityEngine;

namespace Audio
{
    [RequireComponent(typeof(AudioSource))]
    [AddComponentMenu("Scripts/Audio/Audio.AudioSourceTester")]
    internal class AudioSourceTester : MonoBehaviour
    {
        [ReadOnly]
        [SerializeField]
        private AudioSource source;

        private void Awake()
        {
            source = GetComponent<AudioSource>();
        }

        [Button]
        private void Play()
        {
            source.Play();
        }

        [Button]
        private void Pause()
        {
            source.Pause();
        }

        [Button]
        private void UnPause()
        {
            source.UnPause();
        }

        [Button]
        private void Stop()
        {
            source.Stop();
        }
    }
}
