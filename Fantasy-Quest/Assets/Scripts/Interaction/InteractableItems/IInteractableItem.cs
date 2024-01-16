using UnityEngine;

namespace Interaction.InteractableItem
{
    [AddComponentMenu(
        "Scripts/Interaction/InteractableItems/Interaction.InteractableItems.I_InteractableItem"
    )]
    internal interface IInteractableItem
    {
        public bool CanCatInteract();
        public void InteractionByCat();

        public void InteractionByHuman();
    }
}
