using System;
using System.Collections.Generic;
using Common;
using UnityEngine;

namespace DialogueBubble
{
    public struct BubbleSettings
    {
        public bool CanShow;
        public Type BubbleType;
        public List<Sprite> Icons;
    }

    [AddComponentMenu("Scripts/DialogueBubble/DialogueBubble.EventSystem")]
    public class EventSystem : MonoBehaviour, ISceneSingleton<EventSystem>
    {
        public static Action<BubbleSettings> OnTriggerBubble;

        private void Awake()
        {
            this.InitSingleton();
        }

        public void MigrateSingleton(EventSystem instance) { }
    }
}
