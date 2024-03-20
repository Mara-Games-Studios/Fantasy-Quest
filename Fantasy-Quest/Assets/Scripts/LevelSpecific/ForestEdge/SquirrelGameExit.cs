using Configs.Progression;
using UnityEngine;
using UnityEngine.Events;

namespace LevelSpecific.ForestEdge
{
    [AddComponentMenu("Scripts/LevelSpecific/ForestEdge/LevelSpecific.ForestEdge.SquirrelGameExit")]
    internal class SquirrelGameExit : MonoBehaviour
    {
        public UnityEvent PassedFirstTime;

        public void SetMiniGamePassed()
        {
            if (!ProgressionConfig.Instance.ForestEdgeLevel.SquirrelGamePassed)
            {
                ProgressionConfig.Instance.ForestEdgeLevel.SquirrelGamePassed = true;
                PassedFirstTime?.Invoke();
            }
        }
    }
}
