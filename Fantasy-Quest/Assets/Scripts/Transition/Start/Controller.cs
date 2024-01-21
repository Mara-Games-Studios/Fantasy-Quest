using UnityEngine;

namespace Transition.Start
{
    [AddComponentMenu("Scripts/Transition/Start/Transition.Start.Controller")]
    internal class Controller : MonoBehaviour
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
