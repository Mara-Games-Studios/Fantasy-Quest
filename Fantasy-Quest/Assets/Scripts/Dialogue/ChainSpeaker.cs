using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Audio;
using Common;
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

        public List<Replica> Replicas
        {
            get => replicas;
            set => replicas = value;
        }

        private Voice voice;

        private void Awake()
        {
            SoundsManager soundsManager = GameObject.FindAnyObjectByType<SoundsManager>();
            voice = new(soundsManager, gameObject.name);
        }

        private Coroutine coroutine;
        private bool isWithSubtitles = false;

        [Button]
        public void StopTelling()
        {
            _ = this.KillCoroutine(coroutine);
            voice.Silence();
            if (isWithSubtitles)
            {
                Subtitles.Hide();
            }
        }

        [Button]
        public void JustTell()
        {
            IsWrong(coroutine);
            coroutine = StartCoroutine(JustTellRoutine());
        }

        [Button]
        public void JustTellWithoutSubtitles()
        {
            IsWrong(coroutine);
            coroutine = StartCoroutine(JustTellWithoutSubtitlesRoutine());
        }

        [Button]
        public void ShowSubtitles()
        {
            Subtitles.Show(Replicas.First());
        }

        [Button]
        public void HideSubtitles()
        {
            Subtitles.Hide();
        }

        public void Tell(Action nextAction)
        {
            IsWrong(coroutine);
            coroutine = StartCoroutine(TellRoutine(nextAction));
        }

        private void IsWrong(Coroutine coroutine)
        {
            if (coroutine != null)
            {
                Debug.LogError("DoubleTelling");
            }
        }

        public IEnumerator Tell()
        {
            yield return TellRoutine(() => { });
        }

        public IEnumerator TellRoutine(Action nextAction)
        {
            foreach (Replica replica in Replicas)
            {
                voice.Say(replica);
                Subtitles.Show(replica);
                yield return new WaitForSeconds(replica.Duration + replica.DelayAfterSaid);
            }
            Subtitles.Hide();
            nextAction?.Invoke();
        }

        public IEnumerator JustTellRoutine()
        {
            foreach (Replica replica in Replicas)
            {
                voice.Say(replica);
                Subtitles.Show(replica);
                yield return new WaitForSeconds(replica.Duration + replica.DelayAfterSaid);
            }
            Subtitles.Hide();
        }

        private IEnumerator JustTellWithoutSubtitlesRoutine(Action nextAction = null)
        {
            foreach (Replica replica in Replicas)
            {
                voice.Say(replica);
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
