using Sirenix.OdinInspector;
using Spine.Unity;
using UnityEngine;

namespace Utils
{
    [AddComponentMenu("Scripts/Utils/Utils.SkeletonFlipper")]
    internal class SkeletonFlipper : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private SkeletonAnimation skeletonAnimation;

        [Button]
        public void Flip()
        {
            skeletonAnimation.skeleton.ScaleX *= -1;
        }
    }
}
