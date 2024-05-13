using System.Collections;
using System.Collections.Generic;
using Audio;
using Sirenix.OdinInspector;
using Subtitles;
using TNRD;
using UnityEngine;

namespace Dialogue
{
    [RequireComponent(typeof(Collider2D))]
    [AddComponentMenu("Scripts/Dialogue/Dialogue.DialogueSpeaker")]
    public class DialogueSpeaker : MonoBehaviour, ISpeakable
    {
        [InfoBox("CALLED BY 1")]
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
            SoundsManager soundsManager = GameObject.FindAnyObjectByType<SoundsManager>();
            Voice = new Voice(soundsManager, gameObject.name);
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

            // BUGBUGBUBGUGBUGBUBGUBGUBGUBGUBGU
            Voice?.Silence();
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
                Voice.Say(replica);
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
            //BUGBUGBUGBUGBUGBUGBUGBUGBUGBUGBUGBUG
            Voice.Pause();
        }

        public void Resume()
        {
            //BUGBUGBUGBUGBUGBUGBUGBUGBUG
            Voice.Resume();
        }

        public void Kill()
        {
            Stop();
        }
    }
}
