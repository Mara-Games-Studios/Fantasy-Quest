using Rails;
using UnityEngine;

namespace Cat.Jump
{
    [RequireComponent(typeof(RailsImpl))]
    [AddComponentMenu("Scripts/Cat/Jump/Cat.Jump.GroundMask")]
    internal class GroundMask : MonoBehaviour
    {
        [SerializeField]
        private bool isAvailable = true;
        public bool IsAvailable => isAvailable;

        private RailsImpl rails;
        public RailsImpl Rails => rails;

        private void Awake()
        {
            if (rails == null)
            {
                rails = GetComponent<RailsImpl>();
            }
        }

        private void OnValidate()
        {
            rails = GetComponent<RailsImpl>();
        }

        public void SetAvailable(bool isAvailable)
        {
            this.isAvailable = isAvailable;
        }
    }
}
