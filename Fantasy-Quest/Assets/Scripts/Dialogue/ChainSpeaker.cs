using System;
using System.Collections;
using Sirenix.OdinInspector;
using Subtitles;
using TNRD;
using UnityEngine;

namespace Dialogue
{
    [AddComponentMenu("Scripts/Dialogue/Dialogue.ChainSpeaker")]
    internal class ChainSpeaker : MonoBehaviour
    {
        [SerializeField]
        private Replica replica;

        [Required]
        [SerializeField]
        private AudioSource audioSource;

        [SerializeField]
        private SerializableInterface<ISubtitlesView> subtitles;

        private Voice voice;

        private void Awake()
        {
            voice = new(audioSource);
        }

        public void Tell(Action nextAction)
        {
            _ = StartCoroutine(TellRoutine(nextAction));
        }

        private IEnumerator TellRoutine(Action nextAction)
        {
            voice.Say(replica.Audio);
            subtitles.Value.Show(replica);
            yield return new WaitForSeconds(replica.Duration + replica.DelayAfterSaid);
        }
    }
}
