using Configs.Progression;
using UnityEngine;
using UnityEngine.Events;

namespace LevelSpecific.ForestEdge
{
    [AddComponentMenu(
        "Scripts/LevelSpecific/ForestEdge/LevelSpecific.ForestEdge.MouseInHayMinigameExit"
    )]
    internal class MouseInHayMinigameExit : MonoBehaviour
    {
        public UnityEvent PassedFirstTime;

        public void SetMiniGamePassed()
        {
            if (!ProgressionConfig.Instance.ForestEdgeLevel.MouseInHayGamePassed)
            {
                ProgressionConfig.Instance.ForestEdgeLevel.MouseInHayGamePassed = true;
                PassedFirstTime?.Invoke();
            }
        }
    }
}
