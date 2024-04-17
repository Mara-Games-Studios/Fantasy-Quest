using Configs.Progression;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace LevelSpecific.ForestEdge
{
    [AddComponentMenu(
        "Scripts/LevelSpecific/ForestEdge/LevelSpecific.ForestEdge.MouseInHayMinigameExit"
    )]
    internal class MouseInHayMinigameExit : MonoBehaviour
    {
        [SerializeField]
        [ReadOnly]
        private bool cutsceneTriggered = false;

        public UnityEvent PassedFirstTime;
        public UnityEvent PassedFirstTimeAfter;

        public void SetMiniGamePassed()
        {
            if (!ProgressionConfig.Instance.ForestEdgeLevel.MouseInHayGamePassed)
            {
                ProgressionConfig.Instance.ForestEdgeLevel.MouseInHayGamePassed = true;
                PassedFirstTime?.Invoke();
            }
        }

        public void CallMinigamePassed()
        {
            if (
                ProgressionConfig.Instance.ForestEdgeLevel.MouseInHayGamePassed
                && !cutsceneTriggered
            )
            {
                PassedFirstTimeAfter?.Invoke();
                cutsceneTriggered = true;
            }
        }
    }
}
