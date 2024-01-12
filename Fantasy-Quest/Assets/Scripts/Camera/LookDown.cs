using UnityEngine;

public class LookDown : MonoBehaviour
{
    [Header("LookParameters")]
    [SerializeField]
    private PanDirection panDir;

    [SerializeField]
    private float panDistance = 2.0f;

    [SerializeField]
    private float panSpeed = 0.35f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CamManager.Instance.PanCameraCoroutineTrigger(panDistance, panSpeed, panDir, false);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        CamManager.Instance.PanCameraCoroutineTrigger(panDistance, panSpeed, panDir, true);
    }
}

public enum PanDirection
{
    Up,
    Down,
    Left,
    Right
}
