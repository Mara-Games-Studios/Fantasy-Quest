namespace Cat.StateMachine.States.Ground
{
    public class Run : GroundImpl
    {
        private readonly RunStateConfig config;

        public Run(IStateSwitcher stateSwitcher, StateMachineData data, CatImpl cat)
            : base(stateSwitcher, data, cat)
        {
            config = cat.Config.RunStateConfig;
        }

        public override void Enter()
        {
            base.Enter();
            Data.Speed = config.Speed;
        }

        public override void Update()
        {
            base.Update();
            Cat.CurrentMoveType.Update();

            if (IsHorizontalMoveZero())
            {
                StateSwitcher.SwitchState<Idle>();
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
