namespace Cat.StateMachine.States.Air
{
    public class DownJump : Air
    {
        public DownJump(IStateSwitcher stateSwitcher, StateMachineData data, CatImpl cat)
            : base(stateSwitcher, data, cat) { }

        public override void Enter()
        {
            base.Enter();
            Cat.ActiveJumpType.Jump();
        }

        public override void Update()
        {
            base.Update();

            if (Data.YVelocity <= 0)
            {
                StateSwitcher.SwitchState<Falling>();
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
