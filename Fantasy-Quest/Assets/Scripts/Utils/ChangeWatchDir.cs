using Cat;
using Sirenix.OdinInspector;
using UnityEngine;

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

        public void ChangeWatchDirection()
        {
            catMovement.ChangeVector(watchDirection);
        }

        [Button]
        public void FindCat()
        {
            catMovement = FindAnyObjectByType<Cat.Movement>().GetComponent<Cat.Movement>();
        }
    }
}
