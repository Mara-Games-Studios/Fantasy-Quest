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

        public void KillAllSpeakers()
        {
            foreach (DialogueSpeaker speaker in speakers)
            {
                speaker.Kill();
            }
        }

        [Button]
        private void CatchAllSpeakers()
        {
            speakers = FindObjectsOfType<DialogueSpeaker>().ToList();
        }
    }
}
