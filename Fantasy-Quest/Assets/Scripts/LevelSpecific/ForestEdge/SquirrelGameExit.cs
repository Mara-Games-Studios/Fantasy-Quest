using Configs.Progression;
using UnityEngine;

namespace LevelSpecific.ForestEdge
{
    [AddComponentMenu("Scripts/LevelSpecific/ForestEdge/LevelSpecific.ForestEdge.SquirrelGameExit")]
    internal class SquirrelGameExit : MonoBehaviour
    {
        public void SetMiniGamePassed()
        {
            ProgressionConfig.Instance.ForestEdgeLevel.SquirrelGamePassed = false;
        }
    }
}
