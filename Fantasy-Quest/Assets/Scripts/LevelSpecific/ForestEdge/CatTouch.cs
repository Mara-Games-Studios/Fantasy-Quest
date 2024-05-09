using UnityEngine;
using UnityEngine.Events;

namespace LevelSpecific.ForestEdge
{
    [AddComponentMenu("Scripts/LevelSpecific/ForestEdge/LevelSpecific.ForestEdge.CatTouch")]
    internal class CatTouch : MonoBehaviour
    {
        public UnityEvent OnCatTouched;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponentInParent<Cat.Movement>())
            {
                Debug.Log("Touched Symon");
                OnCatTouched?.Invoke();
            }
        }
    }
}
