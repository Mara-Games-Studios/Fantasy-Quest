using Common;
using Dialogue;
using UnityEngine;

namespace Minigames.AltarGame
{
    [AddComponentMenu("Scripts/Minigames/AltarGame/Minigames.AltarGame.Manager")]
    internal class Manager : MonoBehaviour
    {
        [SerializeField]
        private Transition.End.Controller endController;

        [Scene]
        [SerializeField]
        private string nextScene;

        [SerializeField]
        private ChainSpeaker winSpeech;

        public void QuitMiniGame()
        {
            endController.LoadScene(nextScene);
        }

        public void TellWinAndQuit()
        {
            winSpeech.Tell(() => QuitMiniGame());
        }
    }
}
