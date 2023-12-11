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
        _movePositionForMouse = transform.position;
        _movePositionForKeyBoard = transform.position;
        _playerInput = new PlayerInput();
        _mainCamera = Camera.main;
        _minDistanceForWalkable = 0.2f;
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
        else if (_movePositionForMouse != 
            new Vector2(transform.position.x, transform.position.y) && IsEndPointWalkable())
        {
            _agent.SetDestination(_movePositionForMouse);
            NavMesh.CalculatePath(transform.position, _movePositionForMouse, NavMesh.AllAreas, _path);
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
    }

    private void SetMovePointFromKeyBoard()
    {
        _movePositionForKeyBoard = _playerInput.Player.KeyBoardMove.ReadValue<Vector2>();

        if (_movePositionForKeyBoard != Vector2.zero)
        {
            _movePositionForKeyBoard = 
                _movePositionForKeyBoard * .7f + new Vector2(transform.position.x, transform.position.y);
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
