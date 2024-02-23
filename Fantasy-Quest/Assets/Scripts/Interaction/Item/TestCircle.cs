using UnityEngine;

namespace Interaction.Item
{
    [AddComponentMenu("Scripts/Interaction/Item/Interaction.Item.TestInteractableCircle")]
    internal class TestCircle : MonoBehaviour, IInteractable
    {
        public bool CanCatInteract => true;

        public void InteractByCat()
        {
            Debug.Log("Cat Interacted");
        }

        public void InteractByHuman()
        {
            Debug.Log("Human Interacted");
        }
    }
}
