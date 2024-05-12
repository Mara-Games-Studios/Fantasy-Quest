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

        [Button]
        public void JustTell()
        {
            _ = StartCoroutine(JustTellRoutine());
        }

        [Button]
        public void JustTellWithoutSubtitles()
        {
            _ = StartCoroutine(JustTellWithoutSubtitlesRoutine());
        }

        [Button]
        public void ShowSubtitles()
        {
            Subtitles.Show(replicas.First());
        }

        [Button]
        public void HideSubtitles()
        {
            Subtitles.Hide();
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

        private IEnumerator JustTellWithoutSubtitlesRoutine(Action nextAction = null)
        {
            foreach (Replica replica in replicas)
            {
                voice.Say(replica.Audio);
                yield return new WaitForSeconds(replica.Duration + replica.DelayAfterSaid);
            }
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
