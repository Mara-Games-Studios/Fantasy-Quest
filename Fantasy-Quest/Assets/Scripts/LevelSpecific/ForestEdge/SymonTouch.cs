using UnityEngine;
using UnityEngine.Events;

namespace LevelSpecific.ForestEdge
{
    [AddComponentMenu("Scripts/LevelSpecific/ForestEdge/LevelSpecific.ForestEdge.SymonTouch")]
    internal class SymonTouch : MonoBehaviour
    {
        public UnityEvent OnSymonTouched;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponentInParent<Symon.Movement>())
            {
                Debug.Log("Touched Symon");
                OnSymonTouched?.Invoke();
            }
        }
    }
}
