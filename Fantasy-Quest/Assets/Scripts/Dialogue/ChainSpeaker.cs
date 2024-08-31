using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Common;
using Common.DI;
using DI.Project.Services;
using Sirenix.OdinInspector;
using Subtitles;
using UnityEngine;
using VContainer;

namespace Dialogue
{
    [AddComponentMenu("Scripts/Dialogue/Dialogue.ChainSpeaker")]
    internal class ChainSpeaker : InjectingMonoBehaviour
    {
        [Inject]
        private SoundsManager soundsManager;

        [Inject]
        private ISubtitlesView subtitles;

        [SerializeField]
        private List<Replica> replicas;

        public float Duration => replicas.Sum(s => s.Duration);

        public List<Replica> Replicas
        {
            get => replicas;
            set => replicas = value;
        }

        private Voice voice;

        public void UpdateReplicaString(int index, string label)
        {
            replicas[index].UpdateString(label);
            if (currentReplica == replicas[index])
            {
                subtitles.UpdateText(label);
            }
        }

        private void Start()
        {
            voice = new(soundsManager, gameObject.name);
        }

        private Coroutine coroutine;
        private bool isWithSubtitles = false;

        [Button]
        public void StopTelling()
        {
            _ = this.KillCoroutine(coroutine);
            voice.Silence();
            currentReplica = null;
            if (isWithSubtitles)
            {
                subtitles.Hide();
                isWithSubtitles = false;
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
            subtitles.Show(Replicas.First());
        }

        [Button]
        public void HideSubtitles()
        {
            subtitles.Hide();
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

        private Replica currentReplica = null;

        public IEnumerator TellRoutine(Action nextAction)
        {
            isWithSubtitles = true;
            foreach (Replica replica in Replicas)
            {
                voice.Say(replica);
                subtitles.Show(replica);
                currentReplica = replica;
                yield return new WaitForSeconds(replica.Duration + replica.DelayAfterSaid);
                currentReplica = null;
            }
            isWithSubtitles = false;
            subtitles.Hide();
            nextAction?.Invoke();
        }

        public IEnumerator JustTellRoutine()
        {
            isWithSubtitles = true;
            foreach (Replica replica in Replicas)
            {
                voice.Say(replica);
                subtitles.Show(replica);
                currentReplica = replica;
                yield return new WaitForSeconds(replica.Duration + replica.DelayAfterSaid);
                currentReplica = null;
            }
            isWithSubtitles = false;
            subtitles.Hide();
        }

        private IEnumerator JustTellWithoutSubtitlesRoutine(Action nextAction = null)
        {
            foreach (Replica replica in Replicas)
            {
                voice.Say(replica);
                yield return new WaitForSeconds(replica.Duration + replica.DelayAfterSaid);
            }
        }
    }
}
