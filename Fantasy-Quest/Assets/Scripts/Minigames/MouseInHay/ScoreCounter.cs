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
        public int Score => score;

        [SerializeField]
        private int neededScore = 3;
        public bool IsWinGame => score == NeededScore;

        [Required]
        [SerializeField]
        private TMP_Text label;

        private void Update()
        {
            label.text = (neededScore - score).ToString() + " X";
        }

        public int NeededScore
        {
            get => neededScore;
            set => neededScore = value;
        }

        public void ResetScore()
        {
            score = 0;
        }

        public void AddPoint()
        {
            score++;
        }
    }
}
