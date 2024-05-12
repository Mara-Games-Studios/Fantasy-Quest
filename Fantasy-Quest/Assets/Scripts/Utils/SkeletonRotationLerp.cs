using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Utils
{
    [AddComponentMenu("Scripts/Utils/Utils.SkeletonRotationLerp")]
    internal class SkeletonRotationLerp : MonoBehaviour
    {
        [Serializable]
        private struct PointRotation
        {
            public Vector3 Rotation;
            public float Point;

            [NonSerialized]
            public Quaternion Quaternion;
        }

        [Required]
        [SerializeField]
        private Transform targetTransform;

        [SerializeField]
        private List<PointRotation> pointRotations = new();

        private void Start()
        {
            pointRotations = pointRotations.OrderBy(p => p.Point).ToList();
        }

        [Button]
        public void SetRotation(float point)
        {
            PointRotation first = pointRotations.Last(x => x.Point <= point);
            PointRotation second = pointRotations.First(x => x.Point >= point);
            float time = (point - first.Point) / (second.Point - first.Point);
            Vector3 rotation = Vector3.Lerp(first.Rotation, second.Rotation, time);
            targetTransform.rotation = Quaternion.Euler(rotation);
        }
    }
}
