using Configs.Progression;
using UnityEngine;
using UnityEngine.Events;

namespace LevelSpecific.ForestEdge
{
    [AddComponentMenu(
        "Scripts/LevelSpecific/ForestEdge/LevelSpecific.ForestEdge.AltarMiniGameExit"
    )]
    internal class AltarMiniGameExit : MonoBehaviour
    {
        public UnityEvent PassedFirstTime;

        public void SetMiniGamePassed()
        {
            if (!ProgressionConfig.Instance.ForestEdgeLevel.AltarGamePassedCorrectly)
            {
                ProgressionConfig.Instance.ForestEdgeLevel.AltarGamePassedCorrectly = true;
                PassedFirstTime?.Invoke();
            }
        }
    }
}
