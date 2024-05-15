using Sirenix.OdinInspector;
using UnityEngine;

namespace Dialogue
{
    [AddComponentMenu("Scripts/Dialogue/Dialogue.UpdateTargetDialogueSpeaker")]
    internal class UpdateTargetDialogueSpeaker : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private DialogueSpeaker dialogueSpeaker;

        [SerializeField]
        private int firstTrySpeechIndex = -1;

        [SerializeField]
        private int alternativeSpeechIndex = -1;

        [Button]
        public void UpdateFirstTrySpeechText(string text)
        {
            dialogueSpeaker.UpdateFFirstTrySpeechReplica(firstTrySpeechIndex, text);
        }

        [Button]
        public void UpdateAlternativeSpeechText(string text)
        {
            dialogueSpeaker.UpdateFAlternativeSpeechReplica(alternativeSpeechIndex, text);
        }
    }
}
