using Sirenix.OdinInspector;
using Spine.Unity;
using UnityEngine;

namespace Utils
{
    [AddComponentMenu("Scripts/Utils/Utils.TurnTransform")]
    internal class TurnTransform : MonoBehaviour
    {
        public enum Direction
        {
            right,
            left
        }

        [SerializeField]
        private bool toTarget = false;

        [Required]
        [SerializeField]
        private Transform target;

        [ShowIf("toTarget")]
        [SerializeField]
        private Transform to;

        [HideIf("toTarget")]
        [SerializeField]
        private Direction direction;

        [SerializeField]
        private bool useScale = false;

        [Button]
        public void Turner()
        {
            if (!toTarget)
            {
                Turn(direction);
            }
            else
            {
                float x = target.position.x - to.position.x;
                direction = x > 0 ? Direction.left : Direction.right;
                Turn(direction);
            }
        }

        private void Turn(Direction direction)
        {
            if (useScale)
            {
                target.GetComponent<SkeletonAnimation>().skeleton.ScaleX = 1f;
            }

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
