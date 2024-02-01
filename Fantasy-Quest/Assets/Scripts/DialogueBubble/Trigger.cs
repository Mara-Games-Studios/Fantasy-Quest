using System.Collections.Generic;
using Interaction;
using UnityEngine;

namespace DialogueBubble
{
    [AddComponentMenu("Scripts/DialogueBubble/DialogueBubble.Trigger")]
    public class Trigger : MonoBehaviour
    {
        [SerializeField]
        private static bool canTrigger = false;

        [SerializeField]
        private ETypes isEmote = ETypes.Thought;

        //Script can be changed for different situation, so different icons cab be used from one trigger
        [SerializeField]
        private List<Sprite> icons = new();

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (canTrigger)
            {
                if (other.TryGetComponent<InteractionImpl>(out InteractionImpl interaction))
                {
                    EventSystem.OnTriggerBubble?.Invoke(
                        new BubbleSettings
                        {
                            CanShow = true,
                            BubbleType = isEmote,
                            Icons = icons
                        }
                    );
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (canTrigger)
            {
                if (other.TryGetComponent<InteractionImpl>(out InteractionImpl interaction))
                {
                    EventSystem.OnTriggerBubble?.Invoke(
                        new BubbleSettings
                        {
                            CanShow = false,
                            BubbleType = isEmote,
                            Icons = icons
                        }
                    );
                }
            }
        }

        public void LockTriggers()
        {
            canTrigger = false;
        }

        public void UnlockTriggers()
        {
            canTrigger = true;
        }
    }
}
