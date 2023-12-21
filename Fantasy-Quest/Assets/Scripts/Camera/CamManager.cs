using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamManager : MonoBehaviour
{
    public static CamManager Instance;

    [SerializeField]
    private List<CinemachineVirtualCamera> cameras = new();

    [SerializeField]
    private CinemachineVirtualCamera activeCam;

    private CinemachineFramingTransposer transposer;
    private Vector2 startOffset;

    public int ActiveCameraNum = 1;

    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        transposer = activeCam.GetCinemachineComponent<CinemachineFramingTransposer>();
        startOffset = transposer.m_TrackedObjectOffset;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ActiveCameraNum++;
            if (ActiveCameraNum <= cameras.Count - 1)
            {
                activeCam.Priority = 1;
                activeCam = cameras[ActiveCameraNum];
                transposer = activeCam.GetCinemachineComponent<CinemachineFramingTransposer>();
                startOffset = transposer.m_TrackedObjectOffset;
                activeCam.Priority = 100;
            }
            else
            {
                ActiveCameraNum--;
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            ActiveCameraNum--;
            if (ActiveCameraNum > -1)
            {
                activeCam.Priority = 1;
                activeCam = cameras[ActiveCameraNum];
                transposer = activeCam.GetCinemachineComponent<CinemachineFramingTransposer>();
                startOffset = transposer.m_TrackedObjectOffset;
                activeCam.Priority = 100;
            }
            else
            {
                ActiveCameraNum++;
            }
        }
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
        Vector2 endPos = Vector2.zero;
        _ = Vector2.zero;
        Vector2 startPos;
        if (!panToStartingPos)
        {
            switch (panDir)
            {
                case PanDirection.Left:
                    endPos = Vector2.left;
                    break;
                case PanDirection.Right:
                    endPos = Vector2.right;
                    break;
                case PanDirection.Up:
                    endPos = Vector2.up;
                    break;
                case PanDirection.Down:
                    endPos = Vector2.down;
                    break;
            }
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
