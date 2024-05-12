using Sirenix.OdinInspector;
using UnityEngine;

namespace Utils.BackgroundSupport
{
    [AddComponentMenu("Scripts/Utils/BackgroundSupport/Utils.BackgroundSupport.ButterflyFlipper")]
    internal class ButterflyFlipper : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private Transform targetTransform;

        [SerializeField]
        private Vector3 leftRotation;

        [SerializeField]
        private Vector3 rightRotation;

        private float previousX;

        private void Start()
        {
            previousX = targetTransform.position.x;
        }

        [Button]
        public void UpdateRotation()
        {
            if (previousX < targetTransform.position.x)
            {
                targetTransform.rotation = Quaternion.Euler(rightRotation);
            }
            else
            {
                targetTransform.rotation = Quaternion.Euler(leftRotation);
            }
            previousX = targetTransform.position.x;
        }
    }
}
