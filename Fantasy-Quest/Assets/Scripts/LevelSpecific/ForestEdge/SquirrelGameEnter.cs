using Configs.Progression;
using Minigames.SquirrelGame;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LevelSpecific.ForestEdge
{
    [AddComponentMenu(
        "Scripts/LevelSpecific/ForestEdge/LevelSpecific.ForestEdge.SquirrelGameEnter"
    )]
    internal class SquirrelGameEnter : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private Manager miniGameManager;

        public void StartMiniGame()
        {
            if (
                ProgressionConfig.Instance.ForestEdgeLevel.MonsterCutsceneTriggered
                && !ProgressionConfig.Instance.ForestEdgeLevel.SquirrelGamePassed
            )
            {
                miniGameManager.EnableAllMinigameInput();
                miniGameManager.RefreshGame();
            }
        }
    }
}
