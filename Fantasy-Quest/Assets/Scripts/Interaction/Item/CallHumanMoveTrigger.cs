using UnityEngine;
using UnityEngine.Events;

namespace Interaction.Item
{
    [AddComponentMenu("Scripts/Interaction/Item/Interaction.Item.CallHumanMoveTrigger")]
    internal class CallHumanMoveTrigger : MonoBehaviour, ICallHumanMove
    {
        public UnityEvent OnCallHumanMove;

        public void CallHumanMove()
        {
            OnCallHumanMove?.Invoke();
        }
    }
}
