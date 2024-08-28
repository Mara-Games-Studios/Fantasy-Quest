using Configs;
using Configs.Progression;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using VContainer;

namespace LevelSpecific.ForestEdge
{
    [AddComponentMenu(
        "Scripts/LevelSpecific/ForestEdge/LevelSpecific.ForestEdge.ItemCollectorChecker"
    )]
    internal class ItemCollectorChecker : MonoBehaviour
    {
        [Inject]
        private LockerApi lockerSettings;

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
            lockerSettings.Api.UnlockAll();
            CheckForAllCollected();
        }

        public void TakeAcorn()
        {
            Destroy(acorn);
            lockerSettings.Api.UnlockAll();
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
