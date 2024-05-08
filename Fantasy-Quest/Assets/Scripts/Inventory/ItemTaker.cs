using Sirenix.OdinInspector;
using Spine.Unity;
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

        [Button]
        public void TakeItem(Item item)
        {
            if (takenItem != null)
            {
                Debug.Log("Item already taken.");
                return;
            }
            takenItem = item;
            takenItem.GetComponent<BoneFollower>().enabled = true;
        }

        [Button]
        public void RemoveItem()
        {
            takenItem.GetComponent<BoneFollower>().enabled = false;
            takenItem = null;
        }
    }
}
