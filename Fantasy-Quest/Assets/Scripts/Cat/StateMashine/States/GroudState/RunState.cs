public class RunState : GroundState
{
    private readonly RunStateConfig config;

    public RunState(IStateSwitcher stateSwitcher, StateMashineData data, Cat cat)
        : base(stateSwitcher, data, cat)
    {
        config = cat.Config.RunStateConfig;
    }

    public override void Enter()
    {
        base.Enter();
        //View.StartRun();
        Data.Speed = config.Speed;
    }

    public override void Update()
    {
        base.Update();
        CatPlayer.CurrentMoveType.Update();

        if (IsHorizontalMoveZero())
        {
            StateSwitcher.SwitchState<IdleState>();
        }
    }

    public override void Exit()
    {
        base.Exit();
        //View.StopRun();
    }
}
