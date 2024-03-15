using UnityEngine;
using UnityEngine.Events;

namespace Transition.Start
{
    [AddComponentMenu("Scripts/Transition/Start/Transition.Start.Controller")]
    internal class Controller : MonoBehaviour
    {
        [SerializeField]
        private GameObject view;

        [SerializeField]
        private Animator animator;

        public UnityEvent StartTransitionEnd;

        private void Start()
        {
            view.SetActive(true);
            animator.enabled = true;
        }

        // Must me called by view callback
        public void DestroySelf()
        {
            StartTransitionEnd?.Invoke();
            Destroy(gameObject);
        }
    }
}
