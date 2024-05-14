using Configs.Progression;
using UnityEngine;
using UnityEngine.Events;

namespace LevelSpecific.ForestEdge
{
    [AddComponentMenu("Scripts/LevelSpecific/ForestEdge/LevelSpecific.ForestEdge.CheckForItems")]
    internal class CheckForItems : MonoBehaviour
    {
        public UnityEvent AllItemsTaken;

        public void CheckForItem()
        {
            if (
                ProgressionConfig.Instance.ForestEdgeLevel.EggTakenByCymon
                && ProgressionConfig.Instance.ForestEdgeLevel.AcornTakenByCymon
            )
            {
                AllItemsTaken.Invoke();
            }
        }
    }
}
