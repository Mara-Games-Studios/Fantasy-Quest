namespace Cat.StateMachine.States.Air
{
    public class Air : StateBase
    {
        public Air(IStateSwitcher stateSwitcher, StateMachineData data, CatImpl cat)
            : base(stateSwitcher, data, cat) { }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Update()
        {
            base.Update();
            Cat.ActiveJumpType.Update();
        }
    }
}
