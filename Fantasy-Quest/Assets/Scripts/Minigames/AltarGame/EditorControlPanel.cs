using Minigames.AltarGame.Hand;
using UnityEngine;

namespace Minigames.AltarGame
{
    [AddComponentMenu("Scripts/Minigames/AltarGame/Minigames.AltarGame.EditorControlPanel")]
    internal class EditorControlPanel : MonoBehaviour
    {
        [SerializeField]
        private HandImpl hand;

        public void SetToTakeItemPlace(string value)
        {
            hand.TakeItemPoint.Duration = float.Parse(value);
        }

        public void SetToEndGamePointTime(string value)
        {
            hand.EndGamePoint.Duration = float.Parse(value);
        }

        public void SetToNextSlotTime(string value)
        {
            hand.MoveToSlotDuration = float.Parse(value);
        }

        public void SetDecideWaitingTime(string value)
        {
            hand.DecideWaitingTime = float.Parse(value);
        }
    }
}
