using Interaction.InteractableItem;
using UnityEngine;

namespace Interaction.Items
{
    [AddComponentMenu("Scripts/Interaction/Items/Interaction.Items.TestInteractableCircle")]
    internal class TestCircle : MonoBehaviour, IInteractableItem
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
