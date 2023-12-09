using Spine.Unity;
using UnityEngine;

public class GirlController : MonoBehaviour
{
    [SerializeField]
    private Vector3 destinationPoint;

    [SpineAnimation]
    [SerializeField]
    private string idleAnimation;

    [SpineAnimation]
    [SerializeField]
    private string walkAnimation;

    [SerializeField]
    private SkeletonAnimation girlAnimation;

    [SerializeField]
    private Vector3 leftRotation;

    [SerializeField]
    private Vector3 rightRotation;

    private void Start()
    {
        destinationPoint = transform.position;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, destinationPoint, Time.deltaTime);
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 screenPosition = new(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
            destinationPoint = worldPosition;

            if (destinationPoint.x > transform.position.x)
            {
                transform.rotation = Quaternion.Euler(rightRotation);
            }
            else
            {
                transform.rotation = Quaternion.Euler(leftRotation);
            }
        }
        if (
            girlAnimation.AnimationName != idleAnimation
            && Vector3.Distance(transform.position, destinationPoint) <= 0.2
        )
        {
            _ = girlAnimation.AnimationState.SetAnimation(0, idleAnimation, true);
        }
        else if (
            girlAnimation.AnimationName != walkAnimation
            && Vector3.Distance(transform.position, destinationPoint) > 0.2
        )
        {
            _ = girlAnimation.AnimationState.SetAnimation(0, walkAnimation, true);
        }
    }
}
