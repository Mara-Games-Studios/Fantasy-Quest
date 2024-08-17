using Sirenix.OdinInspector;
using UnityEngine;

namespace Rails
{
    [ExecuteAlways]
    [AddComponentMenu("Scripts/Rails/Rails.Point")]
    internal class Point : MonoBehaviour
    {
        [Title("Main info")]
        [Required]
        [SerializeField]
        private RailsImpl rails;
        public RailsImpl Rails => rails;

        [Range(0f, 0.99999f)]
        [SerializeField]
        private float value;
        public float Value => value;

        private void Update()
        {
            transform.position = rails.Path.GetPointAtTime(value);
        }
    }
}
