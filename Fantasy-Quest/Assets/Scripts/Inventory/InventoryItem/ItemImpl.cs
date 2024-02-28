using Sirenix.OdinInspector;
using UnityEngine;

namespace Inventory
{
    [AddComponentMenu("Scripts/Inventory/InventoryItem/Inventory.InventoryItem.ItemImpl")]
    internal partial class ItemImpl : MonoBehaviour
    {
        [ReadOnly]
        [SerializeField]
        private string uid;
        public string Uid => uid;

        [SerializeField]
        private string itemName;
        public string ItemName => itemName;
    }
}
