using UnityEngine;

namespace DialogueBubble
{
    [AddComponentMenu("Scripts/DialogueBubble/DialogueBubble.Controller")]
    internal class Controller : MonoBehaviour
    {
        [SerializeField]
        private Show bubble;

        private void OnEnable()
        {
            EventSystem.OnTriggerBubble += SetBubbleShow;
        }

        private void OnDisable()
        {
            EventSystem.OnTriggerBubble -= SetBubbleShow;
        }

        private void SetBubbleShow(bool state)
        {
            bubble.CanShowSwitch(state);
        }
    }
}
