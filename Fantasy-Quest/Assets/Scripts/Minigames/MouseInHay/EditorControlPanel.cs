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
        private Paw paw;

        [SerializeField]
        private TMP_Text showedLabel;

        [SerializeField]
        private TMP_Text scoreLabel;

        public void SetPawMoveTo(string value)
        {
            paw.MoveToHoleTime = float.Parse(value);
        }

        public void SetPawMoveFrom(string value)
        {
            paw.MoveFromHoleTime = float.Parse(value);
        }

        public void SetMouseShowTime(string value)
        {
            hay.MouseShowTime = new() { Max = float.Parse(value), Min = float.Parse(value) };
        }

        public void SetMouseHideTime(string value)
        {
            hay.NoMouseTime = float.Parse(value);
        }

        public void SetMaxMouses(string value)
        {
            hay.MaxMousesToShow = int.Parse(value);
        }

        public void SetMousesToWin(string value)
        {
            scoreCounter.NeededScore = int.Parse(value);
        }

        private void Update()
        {
            showedLabel.text = hay.MousesShowed.ToString() + " mouses showed";
            scoreLabel.text = scoreCounter.Score.ToString() + " score";
        }
    }
}
