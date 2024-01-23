using UnityEngine;

public class Cat : MonoBehaviour
{
    [SerializeField] private CatView view;
    [SerializeField] private CatConfig config;
    [SerializeField] private GroundChecker checker;

    private IMoveable currentMovementType;
    private IJumpable currentUpJumpType;
    private IJumpable currentDownJumpType;
    private HandleInput playerInput;
    private CatStateMashine stateMashine;

    public HandleInput Input => playerInput;
    public IMoveable CurrentMoveType => currentMovementType;
    public CatView CatView => CatView;
    public CatConfig Config => config;

    public IJumpable CurrentUpJumpType => currentUpJumpType;
    public IJumpable CurrentDownJumpType => currentDownJumpType;

    public GroundChecker GroundChecker => checker;

    private void Awake()
    {
        view.Initialize();
        playerInput = new HandleInput(new CatInput());
        stateMashine = new CatStateMashine(this);
        currentMovementType = new Movement(transform, stateMashine.Data);
        FindObjectOfType<Factory>().Initialize(this.transform, stateMashine.Data);
        currentUpJumpType = new NoJump(this.transform, stateMashine.Data);
        currentDownJumpType = new NoJump(this.transform, stateMashine.Data);
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
}
