using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Subtitles;
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
        [ValidateInput(nameof(HasISubtitlesView), "GameObject must have ISubtitlesView")]
        protected GameObject SubtitlesViewGameObject;

        protected Coroutine SayCoroutine;
        protected ISubtitlesView SubtitlesView;
        protected Voice Voice;
        protected bool WasSaid;

        protected virtual void Awake()
        {
            if (!SubtitlesViewGameObject.TryGetComponent(out SubtitlesView))
            {
                Debug.LogError(
                    $"{SubtitlesView} is NULL\n{GetType()} callback in {gameObject.name}"
                );
            }

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
            SubtitlesView.Hide();
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
                Voice.Say(replica.Audio);
                SubtitlesView.Show(replica);
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
