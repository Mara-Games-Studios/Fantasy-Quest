using Sirenix.OdinInspector;
using UnityEngine;

namespace Inventory
{
    [AddComponentMenu("Scripts/Inventory/Inventory.ItemTaker")]
    internal class ItemTaker : MonoBehaviour
    {
        [ReadOnly]
        [SerializeField]
        private Item takenItem;
        public Item TakenItem => takenItem;

        [Required]
        [SerializeField]
        private Cat.View catView;

        [Required]
        [SerializeField]
        private Item egg;

        [Required]
        [SerializeField]
        private Item acorn;

        [Button]
        public void TakeItem(Item item)
        {
            if (takenItem != null)
            {
                Debug.Log("Item already taken.");
                return;
            }
            takenItem = item;
            if (takenItem.UidEquals(egg))
            {
                catView.SetEggTaken(true);
            }
            if (takenItem.UidEquals(acorn))
            {
                catView.SetAcornTaken(true);
            }
        }

        [Button]
        public void RemoveItem()
        {
            catView.SetEggTaken(false);
            catView.SetAcornTaken(false);
            takenItem = null;
        }
    }
}
