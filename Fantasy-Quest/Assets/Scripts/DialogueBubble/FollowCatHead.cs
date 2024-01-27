using UnityEngine;

public class FollowCatHead : MonoBehaviour
{
    [SerializeField]
    private Transform catTransform;

    // Update is called once per frame
    private void Update()
    {
        transform.position = catTransform.position;
    }
}
