using Common.DI;
using DI.Project.Services;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace DialogueBubble
{
    [AddComponentMenu("Scripts/DialogueBubble/DialogueBubble.Notifier")]
    internal class Notifier : InjectingMonoBehaviour
    {
        [Inject]
        private BubbleEventSystem bubbleEventSystem;

        [Required]
        [SerializeField]
        private Shower bubbleShower;

        private void Start()
        {
            bubbleEventSystem.OnTriggerBubble += SetBubbleShow;
        }

        private void OnDestroy()
        {
            bubbleEventSystem.OnTriggerBubble -= SetBubbleShow;
        }

        private void SetBubbleShow(BubbleConfig settings)
        {
            bubbleShower.ShowBubble(settings);
        }
    }
}
