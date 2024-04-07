using System.Collections.Generic;
using Configs;
using Interaction;
using UnityEngine;

namespace DialogueBubble
{
    [AddComponentMenu("Scripts/DialogueBubble/DialogueBubble.Trigger")]
    public class Trigger : MonoBehaviour
    {
        [SerializeField]
        private Type emoteType = Type.Thought;

        [SerializeField]
        private List<Sprite> emoteIcons = new();

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!LockerSettings.Instance.IsDialogueBubbleLocked)
            {
                if (other.TryGetComponent(out InteractionImpl interaction))
                {
                    EventSystem.OnTriggerBubble?.Invoke(
                        new BubbleSettings
                        {
                            CanShow = true,
                            BubbleType = emoteType,
                            EmoteIcons = emoteIcons
                        }
                    );
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!LockerSettings.Instance.IsDialogueBubbleLocked)
            {
                if (other.TryGetComponent(out InteractionImpl interaction))
                {
                    EventSystem.OnTriggerBubble?.Invoke(
                        new BubbleSettings
                        {
                            CanShow = false,
                            BubbleType = emoteType,
                            EmoteIcons = emoteIcons
                        }
                    );
                }
            }
        }
    }
}
