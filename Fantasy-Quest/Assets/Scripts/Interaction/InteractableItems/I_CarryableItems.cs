using UnityEngine;

namespace Interaction.InteractableItem
{
    [AddComponentMenu(
        "Scripts/Interaction/InteractableItems/Interaction.InteractableItems.I_CarryableItem"
    )]
    internal interface ICarryableItem
    {
        public bool CanCatCarry();
        public void CarryByCat();

        public void CarryByHuman();
    }
}
