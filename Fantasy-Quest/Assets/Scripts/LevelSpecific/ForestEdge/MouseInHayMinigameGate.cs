using Configs.Progression;
using Minigames.MouseInHay;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace LevelSpecific.ForestEdge
{
    [AddComponentMenu(
        "Scripts/LevelSpecific/ForestEdge/LevelSpecific.ForestEdge.MouseInHayMinigameGate"
    )]
    internal class MouseInHayMinigameGate : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private Manager miniGameManager;

        public UnityEvent EnterWithStartGame;

        public void ActivateMiniGame()
        {
            if (
                ProgressionConfig.Instance.ForestEdgeLevel.ExplanationListened
                && !ProgressionConfig.Instance.ForestEdgeLevel.MouseInHayGamePassed
            )
            {
                miniGameManager.EnableAllMinigameInput();
                miniGameManager.RefreshGame();
                miniGameManager.StartGame();
                EnterWithStartGame?.Invoke();
            }
            else
            {
                miniGameManager.EnableAllMinigameInput();
                miniGameManager.RefreshGame();
                miniGameManager.StopGame();
            }
        }
    }
}
