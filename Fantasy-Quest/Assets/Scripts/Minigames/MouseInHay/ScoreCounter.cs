using Common;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Minigames.MouseInHay
{
    [AddComponentMenu("Scripts/Minigames/MouseInHay/Minigames.MouseInHay.ScoreCounter")]
    internal class ScoreCounter : MonoBehaviour
    {
        [ReadOnly]
        [SerializeField]
        private int score = 0;

        [SerializeField]
        private TMP_Text scoreLabel;

        [SerializeField]
        private int neededScore = 3;

        [SerializeField]
        private Transition.End.Controller endController;

        [Scene]
        [SerializeField]
        private string nextScene;

        public void AddPoint()
        {
            score++;
            if (score == neededScore)
            {
                WinGame();
            }
        }

        private void Update()
        {
            scoreLabel.text = score.ToString();
        }

        private void WinGame()
        {
            endController.LoadScene(nextScene, 1f);
        }
    }
}
