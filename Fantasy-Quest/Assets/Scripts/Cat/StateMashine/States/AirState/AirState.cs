public class AirState : MoveState
{
    public AirState(IStateSwitcher stateSwitcher, StateMashineData data, Cat cat)
        : base(stateSwitcher, data, cat) { }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        CatPlayer.ActiveJumpType.Update();
    }
}
