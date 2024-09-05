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

        [Required]
        [SerializeField]
        private QuitInput quitInput;

        public void StartMiniGame()
        {
            if (
                ProgressionConfig.Instance.ForestEdgeLevel.EggTakenByCymon
                && ProgressionConfig.Instance.ForestEdgeLevel.AcornTakenByCymon
                && !ProgressionConfig.Instance.ForestEdgeLevel.AltarGamePassedCorrectly
            )
            {
                miniGameManager.ShowMiniGameUI();
                miniGameManager.EnableAllMinigameInput();
                miniGameManager.RefreshMiniGame();
                miniGameManager.StartMiniGame();
            }
            quitInput.enabled = true;
        }
    }
}
