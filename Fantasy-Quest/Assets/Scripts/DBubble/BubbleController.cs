using UnityEngine;

namespace DBubble
{
    [AddComponentMenu("Scripts/DBubble/DBubble.EnableBubble")]
    internal class BubbleController : MonoBehaviour
    {
        [SerializeField]
        private GameObject bubble;

        private void OnEnable()
        {
            BubbleEventSystem.OnTriggerBubble += SetBubbleShow;
        }

        private void OnDisable()
        {
            BubbleEventSystem.OnTriggerBubble -= SetBubbleShow;
        }

        private void SetBubbleShow(bool state)
        {
            bubble.GetComponent<ShowBubble>().CanShowSwitch(state);
        }
    }
}
