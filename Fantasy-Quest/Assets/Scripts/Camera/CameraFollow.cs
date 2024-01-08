using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform playerTransform;

    [SerializeField]
    private float flipTime = 0.5f;

    private bool isRight;

    private void Awake()
    {
        isRight = false;
    }

    private void Update()
    {
        transform.position = playerTransform.position;
    }

    public void CallTurn()
    {
        _ = LeanTween.rotateY(gameObject, RotateTransform(), flipTime);
    }

    private float RotateTransform()
    {
        isRight = !isRight;

        return isRight ? 180.0f : 0f;
    }
}
