using Common.DI;
using Configs;
using Inventory;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace LevelSpecific.ForestEdge
{
    [AddComponentMenu("Scripts/LevelSpecific/ForestEdge/LevelSpecific.ForestEdge.WithItemUnlocker")]
    internal class WithItemUnlocker : InjectingMonoBehaviour
    {
        [Inject]
        private LockerApi lockerSettings;

        [SerializeField]
        private ItemTaker itemTaker;

        [Button]
        public void LockIfHasItem()
        {
            if (itemTaker.TakenItem != null)
            {
                lockerSettings.Api.LockForCarryingItem();
            }
        }
    }
}
