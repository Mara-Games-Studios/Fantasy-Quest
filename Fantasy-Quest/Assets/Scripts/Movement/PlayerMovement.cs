using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMovement : MonoBehaviour
{
    private PlayerInput _playerInput;
    private Camera _mainCamera;
    private Vector2 _movePositionForMouse;
    private Vector2 _movePositionForKeyBoard;
    private NavMeshAgent _agent;
    private NavMeshPath _path;
    private float _minDistanceForWalkable;
    private float _speedCorrection;

    [Header("Player Speed Changer")]
    private PlayerSpeedChanger _speedChanger;
    [SerializeField] private AnimationCurve _speedChangeCurve;
    [SerializeField] private float _baseSpeed;

    private void OnValidate()
    {
        if (GetComponent<NavMeshAgent>() != null)
        {
            _agent = GetComponent<NavMeshAgent>();
        }
    }

    private void Awake()
    {
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _path = new NavMeshPath();
        _movePositionForMouse = new Vector2(transform.position.x, transform.position.y);
        _movePositionForKeyBoard = transform.position;
        _playerInput = new PlayerInput();
        _mainCamera = Camera.main;
        _minDistanceForWalkable = 0.2f;
        _speedCorrection = 0.7f;
        _speedChanger = new PlayerSpeedChanger(_agent, _speedChangeCurve, _baseSpeed);
    }

    private void OnEnable()
    {
        _playerInput.Enable();
        _playerInput.Player.MouseMove.performed += SetMovePointFromMouse;
    }

    private void OnDisable()
    {
        _playerInput.Disable();
        _playerInput.Player.MouseMove.performed -= SetMovePointFromMouse;
    }

    private void Update()
    {
        SetMovePointFromKeyBoard();

        if (_movePositionForKeyBoard != Vector2.zero)
        {
            _agent.SetDestination(_movePositionForKeyBoard);
            _movePositionForMouse = new Vector2(transform.position.x, transform.position.y);
        }
        else if (_movePositionForMouse.x != transform.position.x &&
                _movePositionForMouse.y != transform.position.y &&
                IsEndPointWalkable())
        {
            NavMesh.CalculatePath(transform.position, _movePositionForMouse, NavMesh.AllAreas, _path);
            _speedChanger.ChangeSpeed(_path);
            _agent.SetDestination(_movePositionForMouse);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        DrawPath();
    }

    private void SetMovePointFromMouse(InputAction.CallbackContext context)
    {
        _movePositionForMouse =
            _mainCamera.ScreenToWorldPoint(_playerInput.Player.MousePositionForMove.ReadValue<Vector2>());

        if (IsEndPointWalkable())
        {
            NavMesh.CalculatePath(transform.position, _movePositionForMouse, NavMesh.AllAreas, _path);
            _speedChanger.CalculatePathDistance(_path);
        }
        else
        {
            _movePositionForMouse = new Vector2(transform.position.x, transform.position.y);
        }
    }

    private void SetMovePointFromKeyBoard()
    {
        _movePositionForKeyBoard = _playerInput.Player.KeyBoardMove.ReadValue<Vector2>();

        if (_movePositionForKeyBoard != Vector2.zero)
        {
            _movePositionForKeyBoard =
                _movePositionForKeyBoard * _speedCorrection +
                new Vector2(transform.position.x, transform.position.y);
        }
    }

    private bool IsEndPointWalkable()
    {
        _agent.SetDestination(_movePositionForMouse);

        if (Mathf.Abs(_agent.destination.x - _movePositionForMouse.x) < _minDistanceForWalkable &&
            Mathf.Abs(_agent.destination.y - _movePositionForMouse.y) < _minDistanceForWalkable)
        {
            return true;
        }

        _agent.SetDestination(transform.position);
        return false;
    }

    private void DrawPath()
    {
        if (_path != null)
        {
            NavMesh.CalculatePath(transform.position, _movePositionForMouse, NavMesh.AllAreas, _path);

            for (int i = 0; i < _path.corners.Length - 1; i++)
            {
                Gizmos.DrawLine(_path.corners[i], _path.corners[i + 1]);
            }
        }
    }
}
