using System.Collections.Generic;
using Dialogue;
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

        [Required]
        [SerializeField]
        private Altar altar;

        [SerializeField]
        private List<GameObject> miniGameUI = new();

        public UnityEvent OnGameFinishedWin;
        public UnityEvent OnGameFinishedLose;
        public UnityEvent OnGameFinishedManual;

        [Button]
        public void StartMiniGame()
        {
            hand.TakeItem();
        }

        [Button]
        public void RefreshMiniGame()
        {
            altar.ResetAltar();
            hand.ResetHand();
        }

        [Button]
        public void ShowMiniGameUI()
        {
            foreach (GameObject elementUI in miniGameUI)
            {
                elementUI.SetActive(true);
            }
        }

        [Button]
        public void HideMiniGameUI()
        {
            foreach (GameObject elementUI in miniGameUI)
            {
                elementUI.SetActive(false);
            }
        }

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

        public void QuitMiniGameManual()
        {
            OnGameFinishedManual?.Invoke();
        }

        public void TellWinAndQuit()
        {
            winSpeech.Tell(() => OnGameFinishedWin?.Invoke());
        }
    }
}
