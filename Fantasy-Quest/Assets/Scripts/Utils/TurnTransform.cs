using Sirenix.OdinInspector;
using UnityEngine;

namespace Utils
{
    [AddComponentMenu("Scripts/Utils/Utils.TurnTransform")]
    internal class TurnTransform : MonoBehaviour
    {
        internal enum Direction
        {
            right,
            left
        }

        [SerializeField]
        private Transform target;

        [SerializeField]
        private Direction direction;

        [Button]
        public void Turner()
        {
            Turn(direction);
        }

        private void Turn(Direction direction)
        {
            switch (direction)
            {
                case Direction.right:
                    target.rotation = Quaternion.Euler(0f, 0f, 0f);
                    break;
                case Direction.left:
                    target.rotation = Quaternion.Euler(0f, 180f, 0f);
                    break;
                default:
                    break;
            }
        }
    }
}
