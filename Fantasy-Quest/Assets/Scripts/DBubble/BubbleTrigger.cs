using UnityEngine;

public class BubbleTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        BubbleEventSystem.TriggerBubble(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        BubbleEventSystem.TriggerBubble(false);
    }
}
