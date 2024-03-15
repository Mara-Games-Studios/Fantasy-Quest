using UnityEngine;
using UnityEngine.Events;

namespace LevelSpecific.ForestEdge
{
    [AddComponentMenu(
        "Scripts/LevelSpecific/ForestEdge/LevelSpecific.ForestEdge.MouseInHayMinigameGate"
    )]
    internal class MouseInHayMinigameGate : MonoBehaviour
    {
        public UnityEvent OnEnterSucceed;

        public void EnterMinigame()
        {
            OnEnterSucceed?.Invoke();
        }
    }
}
