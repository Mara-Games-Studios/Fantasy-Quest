using System.Collections.Generic;
using Common.DI;
using Configs;
using DI.Project.Services;
using Interaction;
using UnityEngine;
using VContainer;

namespace DialogueBubble
{
    [AddComponentMenu("Scripts/DialogueBubble/DialogueBubble.Trigger")]
    public class Trigger : InjectingMonoBehaviour
    {
        [Inject]
        private LockerApi lockerSettings;

        [Inject]
        private BubbleEventSystem bubbleEventSystem;

        [SerializeField]
        private bool oneTimer = false;

        [SerializeField]
        private Type emoteType = Type.Thought;

        [SerializeField]
        private List<Sprite> emoteIcons = new();

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!lockerSettings.Api.IsDialogueBubbleLocked)
            {
                if (other.TryGetComponent(out InteractionImpl interaction))
                {
                    bubbleEventSystem.OnTriggerBubble?.Invoke(
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
            if (!lockerSettings.Api.IsDialogueBubbleLocked)
            {
                if (other.TryGetComponent(out InteractionImpl _) && !oneTimer)
                {
                    bubbleEventSystem.OnTriggerBubble?.Invoke(
                        new BubbleSettings
                        {
                            CanShow = false,
                            BubbleType = emoteType,
                            EmoteIcons = emoteIcons
                        }
                    );
                }
                else if (other.TryGetComponent(out InteractionImpl _) && oneTimer)
                {
                    bubbleEventSystem.OnTriggerBubble?.Invoke(
                        new BubbleSettings
                        {
                            CanShow = false,
                            BubbleType = emoteType,
                            EmoteIcons = emoteIcons
                        }
                    );
                    Destroy(this);
                }
            }
        }
    }
}
