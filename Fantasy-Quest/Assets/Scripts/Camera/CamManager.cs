using System.Collections;
using Cinemachine;
using UnityEngine;

public class CamManager : MonoBehaviour
{
    public static CamManager Instance;

    [SerializeField]
    private CinemachineVirtualCamera activeCam;

    private CinemachineFramingTransposer transposer;
    private Vector2 startOffset;

    public int ActiveCameraNum = 1;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        transposer = activeCam.GetCinemachineComponent<CinemachineFramingTransposer>();
        startOffset = transposer.m_TrackedObjectOffset;
    }

    public void PanCameraCoroutineTrigger(
        float distance,
        float panSpeed,
        PanDirection panDir,
        bool panToStartingPos
    )
    {
        _ = StartCoroutine(PanCamera(distance, panSpeed, panDir, panToStartingPos));
    }

    private IEnumerator PanCamera(
        float distance,
        float panSpeed,
        PanDirection panDir,
        bool panToStartingPos
    )
    {
        Vector2 startPos;
        Vector2 endPos;
        if (!panToStartingPos)
        {
            endPos = panDir switch
            {
                PanDirection.Left => Vector2.left,
                PanDirection.Right => Vector2.right,
                PanDirection.Up => Vector2.up,
                PanDirection.Down => Vector2.down,
                _ => Vector2.zero
            };
            endPos *= distance;
            startPos = startOffset;
            endPos += startOffset;
        }
        else
        {
            startPos = transposer.m_TrackedObjectOffset;
            endPos = startOffset;
        }

        float time = 0.0f;
        while (time < panSpeed)
        {
            time += Time.deltaTime;

            Vector3 panLerp = Vector3.Lerp(startPos, endPos, time / panSpeed);
            transposer.m_TrackedObjectOffset = panLerp;

            yield return null;
        }
    }
}
