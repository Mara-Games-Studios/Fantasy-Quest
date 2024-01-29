namespace Cat.StateMachine.States.Ground
{
    public class Idle : GroundImpl
    {
        public Idle(IStateSwitcher stateSwitcher, StateMachineData data, CatImpl cat)
            : base(stateSwitcher, data, cat) { }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Update()
        {
            base.Update();

            if (!IsHorizontalMoveZero())
            {
                StateSwitcher.SwitchState<Run>();
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
