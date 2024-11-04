using UnityEngine;

namespace Common
{
    [AddComponentMenu("Scripts/Common/Common.TransformXScaleChanger")]
    internal class TransformXScaleChanger : MonoBehaviour
    {
        [SerializeField]
        private Transform targetTransform;

        public void SetXScale(float value)
        {
            Vector3 ls = targetTransform.localScale;
            targetTransform.localScale = new Vector3(value, ls.y, ls.z);
        }
    }
}
