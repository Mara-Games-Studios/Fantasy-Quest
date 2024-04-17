using Configs.Progression;
using UnityEngine;
using UnityEngine.Events;

namespace LevelSpecific.ForestEdge
{
    [AddComponentMenu("Scripts/LevelSpecific/ForestEdge/LevelSpecific.ForestEdge.EndChecker")]
    internal class EndChecker : MonoBehaviour
    {
        public UnityEvent AltarPassedCorrectly;
        public UnityEvent AltarPassedIncorrectly;

        public void CheckForProgression()
        {
            if (ProgressionConfig.Instance.ForestEdgeLevel.AltarGamePassedCorrectly)
            {
                AltarPassedCorrectly?.Invoke();
            }
            else
            {
                AltarPassedIncorrectly?.Invoke();
            }
        }
    }
}
