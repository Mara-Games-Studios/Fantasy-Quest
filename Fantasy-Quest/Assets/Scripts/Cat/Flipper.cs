using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
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

        [SerializeField]
        private float flipTime = 0.5f;

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
            if (toFlip != vector && !isFlipping)
            {
                StartFlipping(vector);
            }
            toFlip = vector;
        }

        private void StartFlipping(Vector vector)
        {
            isFlipping = true;
            TweenerCore<Quaternion, Vector3, QuaternionOptions> rotation = transform.DORotate(
                RotationByVectors[vector],
                flipTime
            );
            rotation.onComplete += () => EndFlipping(vector);
        }

        private void EndFlipping(Vector flippedTo)
        {
            isFlipping = false;
            if (toFlip != flippedTo)
            {
                StartFlipping(toFlip);
            }
        }

        public float GetFlipTime()
        {
            return flipTime;
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
