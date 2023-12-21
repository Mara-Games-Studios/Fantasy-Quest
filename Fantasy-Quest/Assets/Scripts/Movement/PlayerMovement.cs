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

    [SerializeField]
    private bool facingRight = true;

    private NavMeshPath path;
    private PlayerInput playerInput;

    [Header("Player Speed Changer")]
    private PlayerSpeedChanger speedChanger;

    [SerializeField]
    private AnimationCurve speedChangeCurve;

    [SerializeField]
    private float baseSpeed;

    private void OnValidate()
    {
        if (GetComponent<NavMeshAgent>() != null)
        {
            agent = GetComponent<NavMeshAgent>();
        }
    }

    private void Awake()
    {
        path = new NavMeshPath();
        playerInput = new PlayerInput();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        movePositionForMouse = transform.position;
        movePositionForKeyBoard = transform.position;
        speedChanger = new PlayerSpeedChanger(agent, speedChangeCurve, baseSpeed);
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
            movePositionForMouse.x != transform.position.x
            && movePositionForMouse.y != transform.position.y
        )
        {
            _ = NavMesh.CalculatePath(
                transform.position,
                movePositionForMouse,
                NavMesh.AllAreas,
                path
            );
            speedChanger.ChangeSpeed(path);
            _ = agent.SetDestination(movePositionForMouse);
        }

        Vector3 movePositionMouseV3 = movePositionForMouse;
        Vector3 playerDirection = transform.position - movePositionMouseV3;

        if (playerDirection.x < 0 && !facingRight)
        {
            Flip();
        }
        else if (playerDirection.x > 0 && facingRight)
        {
            Flip();
        }

        if (agent.desiredVelocity == Vector3.zero && girlAnimation.AnimationName != idleAnimation)
        {
            _ = girlAnimation.AnimationState.SetAnimation(0, idleAnimation, true);
        }
        else if (
            agent.desiredVelocity != Vector3.zero
            && girlAnimation.AnimationName != walkAnimation
        )
        {
            _ = girlAnimation.AnimationState.SetAnimation(0, walkAnimation, true);
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        cameraFollow.CallTurn();
        transform.localScale = Scaler;
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

        if (IsEndPointWalkable())
        {
            _ = NavMesh.CalculatePath(
                transform.position,
                movePositionForMouse,
                NavMesh.AllAreas,
                path
            );
            speedChanger.CalculatePathDistance(path);
        }
        else
        {
            movePositionForMouse = new Vector2(transform.position.x, transform.position.y);
        }
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
