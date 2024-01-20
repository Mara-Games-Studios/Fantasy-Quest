using UnityEngine;

namespace Transitions
{
    [AddComponentMenu("Scripts/Transitions/Transitions.Start")]
    internal class Start : MonoBehaviour
    {
        [SerializeField]
        private Animator animator;

        private void Awake()
        {
            gameObject.SetActive(true);
            animator.enabled = true;
        }

        // Called by animation clip event
        public void DestroySelf()
        {
            Destroy(gameObject);
        }
    }
}
