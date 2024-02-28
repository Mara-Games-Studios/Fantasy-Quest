using CameraShake;
using UnityEngine;

namespace Minigames.MouseInHay
{
    [AddComponentMenu("Scripts/Minigames/MouseInHay/Minigames.MouseInHay.CameraShake")]
    internal class CameraShake : MonoBehaviour
    {
        [SerializeField]
        private CameraShaker cameraShaker;

        public void Invoke()
        {
            cameraShaker.ShakePresets.Explosion2D();
        }
    }
}
