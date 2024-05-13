using Configs;
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
        private bool exitManual = false;

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

        public void SetExitManual()
        {
            exitManual = true;
        }

        public void UnlockFromManualExit()
        {
            if (exitManual)
            {
                LockerSettings.Instance.UnlockAll();
                exitManual = false;
            }
        }
    }
}
