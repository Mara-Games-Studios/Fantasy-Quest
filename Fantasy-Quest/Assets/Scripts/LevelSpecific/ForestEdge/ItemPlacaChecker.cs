using System.Collections;
using Configs;
using Configs.Progression;
using Dialogue;
using Inventory;
using Sirenix.OdinInspector;
using UnityEngine;

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
        private ChainSpeaker eggTakenSpeech;

        [Required]
        [SerializeField]
        private ChainSpeaker acornTakenSpeech;

        public void ItemPlaceCheck(Item item)
        {
            if (rightPlaceZone.OverlapPoint(item.transform.position))
            {
                if (item.UidEquals(egg))
                {
                    // TODO: Trigger mini cutscene
                    ProgressionConfig.Instance.ForestEdgeLevel.EggTakenByCymon = true;
                    Destroy(item.gameObject);
                    _ = StartCoroutine(EggTaken());
                }

                if (item.UidEquals(acorn))
                {
                    // TODO: Trigger mini cutscene
                    ProgressionConfig.Instance.ForestEdgeLevel.AcornTakenByCymon = true;
                    Destroy(item.gameObject);
                    _ = StartCoroutine(AcornTaken());
                }
            }
        }

        private IEnumerator EggTaken()
        {
            LockerSettings.Instance.LockAll();
            yield return eggTakenSpeech.Tell();
            LockerSettings.Instance.UnlockAll();
        }

        private IEnumerator AcornTaken()
        {
            LockerSettings.Instance.LockAll();
            yield return acornTakenSpeech.Tell();
            LockerSettings.Instance.UnlockAll();
        }
    }
}
