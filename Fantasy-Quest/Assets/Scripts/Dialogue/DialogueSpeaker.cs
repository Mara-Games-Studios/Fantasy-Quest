using System.Collections;
using System.Collections.Generic;
using Common.DI;
using DI.Project.Services;
using Sirenix.OdinInspector;
using Subtitles;
using TNRD;
using UnityEngine;
using VContainer;

namespace Dialogue
{
    [RequireComponent(typeof(Collider2D))]
    [AddComponentMenu("Scripts/Dialogue/Dialogue.DialogueSpeaker")]
    public class DialogueSpeaker : InjectingMonoBehaviour, ISpeakable
    {
        [Inject]
        private SoundsManager soundsManager;

        [InfoBox("CALLED BY 1")]
        [Header("Speech")]
        [SerializeField]
        protected List<Replica> FirstTrySpeech;
        public List<Replica> FFirstTrySpeech => FirstTrySpeech;

        [SerializeField]
        protected List<Replica> AlternativeSpeech;
        public List<Replica> AAlternativeSpeech => AlternativeSpeech;

        [Space]
        [Header("Components")]
        [SerializeField]
        protected SerializableInterface<ISubtitlesView> SubtitlesView;

        protected Coroutine SayCoroutine;
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
                SubtitlesView.Value.UpdateText(text);
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
                SubtitlesView.Value.UpdateText(text);
            }
        }

        protected virtual void Start()
        {
            Voice = new Voice(soundsManager, gameObject.name);
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
            SubtitlesView.Value.Hide();
        }

        protected virtual void Say(List<Replica> speech)
        {
            Stop();
            SayCoroutine = StartCoroutine(SayList(speech));
        }

        private Replica currentReplica = null;

        protected IEnumerator SayList(List<Replica> replicas)
        {
            foreach (Replica replica in replicas)
            {
                SubtitlesView.Value.Show(replica);
                currentReplica = replica;
                yield return new WaitForSeconds(replica.DelayBeforeSaid);
                Voice.Say(replica);
                yield return new WaitForSeconds(replica.Duration + replica.DelayAfterSaid);
                currentReplica = null;
            }
            Stop();
        }

        protected bool HasISubtitlesView(GameObject gameObject)
        {
            return gameObject.TryGetComponent(out ISubtitlesView _);
        }

        public void Kill()
        {
            Stop();
        }
    }
}
