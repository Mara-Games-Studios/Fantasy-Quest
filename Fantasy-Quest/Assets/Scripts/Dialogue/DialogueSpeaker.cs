using System.Collections;
using System.Collections.Generic;
using Subtitles;
using TNRD;
using UnityEngine;

namespace Dialogue
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(AudioSource))]
    [AddComponentMenu("Scripts/Dialogue/Dialogue.DialogueSpeaker")]
    public class DialogueSpeaker : MonoBehaviour, ISpeakable
    {
        [Header("Speech")]
        [SerializeField]
        protected List<Replica> FirstTrySpeech;

        [SerializeField]
        protected List<Replica> AlternativeSpeech;

        [Space]
        [Header("Components")]
        [SerializeField]
        protected SerializableInterface<ISubtitlesView> SubtitlesView;

        protected Coroutine SayCoroutine;
        protected Voice Voice;
        protected bool WasSaid;

        protected virtual void Awake()
        {
            Voice = new Voice(GetComponent<AudioSource>());
        }

        public void Speak()
        {
            Debug.Log("Trying to talk");
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
                SayCoroutine = null;
            }

            Voice.Silence();
            SubtitlesView.Value.Hide();
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
                SubtitlesView.Value.Show(replica);
                yield return new WaitForSeconds(replica.DelayBeforeSaid);
                Voice.Say(replica.Audio);
                yield return new WaitForSeconds(replica.Duration + replica.DelayAfterSaid);
            }
            Stop();
        }

        protected bool HasISubtitlesView(GameObject gameObject)
        {
            return gameObject.TryGetComponent(out ISubtitlesView _);
        }

        public void Pause()
        {
            Voice.Pause();
        }

        public void Resume()
        {
            Voice.Resume();
        }

        public void Kill()
        {
            Stop();
        }
    }
}
