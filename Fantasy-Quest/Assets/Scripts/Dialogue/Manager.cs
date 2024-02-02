using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Dialogue
{
    [AddComponentMenu("Scripts/Dialogue/Dialogue.Manager")]
    internal class Manager : MonoBehaviour
    {
        [ReadOnly]
        [SerializeField]
        private List<DialogueSpeaker> speakers;

        public void Pause()
        {
            foreach (DialogueSpeaker speaker in speakers)
            {
                speaker.Pause();
            }
        }

        public void Resume()
        {
            foreach (DialogueSpeaker speaker in speakers)
            {
                speaker.Resume();
            }
        }

        [Button]
        private void CatchAllSpeakers()
        {
            speakers = FindObjectsOfType<DialogueSpeaker>().ToList();
        }
    }
}
