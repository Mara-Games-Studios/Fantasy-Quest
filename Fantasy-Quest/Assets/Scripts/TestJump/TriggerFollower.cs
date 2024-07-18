using Sirenix.OdinInspector;
using UnityEngine;

namespace TestJump
{
    [AddComponentMenu("Scripts/TestJump/TestJump.TriggerFollower")]
    internal class TriggerFollower : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private Transform targetTransform;

        private void Update()
        {
            transform.position = targetTransform.position;
        }
    }
}
