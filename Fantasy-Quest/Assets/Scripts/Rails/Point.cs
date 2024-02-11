using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Rails
{
    [RequireComponent(typeof(RailsImpl))]
    [AddComponentMenu("Scripts/Rails/Rails.Point")]
    internal class Point : MonoBehaviour
    {
        [Title("Main info")]
        [ReadOnly]
        [SerializeField]
        private RailsImpl rails;
        public RailsImpl Rails => rails;

        [Range(0f, 1f)]
        [SerializeField]
        private float value;
        public float Value => value;

        [Title("Cosmetic")]
        [MinValue(0)]
        [SerializeField]
        private float step = 1;

        [SerializeField]
        private string pointName = "Point";

        [SerializeField]
        private Color color = Color.black;

        [SerializeField]
        private float radius = 0.2f;

        private void OnValidate()
        {
            rails = GetComponent<RailsImpl>();
        }

        [Button]
        public void MoveRight()
        {
            value = Mathf.Clamp01(value + (step / rails.Curve.length));
        }

        [Button]
        public void MoveLeft()
        {
            value = Mathf.Clamp01(value - (step / rails.Curve.length));
        }

        private void OnDrawGizmos()
        {
            if (rails == null)
            {
                return;
            }

            Color tempColor = Handles.color;
            Handles.color = color;
            Vector3 center = transform.position + rails.Curve.GetPointAt(value);
            Handles.DrawSolidDisc(center, Vector3.forward, radius);
            Handles.Label(center + (0.8f * radius * Vector3.left), pointName);
            Handles.color = tempColor;
        }
    }
}
