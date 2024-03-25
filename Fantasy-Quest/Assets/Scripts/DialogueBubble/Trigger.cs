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
        private Type isEmote = Type.Thought;

        //Script can be changed for different situation, so different icons cab be used from one trigger
        [SerializeField]
        private List<Sprite> kbIcons = new();

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
                            BubbleType = isEmote,
                            KBicons = kbIcons,
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
                            BubbleType = isEmote,
                            KBicons = kbIcons,
                            EmoteIcons = emoteIcons
                        }
                    );
                }
            }
        }

        public void SetNewIcon(Sprite newIcon)
        {
            kbIcons.Clear();
            kbIcons.Add(newIcon);
        }
    }
}
