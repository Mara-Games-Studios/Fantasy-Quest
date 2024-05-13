using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Interaction.Item
{
    [AddComponentMenu("Scripts/Interaction/Item/Interaction.Item.CallHumanMoveTrigger")]
    internal class CallHumanMoveTrigger : MonoBehaviour, ICallHumanMove
    {
        [InfoBox("CALLED BY 2")]
        public UnityEvent OnCallHumanMove;

        public void CallHumanMove()
        {
            OnCallHumanMove?.Invoke();
        }
    }
}
