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
            Handles.DrawWireCube(
                transform.position
                    + Vector3.Scale((Vector3)boxCollider.offset, transform.localScale),
                Vector3.Scale(boxCollider.size, transform.localScale)
            );
            Handles.color = tempColor;
        }
    }
}
#endif
