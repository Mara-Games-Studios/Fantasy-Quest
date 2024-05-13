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

        [InfoBox("CALLED BY 1")]
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
