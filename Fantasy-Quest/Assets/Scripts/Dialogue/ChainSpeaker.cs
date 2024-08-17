using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Common;
using Common.DI;
using DI.Project.Services;
using Sirenix.OdinInspector;
using Subtitles;
using TNRD;
using UnityEngine;
using VContainer;

namespace Dialogue
{
    [AddComponentMenu("Scripts/Dialogue/Dialogue.ChainSpeaker")]
    internal class ChainSpeaker : InjectingMonoBehaviour
    {
        [Inject]
        private SoundsManager soundsManager;

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

        [Button]
        public void UpdateReplicaString(int index, string label)
        {
            replicas[index].UpdateString(label);
            if (currentReplica == replicas[index])
            {
                Subtitles.UpdateText(label);
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
                Subtitles.Hide();
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

        private Replica currentReplica = null;

        public IEnumerator TellRoutine(Action nextAction)
        {
            isWithSubtitles = true;
            foreach (Replica replica in Replicas)
            {
                voice.Say(replica);
                Subtitles.Show(replica);
                currentReplica = replica;
                yield return new WaitForSeconds(replica.Duration + replica.DelayAfterSaid);
                currentReplica = null;
            }
            isWithSubtitles = false;
            Subtitles.Hide();
            nextAction?.Invoke();
        }

        public IEnumerator JustTellRoutine()
        {
            isWithSubtitles = true;
            foreach (Replica replica in Replicas)
            {
                voice.Say(replica);
                Subtitles.Show(replica);
                currentReplica = replica;
                yield return new WaitForSeconds(replica.Duration + replica.DelayAfterSaid);
                currentReplica = null;
            }
            isWithSubtitles = false;
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
