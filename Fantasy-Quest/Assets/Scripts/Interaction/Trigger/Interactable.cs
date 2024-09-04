using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Interaction.Trigger
{
    [RequireComponent(typeof(Collider2D))]
    [AddComponentMenu("Scripts/Interaction/Trigger/Interaction.Trigger.Interactable")]
    internal class Interactable : MonoBehaviour, IInteractable
    {
        [InfoBox("CALLED BY E")]
        public UnityEvent TriggeredByCat;

        public void Interact()
        {
            TriggeredByCat?.Invoke();
        }
    }
}
