using Configs;
using Inventory;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LevelSpecific.ForestEdge
{
    [AddComponentMenu("Scripts/LevelSpecific/ForestEdge/LevelSpecific.ForestEdge.WithItemUnlocker")]
    internal class WithItemUnlocker : MonoBehaviour
    {
        [SerializeField]
        private ItemTaker itemTaker;

        [Button]
        public void LockIfHasItem()
        {
            if (itemTaker.TakenItem != null)
            {
                LockerSettings.Instance.LockForCarryingItem();
            }
        }
    }
}
