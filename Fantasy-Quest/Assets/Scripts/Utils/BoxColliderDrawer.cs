using UnityEditor;
using UnityEngine;

namespace Utils
{
    [RequireComponent(typeof(BoxCollider2D))]
    [AddComponentMenu("Scripts/Utils/Utils.BoxColliderDrawer")]
    internal class BoxColliderDrawer : MonoBehaviour
    {
        [SerializeField]
        private BoxCollider2D boxCollider;

        [SerializeField]
        private Color color = Color.white;
#if UNITY_EDITOR

        private void OnValidate()
        {
            boxCollider = GetComponent<BoxCollider2D>();
        }

        private void OnDrawGizmos()
        {
            Color tempColor = Handles.color;
            Handles.color = color;
            Handles.DrawWireCube(
                transform.position
                    + Vector3.Scale((Vector3)boxCollider.offset, transform.lossyScale),
                Vector3.Scale(boxCollider.size, transform.lossyScale)
            );
            Handles.color = tempColor;
        }
#endif
    }
}
