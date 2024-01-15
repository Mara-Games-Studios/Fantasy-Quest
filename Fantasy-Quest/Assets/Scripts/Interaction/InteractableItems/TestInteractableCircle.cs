using Interaction.InteractableItem;
using UnityEngine;

namespace Interaction.InteractableItems
{
    [AddComponentMenu(
        "Scripts/Interaction/InteractableItems/Interaction.InteractableItems.TestInteractableCircle"
    )]
    internal class TestInteractableCircle : MonoBehaviour, IInteractableItem
    {
        public bool CanCatInteract()
        {
            return true;
        }

        public void InteractionByCat()
        {
            Debug.Log("Cat Interacted");
        }

        public void InteractionByHuman()
        {
            Debug.Log("Human Interacted");
        }
    }
}
