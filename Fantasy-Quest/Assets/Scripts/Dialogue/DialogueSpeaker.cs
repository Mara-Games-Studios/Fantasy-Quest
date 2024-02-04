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
        private List<Replica> firstTrySpeech;

        [SerializeField]
        private List<Replica> alternativeSpeech;

        [Space]
        [Header("Components")]
        [SerializeField]
        [ValidateInput(nameof(HasISubtitlesView), "GameObject must have ISubtitlesView")]
        private GameObject subtitlesViewGameObject;

        private Coroutine sayCoroutine;
        private ISubtitlesView subtitlesView;
        private Voice voice;
        private bool wasSaid;

        private void Awake()
        {
            if (!subtitlesViewGameObject.TryGetComponent(out subtitlesView))
            {
                Debug.LogError(
                    $"{subtitlesView} is NULL\n{GetType()} callback in {gameObject.name}"
                );
            }

            voice = new Voice(GetComponent<AudioSource>());
        }

        public void Speak()
        {
            Debug.Log("Trying to talk");
            if (wasSaid)
            {
                Say(alternativeSpeech);
            }
            else
            {
                Say(firstTrySpeech);
                wasSaid = true;
            }
        }

        public void Stop()
        {
            if (sayCoroutine != null)
            {
                StopCoroutine(sayCoroutine);
                sayCoroutine = null;
            }

            voice.Silence();
            subtitlesView.Hide();
        }

        private void Say(List<Replica> speech)
        {
            Stop();
            sayCoroutine = StartCoroutine(SayList(speech));
        }

        private IEnumerator SayList(List<Replica> replicas)
        {
            foreach (Replica replica in replicas)
            {
                voice.Say(replica.Audio);
                subtitlesView.Show(replica);
                yield return new WaitForSeconds(replica.Duration + replica.DelayAfterSaid);
            }
            Stop();
        }

        private bool HasISubtitlesView(GameObject gameObject)
        {
            return gameObject.TryGetComponent(out ISubtitlesView _);
        }

        public void Pause()
        {
            voice.Pause();
        }

        public void Resume()
        {
            voice.Resume();
        }

        public void Kill()
        {
            Stop();
        }
    }
}
