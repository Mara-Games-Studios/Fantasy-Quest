using System;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueBubble
{
    public struct BubbleSettings
    {
        public bool CanShow;
        public ETypes BubbleType;
        public List<Sprite> Icons;
    }

    [AddComponentMenu("Scripts/DialogueBubble/DialogueBubble.EventSystem")]
    public class EventSystem : MonoBehaviour
    {
        public static EventSystem Instance = null;

        public static Action<BubbleSettings> OnTriggerBubble;

        private void Start()
        {
            if (Instance != null)
            {
                Debug.LogError("More than one BubbleEventSystem in scene.");
            }
            Instance = this;

            DontDestroyOnLoad(gameObject);
        }
    }
}
