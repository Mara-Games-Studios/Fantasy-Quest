using UnityEngine;

namespace Utils
{
    [AddComponentMenu("Scripts/Utils/Utils.DirectionDetector")]
    internal class DirectionDetector : MonoBehaviour
    {
        [SerializeField]
        private Transform from;

        [SerializeField]
        private Transform to;

        [SerializeField]
        private TurnTransform turnLeft;

        [SerializeField]
        private TurnTransform turnRight;

        public void TurnInDirection()
        {
            float xDir = to.position.x - from.position.x;

            if (xDir > 0)
            {
                turnLeft.Turner();
            }
            else if (xDir < 0)
            {
                turnRight.Turner();
            }
        }
    }
}
