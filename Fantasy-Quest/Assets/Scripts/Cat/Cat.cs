using UnityEngine;

public class Cat : MonoBehaviour
{
    [SerializeField] private CatConfig config;
    [SerializeField] private GroundChecker checker;
    private IMoveable currentMovementType;
    private IJumpable currentUpJumpType;
    private IJumpable currentDownJumpType;
    private IJumpable activeJumpType;
    private HandleInput playerInput;
    private CatStateMashine stateMashine;

    public HandleInput Input => playerInput;
    public IMoveable CurrentMoveType => currentMovementType;
    public IJumpable ActiveJumpType => activeJumpType;
    public CatConfig Config => config;
    public GroundChecker GroundChecker => checker;
    public CatStateMashine StateMashine => stateMashine;

    private void Awake()
    {
        playerInput = new HandleInput(new CatInput());
        stateMashine = new CatStateMashine(this);
        currentMovementType = new Movement(transform, stateMashine.Data);
        currentUpJumpType = new NoJump();
        currentDownJumpType = new NoJump();
        activeJumpType = new NoJump();
    }

    private void OnEnable()
    {
        playerInput.EnableInput();
    }

    private void OnDisable()
    {
        playerInput.DisableInput();
    }

    private void Update()
    {

        stateMashine.CheckInput();

        stateMashine.Update();
    }

    public void ChangeMovementType(IMoveable moveType)
    {
        currentMovementType = moveType;
    }

    public void ChangeUpJumpType(IJumpable jumpType)
    {
        currentUpJumpType = jumpType;
    }

    public void ChangeDownJumpType(IJumpable jumpType)
    {
        currentDownJumpType = jumpType;
    }

    public void SetActiveDownJumpType()
    {
        activeJumpType = currentDownJumpType;
    }

    public void SetActiveUpJumpType()
    {
        activeJumpType = currentUpJumpType;
    }
}
