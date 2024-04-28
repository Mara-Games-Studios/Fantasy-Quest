using Sirenix.OdinInspector;
using UnityEngine;

namespace Symon
{
    [AddComponentMenu("Scripts/Symon/Symon.MovementInvoke")]
    internal class MovementInvoke : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private Movement movement;
    }
}
