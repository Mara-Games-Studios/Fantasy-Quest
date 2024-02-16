using Cinemachine;
using UnityEngine;

namespace Camera
{
    [AddComponentMenu("Scripts/Camera/Camera.SwitchToFocusCamera")]
    public class SwitchToFocusCamera : MonoBehaviour
    {
        [SerializeField]
        private CinemachineVirtualCamera virtualCamera;

        [SerializeField]
        private int priority = 1000;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                virtualCamera.Priority = priority;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                virtualCamera.Priority = 1;
            }
        }
    }
}
