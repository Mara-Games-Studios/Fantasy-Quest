using Common.DI;
using DI.Project.Services;
using UnityEngine;
using VContainer;

namespace DialogueBubble
{
    [AddComponentMenu("Scripts/DialogueBubble/DialogueBubble.Controller")]
    internal class Controller : InjectingMonoBehaviour
    {
        [Inject]
        private BubbleEventSystem bubbleEventSystem;

        [SerializeField]
        private GameObject dialogueBubble;

        private void Start()
        {
            bubbleEventSystem.OnTriggerBubble += SetBubbleShow;
        }

        private void OnDestroy()
        {
            bubbleEventSystem.OnTriggerBubble -= SetBubbleShow;
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
