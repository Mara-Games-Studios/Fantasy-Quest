using Cat.StateMachine.States.Air;
using UnityEngine.InputSystem;

namespace Cat.StateMachine.States.Ground
{
    public class GroundImpl : StateBase
    {
        private GroundChecker groundChecker;

        public GroundImpl(IStateSwitcher stateSwitcher, StateMachineData data, CatImpl cat)
            : base(stateSwitcher, data, cat)
        {
            groundChecker = cat.GroundChecker;
        }

        public override void Update()
        {
            base.Update();

            if (groundChecker.IsTouch == false)
            {
                StateSwitcher.SwitchState<Falling>();
            }
        }

        protected override void AddInputActionCallback()
        {
            base.AddInputActionCallback();
            Input.CatInput.Movement.UpJump.started += OnUpJumpKeyPressed;
            Input.CatInput.Movement.DownJump.started += OnDownJumpKeyPressed;
        }

        protected override void RemoveInputActionCallback()
        {
            base.RemoveInputActionCallback();
            Input.CatInput.Movement.UpJump.started -= OnUpJumpKeyPressed;
            Input.CatInput.Movement.DownJump.started -= OnDownJumpKeyPressed;
        }

        private void OnDownJumpKeyPressed(InputAction.CallbackContext context)
        {
            Cat.SetActiveDownJumpType();
            StateSwitcher.SwitchState<DownJump>();
        }

        private void OnUpJumpKeyPressed(InputAction.CallbackContext context)
        {
            Cat.SetActiveUpJumpType();
            StateSwitcher.SwitchState<UpJump>();
        }
    }
}
