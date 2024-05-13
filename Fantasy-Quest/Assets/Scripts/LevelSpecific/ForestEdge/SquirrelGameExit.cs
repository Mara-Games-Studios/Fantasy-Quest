using Configs.Progression;
using UnityEngine;
using UnityEngine.Events;

namespace LevelSpecific.ForestEdge
{
    [AddComponentMenu("Scripts/LevelSpecific/ForestEdge/LevelSpecific.ForestEdge.SquirrelGameExit")]
    internal class SquirrelGameExit : MonoBehaviour
    {
        public UnityEvent PassedFirstTime;
        public UnityEvent PassedFirstTimeAfterFade;
        private bool first = true;

        public void SetMiniGamePassed()
        {
            if (!ProgressionConfig.Instance.ForestEdgeLevel.SquirrelGamePassed)
            {
                ProgressionConfig.Instance.ForestEdgeLevel.SquirrelGamePassed = true;
                PassedFirstTime?.Invoke();
            }
        }

        public void TriggerPassedFirstTimeAfterFade()
        {
            if (ProgressionConfig.Instance.ForestEdgeLevel.SquirrelGamePassed && first)
            {
                PassedFirstTimeAfterFade?.Invoke();
                first = false;
            }
        }
    }
}
