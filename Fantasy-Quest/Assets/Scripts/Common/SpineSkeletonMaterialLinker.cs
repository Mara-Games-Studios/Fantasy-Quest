using Sirenix.OdinInspector;
using UnityEngine;

namespace Common
{
    [AddComponentMenu("Scripts/Common/Common.SpineSkeletonMaterialLinker")]
    internal class SpineSkeletonMaterialLinker : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private Material material;
        public Material Material => material;
    }
}
