using Sirenix.OdinInspector;
using UnityEngine;

namespace Minigames.AltarGame
{
    [AddComponentMenu("Scripts/Minigames/AltarGame/Minigames.AltarGame.Slot")]
    internal class Slot : MonoBehaviour
    {
        [AssetsOnly]
        [SerializeField]
        private Item neededItem;

        [ReadOnly]
        [SerializeField]
        private Item placedItem;
        public bool IsItemPlaced => placedItem != null;

        public void FreeSlot()
        {
            if (placedItem != null)
            {
                Destroy(placedItem.gameObject);
                placedItem = null;
            }
        }

        public void PlaceItem(Item item)
        {
            placedItem = item;
            item.transform.parent = transform;
        }

        public bool IsPlacedCorrectItem()
        {
            if (placedItem == null)
            {
                return false;
            }

            if (neededItem.Compare(placedItem))
            {
                return true;
            }

            return false;
        }
    }
}
