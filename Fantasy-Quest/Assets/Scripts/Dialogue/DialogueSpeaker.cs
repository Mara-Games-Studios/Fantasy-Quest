using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SubtitlesSystem;
using UnityEngine;

namespace Dialogue
{
    [RequireComponent(typeof(Collider2D), typeof(AudioSource))]
    public class DialogueSpeaker : MonoBehaviour, ISpeakable
    {
        #region Fields
        
        [Header("Speech")] 
        [SerializeField] 
        private List<Replica> firstTrySpeech;
        [SerializeField] 
        private List<Replica> alternativeSpeech;
        [Space]
        [Header("Components")]
        [SerializeField] 
        private GameObject subtitlesView;

        private bool wasSaid;
        private Coroutine waitCoroutine;
        private Voice voice;
        private ISubtitlesView iSubtitlesView;
        private Queue<Replica> replicasToShow;

        #endregion

        private void Awake()
        {
            iSubtitlesView = subtitlesView.GetComponent<ISubtitlesView>();
            voice = new Voice(GetComponent<AudioSource>());
            replicasToShow = new Queue<Replica>(firstTrySpeech.Count);
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
            if (replicasToShow.Any())
            {
                replicasToShow.Clear();
            }

            if (waitCoroutine != null)
            {
                StopCoroutine(waitCoroutine);
                waitCoroutine = null;
            }
            
            voice.Silence();
            iSubtitlesView.Hide();
        }

        private void Say(List<Replica> speech)
        {
            Stop();

            foreach (Replica replica in speech)
            {
                replicasToShow.Enqueue(replica);
            }

            waitCoroutine = StartCoroutine(SayQueue());
        }
        
        private IEnumerator SayQueue()
        {
            Replica replica;
            float audioLength;
            while (replicasToShow.Count > 0)
            {
                replica = replicasToShow.Dequeue();
                audioLength = replica.Audio.length;
                voice.Say(replica.Audio);
                iSubtitlesView.Show(replica.Text, audioLength, replica.delayAfterSaid);
                yield return new WaitForSecondsRealtime(audioLength + replica.delayAfterSaid);
            }
            Stop();
        }
    }
}