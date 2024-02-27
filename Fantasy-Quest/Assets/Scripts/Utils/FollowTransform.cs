using UnityEngine;

namespace Utils
{
    [AddComponentMenu("Scripts/Utils/Utils.FollowTransform")]
    internal class FollowTransform : MonoBehaviour
    {
        [SerializeField]
        private Transform targetTransform;

        [SerializeField]
        //private float speed = 0.2f;

        private void Update()
        {
            transform.position = targetTransform.position;
            //Vector3.Lerp(transform.position, targetTransform.position, speed);
        }
    }
}
