using System;
using UnityEngine;

namespace DialogueBubble
{
    [AddComponentMenu("Scripts/DialogueBubble/DialogueBubble.EventSystem")]
    public class EventSystem : MonoBehaviour
    {
        public static EventSystem Instance = null;

        public static Action<bool> OnTriggerBubble;

        void Start()
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
