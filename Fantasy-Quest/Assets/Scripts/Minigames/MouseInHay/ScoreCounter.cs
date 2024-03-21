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
        private int neededScore = 3;
        public bool IsWinGame => score == neededScore;

        [SerializeField]
        private TMP_Text scoreLabel;

        public void ResetScore()
        {
            score = 0;
            scoreLabel.text = score.ToString();
        }

        public void AddPoint()
        {
            score++;
            scoreLabel.text = score.ToString();
        }
    }
}
