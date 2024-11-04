using UnityEngine;

namespace LevelSpecific.ForestEdge
{
    [AddComponentMenu("Scripts/LevelSpecific/ForestEdge/LevelSpecific.ForestEdge.CloudMover")]
    internal class CloudMover : MonoBehaviour
    {
        [SerializeField]
        private float speed;

        [SerializeField]
        private float startX;

        [SerializeField]
        private float finishX;

        private void Update()
        {
            transform.localPosition += speed * Time.deltaTime * Vector3.right;
            if (transform.localPosition.x >= finishX)
            {
                transform.localPosition = new Vector3(
                    startX,
                    transform.localPosition.y,
                    transform.localPosition.z
                );
            }
        }
    }
}
