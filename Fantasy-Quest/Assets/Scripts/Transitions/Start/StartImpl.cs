using UnityEngine;

namespace Transitions.Start
{
    [AddComponentMenu("Scripts/Transitions/Transitions.Start")]
    internal class StartImpl : MonoBehaviour
    {
        [SerializeField]
        private GameObject view;

        [SerializeField]
        private Animator animator;

        private void Start()
        {
            view.SetActive(true);
            animator.enabled = true;
        }

        // Must me called by view callback
        public void DestroySelf()
        {
            Destroy(gameObject);
        }
    }
}
