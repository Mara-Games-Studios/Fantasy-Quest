namespace Cat.StateMachine.States.Air
{
    public class UpJump : Air
    {
        public UpJump(IStateSwitcher stateSwitcher, StateMachineData data, CatImpl cat)
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
    }
}
