using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Minigames.MouseInHay
{
    [AddComponentMenu("Scripts/Minigames/MouseInHay/Minigames.MouseInHay.EditorControlPanel")]
    internal class EditorControlPanel : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private Hay hay;

        [Required]
        [SerializeField]
        private ScoreCounter scoreCounter;

        [Required]
        [SerializeField]
        private TMP_Text scoreLabel;

        private void Update()
        {
            scoreLabel.text =
                scoreCounter.Score + "/" + scoreCounter.NeededScore + "/" + hay.AllMousesCount;
        }
    }
}
