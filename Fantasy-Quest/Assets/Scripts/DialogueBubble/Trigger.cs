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
        private List<Sprite> icons = new();

        private void OnEnable() { }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!LockerSettings.Instance.IsDialogueBubbleLocked)
            {
                Debug.Log($"!LockerSettings.Instance.IsDialogueBubbleLocked = true");
                if (other.TryGetComponent(out InteractionImpl interaction))
                {
                    Debug.Log($"InteractionImpl is found");
                    EventSystem.OnTriggerBubble?.Invoke(
                        new BubbleSettings
                        {
                            CanShow = true,
                            BubbleType = isEmote,
                            Icons = icons
                        }
                    );
                    Debug.Log($"EventSystem invoked");
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
                            Icons = icons
                        }
                    );
                }
            }
        }

        public void SetNewIcon(Sprite newIcon)
        {
            icons.Clear();
            icons.Add(newIcon);
        }
    }
}
