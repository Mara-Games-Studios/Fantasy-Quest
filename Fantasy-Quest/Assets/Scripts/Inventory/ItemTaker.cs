using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Inventory
{
    [AddComponentMenu("Scripts/Inventory/Inventory.ItemTaker")]
    internal class ItemTaker : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private Transform holdItemPosition;

        [Required]
        [SerializeField]
        private Transform placeItemPosition;

        [ReadOnly]
        [SerializeField]
        private Item takenItem;

        public UnityEvent<Item> ItemPlaced;

        public void TakeItem(Item item)
        {
            if (takenItem != null)
            {
                Debug.Log("Item already taken.");
                return;
            }

            takenItem = item;
        }

        private void Update()
        {
            if (takenItem != null)
            {
                takenItem.transform.position = holdItemPosition.position;
            }
        }

        public void PlaceItem()
        {
            if (takenItem == null)
            {
                Debug.Log("No Item to place");
                return;
            }

            takenItem.transform.position = placeItemPosition.position;
            ItemPlaced?.Invoke(takenItem);
            takenItem = null;
        }
    }
}
