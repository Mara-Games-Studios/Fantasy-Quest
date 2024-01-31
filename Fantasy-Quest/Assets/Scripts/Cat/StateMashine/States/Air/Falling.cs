using Cat.StateMachine.States.Ground;

namespace Cat.StateMachine.States.Air
{
    public class Falling : Air
    {
        private readonly GroundChecker groundChecker;

        public Falling(IStateSwitcher stateSwitcher, StateMachineData data, CatImpl cat)
            : base(stateSwitcher, data, cat)
        {
            groundChecker = cat.GroundChecker;
        }

        public override void Update()
        {
            base.Update();

            if (groundChecker.IsTouch)
            {
                Data.YVelocity = 0f;
                Data.XVelocity = 0f;
                StateSwitcher.SwitchState<Idle>();
            }
        }
    }
}
