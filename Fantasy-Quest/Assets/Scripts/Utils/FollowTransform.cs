using UnityEngine;

namespace Utils
{
    [AddComponentMenu("Scripts/Utils/Utils.FollowTransform")]
    internal class FollowTransform : MonoBehaviour
    {
        [SerializeField]
        private Transform targetTransform;

        private void Update()
        {
            transform.position = targetTransform.position;
        }
    }
}
