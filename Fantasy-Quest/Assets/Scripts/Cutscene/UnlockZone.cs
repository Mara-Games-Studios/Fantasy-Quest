using UnityEngine;

namespace Cutscene
{
    [AddComponentMenu("Scripts/Cutscene/Cutscene.UnlockZone")]
    internal class UnlockZone : MonoBehaviour
    {
        [SerializeField]
        private double startTime;

        [SerializeField]
        private double endTime;

        public bool IsInRange(double value)
        {
            return value <= endTime && value >= startTime;
        }
    }
}
