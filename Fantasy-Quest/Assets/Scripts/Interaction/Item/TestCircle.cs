using UnityEngine;

namespace Interaction.Item
{
    [AddComponentMenu("Scripts/Interaction/Item/Interaction.Item.TestInteractableCircle")]
    internal class TestCircle : MonoBehaviour, IInteractable
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
