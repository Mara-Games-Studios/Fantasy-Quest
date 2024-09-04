using Sirenix.OdinInspector;
using UnityEngine;

namespace Rails
{
    [ExecuteAlways]
    [AddComponentMenu("Scripts/Rails/Rails.Point")]
    internal class Point : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private RailsImpl rails;
        public RailsImpl Rails => rails;

        [Range(RailsImpl.MIN_TIME, RailsImpl.MAX_TIME)]
        [SerializeField]
        private float value;
        public float Value => value;

        private void Update()
        {
            transform.position = rails.Path.GetPointAtTime(value);
        }
    }
}
