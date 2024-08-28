using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Interaction.Item
{
    [AddComponentMenu("Scripts/Interaction/Item/Interaction.Item.InteractableTrigger")]
    internal class InteractableTrigger : MonoBehaviour, IInteractable
    {
        [InfoBox("CALLED BY E")]
        public UnityEvent TriggeredByCat;

        public void InteractionByCat()
        {
            TriggeredByCat?.Invoke();
        }
    }
}
