﻿using Dialogue;
using Minigames.AltarGame.Hand;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Minigames.AltarGame
{
    [AddComponentMenu("Scripts/Minigames/AltarGame/Minigames.AltarGame.Manager")]
    internal class Manager : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private ChainSpeaker winSpeech;

        [Required]
        [SerializeField]
        private HandImpl hand;

        [Required]
        [SerializeField]
        private Hand.Input handInput;

        public UnityEvent OnGameFinishedWin;
        public UnityEvent OnGameFinishedLose;

        [Button]
        public void StartMiniGame()
        {
            hand.TakeItem();
        }

        [Button]
        public void RefreshMiniGame() { }

        [Button]
        public void EnableAllMinigameInput()
        {
            handInput.Enable();
        }

        [Button]
        public void DisableAllMinigameInput()
        {
            handInput.Disable();
        }

        public void QuitMiniGame()
        {
            OnGameFinishedLose?.Invoke();
        }

        public void TellWinAndQuit()
        {
            winSpeech.Tell(() => OnGameFinishedWin?.Invoke());
        }
    }
}
