using Sirenix.OdinInspector;
using Spine.Unity;
using UnityEngine;

namespace Utils
{
    [AddComponentMenu("Scripts/Utils/Utils.SkeletonRotationSetter")]
    internal class SkeletonRotationSetter : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private SkeletonAnimation skeletonAnimation;

        [SerializeField]
        private Vector3 rotation;

        [Button]
        public void SetRotation()
        {
            skeletonAnimation.transform.rotation = Quaternion.Euler(rotation);
        }
    }
}
