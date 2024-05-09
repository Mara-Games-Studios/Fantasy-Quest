using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Utils
{
    [AddComponentMenu("Scripts/Utils/Utils.PointDrawer")]
    internal class PointDrawer : MonoBehaviour
    {
        [SerializeField]
        private float radius = 0.5f;

        [SerializeField]
        private Color color = Color.white;

#if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            Color tempColor = Handles.color;
            Handles.color = color;
            Handles.DrawSolidDisc(transform.position, Vector3.forward, radius);
            Handles.color = tempColor;
        }
#endif
    }
}
