using System.Collections;
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
        [RequiredIn(PrefabKind.PrefabInstanceAndNonPrefabInstance)]
        private Movement catMovement;

        private float flipTime;
        public UnityEvent FlipFinished;

        private void Awake()
        {
            flipTime = catMovement.GetComponentInChildren<Flipper>().GetFlipTime();
        }

        public void ChangeWatchDirection()
        {
            catMovement.ChangeVector(watchDirection);
            _ = StartCoroutine(WaitForFlip());
            FlipFinished?.Invoke();
        }

        public void ChangeWatchDirection(Vector newWatchDirection)
        {
            catMovement.ChangeVector(newWatchDirection);
            _ = StartCoroutine(WaitForFlip());
            FlipFinished?.Invoke();
        }

        private IEnumerator WaitForFlip()
        {
            yield return new WaitForSeconds(flipTime);
        }

        public float GetDuration()
        {
            return flipTime;
        }

        [Button]
        public void FindCat()
        {
            catMovement = FindAnyObjectByType<Movement>().GetComponent<Movement>();
        }
    }
}
