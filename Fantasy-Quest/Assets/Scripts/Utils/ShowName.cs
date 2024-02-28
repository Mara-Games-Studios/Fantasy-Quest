using UnityEditor;
using UnityEngine;

namespace Utils
{
    [AddComponentMenu("Scripts/Utils/Utils.ShowName")]
    internal class ShowName : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            GUIStyle style = new() { fontSize = 16 };
            style.normal.textColor = Color.red;
            Handles.Label(transform.position, gameObject.name, style);
        }
    }
}
