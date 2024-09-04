using System.Collections;
using System.Collections.Generic;
using Common.DI;
using DI.Project.Services;
using Interaction;
using Subtitles;
using UnityEngine;
using VContainer;

namespace Dialogue
{
    [RequireComponent(typeof(Collider2D))]
    [AddComponentMenu("Scripts/Dialogue/Dialogue.DialogueSpeaker")]
    public class DialogueSpeaker : InjectingMonoBehaviour, IInteractable
    {
        [Inject]
        private SoundsManager soundsManager;

        [Inject]
        protected ISubtitlesView Subtitles;

        [SerializeField]
        protected List<Replica> FirstTrySpeech;
        public List<Replica> FFirstTrySpeech => FirstTrySpeech;

        [SerializeField]
        protected List<Replica> AlternativeSpeech;
        public List<Replica> AAlternativeSpeech => AlternativeSpeech;

        protected Coroutine SayCoroutine;
        private Replica currentReplica = null;
        protected Voice Voice;
        protected bool WasSaid;

        public void UpdateFFirstTrySpeechReplica(int index, string text)
        {
            if (index < 0 || index >= FirstTrySpeech.Count)
            {
                return;
            }

            FirstTrySpeech[index].UpdateString(text);
            if (currentReplica == FirstTrySpeech[index])
            {
                Subtitles.UpdateText(text);
            }
        }

        public void UpdateFAlternativeSpeechReplica(int index, string text)
        {
            if (index < 0 || index >= AlternativeSpeech.Count)
            {
                return;
            }

            AlternativeSpeech[index].UpdateString(text);
            if (currentReplica == AlternativeSpeech[index])
            {
                Subtitles.UpdateText(text);
            }
        }

        protected virtual void Start()
        {
            Voice = new Voice(soundsManager, gameObject.name);
        }

        public void Interact()
        {
            Speak();
        }

        public void Speak()
        {
            if (WasSaid)
            {
                Say(AlternativeSpeech);
            }
            else
            {
                Say(FirstTrySpeech);
                WasSaid = true;
            }
        }

        public virtual void Stop()
        {
            if (SayCoroutine != null)
            {
                StopCoroutine(SayCoroutine);
                currentReplica = null;
                SayCoroutine = null;
            }
            Voice?.Silence();
            Subtitles.Hide();
        }

        protected virtual void Say(List<Replica> speech)
        {
            Stop();
            SayCoroutine = StartCoroutine(SayList(speech));
        }

        protected IEnumerator SayList(List<Replica> replicas)
        {
            foreach (Replica replica in replicas)
            {
                Subtitles.Show(replica);
                currentReplica = replica;
                yield return new WaitForSeconds(replica.DelayBeforeSaid);
                Voice.Say(replica);
                yield return new WaitForSeconds(replica.Duration + replica.DelayAfterSaid);
                currentReplica = null;
            }
            Stop();
        }
    }
}
