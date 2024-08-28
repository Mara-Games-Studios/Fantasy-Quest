using Sirenix.OdinInspector;
using UnityEngine;

namespace Dialogue
{
    [AddComponentMenu("Scripts/Dialogue/Dialogue.UpdateTargetReplicaProxy")]
    internal class UpdateTargetReplicaProxy : MonoBehaviour
    {
        [SerializeField]
        private int index;

        [Required]
        [SerializeField]
        private ChainSpeaker chainSpeaker;

        /// Called by unity events
        public void UpdateText(string text)
        {
            chainSpeaker.UpdateReplicaString(index, text);
        }
    }
}
