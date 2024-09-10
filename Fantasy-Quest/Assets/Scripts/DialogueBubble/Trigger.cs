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
        private bool oneTime = false;

        [SerializeField]
        private Type emoteType = Type.Thought;

        [SerializeField]
        private List<Sprite> emoteIcons = new();

        private BubbleConfig enterSettings;
        private BubbleConfig exitSettings;

        private void Start()
        {
            enterSettings = new BubbleConfig
            {
                CanShow = true,
                BubbleType = emoteType,
                EmoteIcons = emoteIcons
            };

            exitSettings = new()
            {
                CanShow = false,
                BubbleType = emoteType,
                EmoteIcons = emoteIcons
            };
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out InteractionImpl interaction))
            {
                bubbleEventSystem.OnTriggerBubble?.Invoke(enterSettings);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out InteractionImpl _))
            {
                bubbleEventSystem.OnTriggerBubble?.Invoke(exitSettings);
                if (oneTime)
                {
                    Destroy(this);
                }
            }
        }
    }
}
