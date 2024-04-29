using Configs.Progression;
using Inventory;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace LevelSpecific.ForestEdge
{
    [AddComponentMenu("Scripts/LevelSpecific/ForestEdge/LevelSpecific.ForestEdge.ItemPlaceChecker")]
    internal class ItemPlaceChecker : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private Collider2D rightPlaceZone;

        [Required]
        [SerializeField]
        private Item egg;

        [Required]
        [SerializeField]
        private Item acorn;

        [Required]
        [SerializeField]
        private ItemTaker itemTaker;

        public UnityEvent EggTaken;
        public UnityEvent AcornTaken;

        [Button]
        public void SpecificPlaceItem()
        {
            if (itemTaker.TakenItem == null)
            {
                return;
            }

            if (rightPlaceZone.OverlapPoint(itemTaker.TakenItem.transform.position))
            {
                if (itemTaker.TakenItem.UidEquals(egg))
                {
                    ProgressionConfig.Instance.ForestEdgeLevel.EggTakenByCymon = true;
                    EggTaken.Invoke();
                }

                if (itemTaker.TakenItem.UidEquals(acorn))
                {
                    ProgressionConfig.Instance.ForestEdgeLevel.AcornTakenByCymon = true;
                    AcornTaken.Invoke();
                }
            }
            else
            {
                // TODO: make cutscene
                itemTaker.PlaceItem();
            }
        }
    }
}
