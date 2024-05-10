using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Audio;
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
        private List<Replica> replicas;

        [SerializeField]
        private SerializableInterface<ISubtitlesView> subtitles;
        private ISubtitlesView Subtitles => subtitles.Value;

        private Voice voice;

        private void Awake()
        {
            SoundsManager soundsManager = GameObject.FindAnyObjectByType<SoundsManager>();
            voice = new(soundsManager);
        }

        public void JustTell()
        {
            _ = StartCoroutine(JustTellRoutine());
        }

        public void Tell(Action nextAction)
        {
            _ = StartCoroutine(TellRoutine(nextAction));
        }

        public IEnumerator Tell()
        {
            yield return TellRoutine(() => { });
        }

        private IEnumerator TellRoutine(Action nextAction)
        {
            foreach (Replica replica in replicas)
            {
                voice.Say(replica.Audio);
                Subtitles.Show(replica);
                yield return new WaitForSeconds(replica.Duration + replica.DelayAfterSaid);
            }
            Subtitles.Hide();
            nextAction?.Invoke();
        }

        public IEnumerator JustTellRoutine()
        {
            foreach (Replica replica in replicas)
            {
                voice.Say(replica.Audio);
                Subtitles.Show(replica);
                yield return new WaitForSeconds(replica.Duration + replica.DelayAfterSaid);
            }
            Subtitles.Hide();
        }

        [Button]
        private void FindSubtitlesView()
        {
            subtitles.Value =
                FindObjectsOfType<MonoBehaviour>().First(x => x is ISubtitlesView)
                as ISubtitlesView;
        }
    }
}
