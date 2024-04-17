using Configs;
using Sirenix.OdinInspector;
using UnityEngine;

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
        public Item TakenItem => takenItem;

        [Button]
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

        [Button]
        public void RemoveItem()
        {
            takenItem = null;
        }

        [Button]
        public void PlaceItem()
        {
            if (takenItem == null)
            {
                Debug.Log("No Item to place");
                return;
            }

            takenItem.transform.position = placeItemPosition.position;
            takenItem = null;
            LockerSettings.Instance.UnlockAll();
        }
    }
}
