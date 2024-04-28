using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Camera
{
    [AddComponentMenu("Scripts/Camera/Camera.FollowTarget")]
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField]
        private Transform playerTransform;

        [SerializeField]
        private float flipTime = 0.5f;

        [SerializeField]
        private Vector3 shift;

        private bool isRight;

        private void Awake()
        {
            isRight = false;
        }

        private void Update()
        {
            transform.position = playerTransform.position + shift;
        }

        public void CallTurn()
        {
            Vector3 vector = transform.rotation.eulerAngles;
            vector.y = RotateTransform();
            _ = transform.DORotate(vector, flipTime);
        }

        private float RotateTransform()
        {
            isRight = !isRight;
            return isRight ? 180.0f : 0f;
        }

        [Button]
        public void SetPos()
        {
            transform.position = playerTransform.position + shift;
        }
    }
}
