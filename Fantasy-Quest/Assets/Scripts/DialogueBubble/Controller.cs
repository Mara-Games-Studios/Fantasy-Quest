using UnityEngine;

namespace DialogueBubble
{
    [AddComponentMenu("Scripts/DialogueBubble/DialogueBubble.Controller")]
    internal class Controller : MonoBehaviour
    {
        [SerializeField]
        private GameObject dialogueBubble;

        private void OnEnable()
        {
            EventSystem.OnTriggerBubble += SetBubbleShow;
        }

        private void OnDisable()
        {
            EventSystem.OnTriggerBubble -= SetBubbleShow;
        }

        private void SetBubbleShow(BubbleSettings settings)
        {
            switch (settings.BubbleType)
            {
                case Type.Dialogue:
                case Type.Thought:
                    dialogueBubble.GetComponent<IShowBubble>().SwitchShow(settings);
                    break;
            }
        }
    }
}
