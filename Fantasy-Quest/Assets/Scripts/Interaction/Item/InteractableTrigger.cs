using UnityEngine;
using UnityEngine.Events;

namespace Interaction.Item
{
    [AddComponentMenu("Scripts/Interaction/Item/Interaction.Item.InteractableTrigger")]
    internal class InteractableTrigger : MonoBehaviour, IInteractable
    {
        public UnityEvent TriggeredByCat;
        public UnityEvent TriggeredByHuman;

        public void InteractByCat()
        {
            TriggeredByCat?.Invoke();
        }

        public void InteractByHuman()
        {
            TriggeredByHuman?.Invoke();
        }
    }
}
