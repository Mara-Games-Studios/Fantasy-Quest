using Sirenix.OdinInspector;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Cat
{
    [AddComponentMenu("Scripts/Cat/GroundChecker")]
    public class GroundChecker : MonoBehaviour
    {
        [SerializeField]
        private LayerMask ground;

        [SerializeField]
        [Range(0.01f, 1)]
        private float distanceToCheckGround;

        [ReadOnly]
        [SerializeField]
        private bool isTouch;

        public bool IsTouch => isTouch;

        private void FixedUpdate()
        {
            isTouch = Physics2D.OverlapCircle(transform.position, distanceToCheckGround, ground);
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Handles.DrawSolidDisc(transform.position, Vector3.forward, distanceToCheckGround);
        }
#endif
    }
}
