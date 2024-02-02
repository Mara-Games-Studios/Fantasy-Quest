#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Utils
{
    [RequireComponent(typeof(BoxCollider2D))]
    [AddComponentMenu("Scripts/Scripts/Utils/Utils.BoxColliderDrawer")]
    internal class BoxColliderDrawer : MonoBehaviour
    {
        [SerializeField]
        private BoxCollider2D boxCollider;

        [SerializeField]
        private Color color = Color.white;

        private void OnValidate()
        {
            boxCollider = GetComponent<BoxCollider2D>();
        }

        private void OnDrawGizmos()
        {
            Color tempColor = Handles.color;
            Handles.color = color;
            Vector3 size = transform.position + (Vector3)boxCollider.offset;
            size.Scale(transform.lossyScale);
            Handles.DrawWireCube(size, boxCollider.size);
            Handles.color = tempColor;
        }
    }
}
#endif
