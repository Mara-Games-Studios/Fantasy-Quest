﻿using Configs.Progression;
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

        [Required]
        [SerializeField]
        private GameObject acorn;

        public void StartMiniGame()
        {
            if (
                ProgressionConfig.Instance.ForestEdgeLevel.MonsterCutsceneTriggered
                && !ProgressionConfig.Instance.ForestEdgeLevel.SquirrelGamePassed
            )
            {
                acorn.SetActive(true);
                miniGameManager.EnableAllMinigameInput();
                miniGameManager.RefreshGame();
            }
        }
    }
}
