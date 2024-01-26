using System.Collections.Generic;
using UnityEngine;

namespace DialogueBubble
{
    [AddComponentMenu("Scripts/DialogueBubble/DialogueBubble.Trigger")]
    public class Trigger : MonoBehaviour
    {
        [SerializeField]
        private bool isEmote = false;

        //Script can be changed for different situation, so different icons cab be used from one trigger
        [SerializeField]
        private List<Sprite> icon = new();

        private void OnTriggerEnter2D(Collider2D other)
        {
            EventSystem.OnTriggerBubble?.Invoke(
                new BubbleSettings
                {
                    CanShow = true,
                    IsEmote = isEmote,
                    Icon = icon[0]
                }
            );
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            EventSystem.OnTriggerBubble?.Invoke(
                new BubbleSettings
                {
                    CanShow = false,
                    IsEmote = isEmote,
                    Icon = icon[0]
                }
            );
        }
    }
}
