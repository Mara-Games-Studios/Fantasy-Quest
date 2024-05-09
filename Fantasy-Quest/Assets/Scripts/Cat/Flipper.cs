using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Cat
{
    [Serializable]
    public struct RotationByVector
    {
        public Vector3 Rotation;
        public Vector Vector;
    }

    [AddComponentMenu("Scripts/Cat/Cat.Flipper")]
    internal class Flipper : MonoBehaviour
    {
        [SerializeField]
        private Movement catMovement;

        //[SerializeField]
        //private float flipTime = 0.5f;

        [ReadOnly]
        [SerializeField]
        private Vector toFlip;

        [ReadOnly]
        [SerializeField]
        private bool isFlipping;

        [SerializeField]
        private List<RotationByVector> rotationByVectorsRaw;
        private Dictionary<Vector, Vector3> RotationByVectors =>
            rotationByVectorsRaw.ToDictionary(x => x.Vector, x => x.Rotation);

        private void VectorChanged(Vector vector)
        {
            transform.rotation = Quaternion.Euler(RotationByVectors[vector]);
        }

        public float GetFlipTime()
        {
            return 0;
        }

        private void OnEnable()
        {
            catMovement.OnVectorChanged += VectorChanged;
        }

        private void OnDisable()
        {
            catMovement.OnVectorChanged -= VectorChanged;
        }
    }
}
