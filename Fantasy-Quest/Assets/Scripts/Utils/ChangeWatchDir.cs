using Cat;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Utils
{
    [AddComponentMenu("Scripts/Utils/Utils.ChangeWatchDir")]
    internal class ChangeWatchDir : MonoBehaviour
    {
        [SerializeField]
        private Vector watchDirection = Vector.Right;

        [SerializeField]
        [RequiredIn(PrefabKind.InstanceInScene)]
        private Movement catMovement;

        public UnityEvent FlipFinished;

        public void ChangeWatchDirection()
        {
            catMovement.ChangeVector(watchDirection);
            FlipFinished?.Invoke();
        }

        public void ChangeWatchDirection(Vector newWatchDirection)
        {
            catMovement.ChangeVector(newWatchDirection);
            FlipFinished?.Invoke();
        }
    }
}
