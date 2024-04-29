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

        [SerializeField]
        private float takeAndPlaceDelay = 0.2f;

        private float timer = 0f;

        [Button]
        public void TakeItem(Item item)
        {
            if (takenItem != null)
            {
                Debug.Log("Item already taken.");
                return;
            }
            timer = takeAndPlaceDelay;
            takenItem = item;
        }

        private void Update()
        {
            if (takenItem != null)
            {
                takenItem.transform.position = holdItemPosition.position;
            }

            if (timer >= 0)
            {
                timer -= Time.deltaTime;
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
            if (timer >= 0)
            {
                return;
            }

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
