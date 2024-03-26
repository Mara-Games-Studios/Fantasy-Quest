using TMPro;
using UnityEngine;

namespace Minigames.MouseInHay
{
    [AddComponentMenu("Scripts/Minigames/MouseInHay/Minigames.MouseInHay.EditorControlPanel")]
    internal class EditorControlPanel : MonoBehaviour
    {
        [SerializeField]
        private Hay hay;

        [SerializeField]
        private ScoreCounter scoreCounter;

        [SerializeField]
        private TMP_Text scoreLabel;

        private void Update()
        {
            scoreLabel.text = scoreCounter.Score.ToString() + "/" + hay.AllMousesCount;
        }
    }
}
