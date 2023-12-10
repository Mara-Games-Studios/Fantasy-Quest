using Cinemachine;
using UnityEngine;

public class SwitchToFocusCamera : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera virtualCamera;

    [SerializeField]
    private int priority = 1000;
    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            virtualCamera.Priority = priority;
        }

    }

    private void OnTriggerExit2D(UnityEngine.Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            virtualCamera.Priority = 1;
        }
    }
}
