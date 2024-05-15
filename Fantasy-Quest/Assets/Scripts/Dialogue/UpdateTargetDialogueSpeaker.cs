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
        private int firstTrySpeechIndex = 0;

        [SerializeField]
        private int alternativeSpeechIndex = 0;

        [Button]
        public void UpdateFirstTrySpeechText(string text)
        {
            dialogueSpeaker.FFirstTrySpeech[firstTrySpeechIndex].UpdateString(text);
        }

        [Button]
        public void UpdateAlternativeSpeechText(string text)
        {
            dialogueSpeaker.AAlternativeSpeech[alternativeSpeechIndex].UpdateString(text);
        }
    }
}
