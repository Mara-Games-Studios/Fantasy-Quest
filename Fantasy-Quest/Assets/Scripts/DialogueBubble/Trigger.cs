using UnityEngine;

namespace DialogueBubble
{
    [AddComponentMenu("Scripts/DialogueBubble/DialogueBubble.Trigger")]
    public class Trigger : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            EventSystem.OnTriggerBubble?.Invoke(true);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            EventSystem.OnTriggerBubble?.Invoke(false);
        }
    }
}
