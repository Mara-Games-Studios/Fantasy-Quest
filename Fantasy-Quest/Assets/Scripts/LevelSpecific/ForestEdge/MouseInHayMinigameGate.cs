using Configs.Progression;
using Minigames.MouseInHay;
using Sirenix.OdinInspector;
using UnityEngine;

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

        public void ActivateMiniGame()
        {
            if (
                ProgressionConfig.Instance.ForestEdgeLevel.BagTaken
                && !ProgressionConfig.Instance.ForestEdgeLevel.MouseInHayGamePassed
            )
            {
                miniGameManager.EnableAllMinigameInput();
                miniGameManager.RefreshGame();
                miniGameManager.StartGame();
            }
            else
            {
                miniGameManager.RefreshGame();
                miniGameManager.StopGame();
            }
        }
    }
}
