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

        [SerializeField]
        private bool isCatScaleChange = false;
        public bool IsCatScaleChange => isCatScaleChange;

        [SerializeField]
        private float newCatScale = 1.0f;
        public float NewCatScale => newCatScale;

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
