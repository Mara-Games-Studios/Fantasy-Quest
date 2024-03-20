using Configs.Progression;
using Minigames.AltarGame;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LevelSpecific.ForestEdge
{
    [AddComponentMenu(
        "Scripts/LevelSpecific/ForestEdge/LevelSpecific.ForestEdge.AltarMinigameEnter"
    )]
    internal class AltarMinigameEnter : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private Manager miniGameManager;

        public void StartMiniGame()
        {
            if (
                ProgressionConfig.Instance.ForestEdgeLevel.EggTakenByCymon
                && ProgressionConfig.Instance.ForestEdgeLevel.AcornTakenByCymon
                && !ProgressionConfig.Instance.ForestEdgeLevel.AltarGamePassedCorrectly
            )
            {
                miniGameManager.EnableAllMinigameInput();
                miniGameManager.RefreshMiniGame();
            }
        }
    }
}
