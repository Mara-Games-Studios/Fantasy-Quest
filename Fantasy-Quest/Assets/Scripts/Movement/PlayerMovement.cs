using Sirenix.OdinInspector;
using Spine.Unity;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private CameraFollow cameraFollow;

    [SerializeField]
    private Camera mainCamera;

    [SerializeField]
    private NavMeshAgent agent;

    [ReadOnly]
    [SerializeField]
    private Vector2 movePositionForMouse;

    [ReadOnly]
    [SerializeField]
    private Vector2 movePositionForKeyBoard;

    [SerializeField]
    private float minDistanceForWalkable = 0.2f;

    [SerializeField]
    private float speedCorrection = 0.7f;

    [SerializeField]
    private SkeletonAnimation girlAnimation;

    [SpineAnimation]
    [SerializeField]
    private string idleAnimation;

    [SpineAnimation]
    [SerializeField]
    private string walkAnimation;

    [SerializeField]
    private Vector3 leftRotation;

    [SerializeField]
    private Vector3 rightRotation;

    private NavMeshPath path;
    private PlayerInput playerInput;

    private void Awake()
    {
        path = new();
        playerInput = new();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        movePositionForMouse = transform.position;
        movePositionForKeyBoard = transform.position;
    }

    private void OnEnable()
    {
        playerInput.Enable();
        playerInput.Player.MouseMove.performed += SetMovePointFromMouse;
    }

    private void OnDisable()
    {
        playerInput.Disable();
        playerInput.Player.MouseMove.performed -= SetMovePointFromMouse;
    }

    private void Update()
    {
        SetMovePointFromKeyBoard();

        if (movePositionForKeyBoard != Vector2.zero)
        {
            _ = agent.SetDestination(movePositionForKeyBoard);
            movePositionForMouse = new Vector2(transform.position.x, transform.position.y);
        }
        else if (
            movePositionForMouse != new Vector2(transform.position.x, transform.position.y)
            && IsEndPointWalkable()
        )
        {
            _ = agent.SetDestination(movePositionForMouse);
            _ = NavMesh.CalculatePath(
                transform.position,
                movePositionForMouse,
                NavMesh.AllAreas,
                path
            );
        }

        if (agent.desiredVelocity.x > 0 && agent.desiredVelocity.x != 0)
        {
            transform.rotation = Quaternion.Euler(rightRotation);
        }
        else if (agent.desiredVelocity.x != 0)
        {
            transform.rotation = Quaternion.Euler(leftRotation);
        }

        if (agent.desiredVelocity == Vector3.zero && girlAnimation.AnimationName != idleAnimation)
        {
            _ = girlAnimation.AnimationState.SetAnimation(0, idleAnimation, true);
        }
        else if (
            agent.desiredVelocity != Vector3.zero && girlAnimation.AnimationName != walkAnimation
        )
        {
            _ = girlAnimation.AnimationState.SetAnimation(0, walkAnimation, true);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        DrawPath();
    }

    private void SetMovePointFromMouse(InputAction.CallbackContext context)
    {
        movePositionForMouse = mainCamera.ScreenToWorldPoint(
            playerInput.Player.MousePositionForMove.ReadValue<Vector2>()
        );
    }

    private void SetMovePointFromKeyBoard()
    {
        movePositionForKeyBoard = playerInput.Player.KeyBoardMove.ReadValue<Vector2>();

        if (movePositionForKeyBoard != Vector2.zero)
        {
            movePositionForKeyBoard =
                (movePositionForKeyBoard * speedCorrection)
                + new Vector2(transform.position.x, transform.position.y);
        }
    }

    private bool IsEndPointWalkable()
    {
        _ = agent.SetDestination(movePositionForMouse);

        if (
            Mathf.Abs(agent.destination.x - movePositionForMouse.x) < minDistanceForWalkable
            && Mathf.Abs(agent.destination.y - movePositionForMouse.y) < minDistanceForWalkable
        )
        {
            return true;
        }

        _ = agent.SetDestination(transform.position);
        return false;
    }

    private void DrawPath()
    {
        if (path != null)
        {
            _ = NavMesh.CalculatePath(
                transform.position,
                movePositionForMouse,
                NavMesh.AllAreas,
                path
            );

            for (int i = 0; i < path.corners.Length - 1; i++)
            {
                Gizmos.DrawLine(path.corners[i], path.corners[i + 1]);
            }
        }
    }
}
