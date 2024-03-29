using System.Collections.Generic;
using UnityEngine;

namespace DialogueBubble
{
    [AddComponentMenu("Scripts/DialogueBubble/DialogueBubble.Controller")]
    internal class Controller : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> keyboardHints;

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
                case Type.OneButton:
                    keyboardHints[0].GetComponent<IShowBubble>().SwitchShow(settings);
                    break;
                case Type.TwoButtons:
                    for (int i = 1; i < keyboardHints.Count; i++)
                    {
                        keyboardHints[i].GetComponent<IShowBubble>().SwitchShow(settings);
                    }
                    break;
            }
        }
    }
}
