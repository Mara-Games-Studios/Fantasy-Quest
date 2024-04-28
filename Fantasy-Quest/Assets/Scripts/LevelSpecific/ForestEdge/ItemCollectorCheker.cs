using Configs;
using Configs.Progression;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace LevelSpecific.ForestEdge
{
    [AddComponentMenu(
        "Scripts/LevelSpecific/ForestEdge/LevelSpecific.ForestEdge.ItemCollectorChecker"
    )]
    internal class ItemCollectorChecker : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private GameObject egg;

        [Required]
        [SerializeField]
        private GameObject acorn;

        public UnityEvent AllTaken;

        public void TakeEgg()
        {
            Destroy(egg);
            LockerSettings.Instance.UnlockAll();
            CheckForAllCollected();
        }

        public void TakeAcorn()
        {
            Destroy(acorn);
            LockerSettings.Instance.UnlockAll();
            CheckForAllCollected();
        }

        public void CheckForAllCollected()
        {
            if (
                ProgressionConfig.Instance.ForestEdgeLevel.EggTakenByCymon
                && ProgressionConfig.Instance.ForestEdgeLevel.AcornTakenByCymon
            )
            {
                AllTaken.Invoke();
            }
        }
    }
}
