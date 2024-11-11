using Configs.Progression;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace LevelSpecific.ForestEdge
{
    [AddComponentMenu(
        "Scripts/LevelSpecific/ForestEdge/LevelSpecific.ForestEdge.AltarMiniGameExit"
    )]
    internal class AltarMiniGameExit : MonoBehaviour
    {
        [SerializeField]
        [ReadOnly]
        private bool passedLoseWin = false;

        public UnityEvent PassedFirstTime;
        public UnityEvent PassedFirstTimeAfter;

        public void SetPassedLoseWin()
        {
            passedLoseWin = true;
        }

        public void SetMiniGamePassedCorrectly()
        {
            if (!ProgressionConfig.Instance.ForestEdgeLevel.AltarGamePassedCorrectly)
            {
                ProgressionConfig.Instance.ForestEdgeLevel.AltarGamePassedCorrectly = true;
                PassedFirstTime?.Invoke();
            }
        }

        public void CallMinigamePassed()
        {
            if (passedLoseWin)
            {
                ProgressionConfig.Instance.ForestEdgeLevel.AltarGamePassed = true;
                PassedFirstTimeAfter?.Invoke();
                passedLoseWin = false;
            }
        }
    }
}
