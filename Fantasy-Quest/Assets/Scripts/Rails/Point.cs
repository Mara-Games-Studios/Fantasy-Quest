﻿using Sirenix.OdinInspector;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Rails
{
    [AddComponentMenu("Scripts/Rails/Rails.Point")]
    internal class Point : MonoBehaviour
    {
        [Title("Main info")]
        [Required]
        [SerializeField]
        private RailsImpl rails;
        public RailsImpl Rails => rails;

        [Range(0f, 1f)]
        [SerializeField]
        private float value;
        public float Value => value;

        [Title("Cosmetic")]
        [SerializeField]
        private bool showPoint = true;

        [MinValue(0)]
        [SerializeField]
        private float step = 1;

        [SerializeField]
        private Color color = Color.black;

        [SerializeField]
        private float radius = 0.2f;

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

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (!showPoint)
            {
                return;
            }

            if (rails == null)
            {
                Debug.LogError($"Null rails, Please assign instance to {name}", gameObject);
                return;
            }

            Color tempColor = Handles.color;
            Handles.color = color;
            Vector3 center = transform.position + rails.Curve.GetPointAt(value);
            Handles.DrawSolidDisc(center, Vector3.forward, radius);
            GUIStyle style = new() { fontSize = 12 };
            style.normal.textColor = Color.white;
            Handles.Label(center + (0.8f * radius * Vector3.left), gameObject.name, style);
            Handles.color = tempColor;
        }
#endif
    }
}
