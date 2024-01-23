using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Subtitles;
using UnityEngine;

namespace Dialogue
{
    [Serializable]
    public struct Replica
    {
        public string Text;
        public AudioClip Audio;
        public float DelayAfterSaid;

        public Replica(string text, AudioClip audioClip, float delay = 1.2f)
        {
            Text = text;
            Audio = audioClip;
            DelayAfterSaid = delay;
        }
    }

    [RequireComponent(typeof(Collider2D), typeof(AudioSource))]
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
        [ValidateInput("HasISubtitlesView", "GameObject must have ISubtitlesView")]
        private GameObject subtitlesViewGameObject;

        private bool wasSaid;
        private Coroutine sayCoroutine;
        private Voice voice;
        private ISubtitlesView subtitlesView;

        private void Awake()
        {
            subtitlesView = subtitlesViewGameObject.GetComponent<ISubtitlesView>();
            voice = new Voice(GetComponent<AudioSource>());
        }

        public void Speak()
        {
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
                subtitlesView.Show(replica.Text, replica.Audio.length, replica.DelayAfterSaid);
                yield return new WaitForSecondsRealtime(replica.Audio.length + replica.DelayAfterSaid);
            }
            Stop();
        }

        private bool HasISubtitlesView(GameObject gameObject)
        {
            return gameObject.TryGetComponent(out ISubtitlesView view);
        }
    }
}
