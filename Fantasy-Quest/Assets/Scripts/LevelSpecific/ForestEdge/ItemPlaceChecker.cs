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

        private bool firstItemPlaced = false;
        public UnityEvent EggTaken;
        public UnityEvent AcornTaken;
        public UnityEvent FirstTimePlaced;
        public UnityEvent SecondTimePlaced;

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
                    if (!firstItemPlaced)
                    {
                        firstItemPlaced = true;
                        FirstTimePlaced?.Invoke();
                    }
                    else
                    {
                        SecondTimePlaced?.Invoke();
                    }
                }
                else if (itemTaker.TakenItem.UidEquals(acorn))
                {
                    ProgressionConfig.Instance.ForestEdgeLevel.AcornTakenByCymon = true;
                    AcornTaken.Invoke();
                    if (!firstItemPlaced)
                    {
                        firstItemPlaced = true;
                        FirstTimePlaced?.Invoke();
                    }
                    else
                    {
                        SecondTimePlaced?.Invoke();
                    }
                }
                else
                {
                    Debug.LogError("Unknown Item");
                    return;
                }
            }
            else
            {
                Debug.Log("Cannot place on ground");
            }
        }
    }
}
