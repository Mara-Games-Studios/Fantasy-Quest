using Configs.Progression;
using UnityEngine;

namespace LevelSpecific.ForestEdge
{
    [AddComponentMenu(
        "Scripts/LevelSpecific/ForestEdge/LevelSpecific.ForestEdge.MouseInHayMinigameExit"
    )]
    internal class MouseInHayMinigameExit : MonoBehaviour
    {
        public void SetMiniGamePassed()
        {
            ProgressionConfig.Instance.ForestEdgeLevel.MouseInHayGamePassed = true;
        }
    }
}
