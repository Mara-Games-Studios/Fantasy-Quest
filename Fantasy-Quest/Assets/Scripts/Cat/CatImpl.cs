using Cat.StateMachine;
using Cat.Strategies.Jump;
using Cat.Strategies.Move;
using UnityEngine;

namespace Cat
{
    [AddComponentMenu("Scripts/Cat/Cat")]
    public class CatImpl : MonoBehaviour
    {
        [SerializeField]
        private CatConfig config;

        [SerializeField]
        private GroundChecker checker;
        private IMoveable currentMovementType;
        private IJumpable currentUpJumpType;
        private IJumpable currentDownJumpType;
        private IJumpable activeJumpType;
        private HandleInput playerInput;
        private StateMachineImpl stateMachine;

        public HandleInput Input => playerInput;
        public IMoveable CurrentMoveType => currentMovementType;
        public IJumpable ActiveJumpType => activeJumpType;
        public CatConfig Config => config;
        public GroundChecker GroundChecker => checker;
        public StateMachineImpl StateMachine => stateMachine;

        private void Awake()
        {
            playerInput = new HandleInput(new CatInput());
            stateMachine = new StateMachineImpl(this);
            currentMovementType = new AnyWay(transform, stateMachine.Data);
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
            stateMachine.CheckInput();
            stateMachine.Update();
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
}
