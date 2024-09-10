using System;
using System.Collections.Generic;
using System.Linq;
using Cat;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DialogueBubble
{
    [AddComponentMenu("Scripts/DialogueBubble/DialogueBubble.FlipperByCat")]
    internal class FlipperByCat : MonoBehaviour
    {
        [Serializable]
        private struct PosAndRotationByVector
        {
            public Vector Vector;
            public Quaternion Rotation;
            public Vector3 Position;

            public void Apply(Transform transform)
            {
                transform.localPosition = Position;
                transform.localRotation = Rotation;
            }
        }

        [Required]
        [SerializeField]
        private Movement catMovement;

        [SerializeField]
        private List<PosAndRotationByVector> positions;

        private Dictionary<Vector, PosAndRotationByVector> PositionsByVector =>
            positions.ToDictionary(x => x.Vector, x => x);

        private void Awake()
        {
            catMovement.OnVectorChanged += StateChanged;
        }

        private void OnDestroy()
        {
            catMovement.OnVectorChanged -= StateChanged;
        }

        private void StateChanged(Vector direction)
        {
            PositionsByVector[direction].Apply(transform);
        }
    }
}
