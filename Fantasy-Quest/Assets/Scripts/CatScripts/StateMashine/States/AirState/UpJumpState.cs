
public class UpJumpState : AirState
{
    private readonly UpJumpConfig upJumpConfig;

    public UpJumpState(IStateSwitcher stateSwitcher, StateMashineData data, Cat cat) : base(stateSwitcher, data, cat)
    {
        upJumpConfig = cat.Config.AirConfig.JumpUpStateConfig;
    }

    public override void Enter()
    {
        base.Enter();
        CatPlayer.CurrentUpJumpType.Jump(Data, upJumpConfig.StartXVelocity, upJumpConfig.StartYVelocity);
    }

    public override void Update()
    {
        base.Update();

        if(Data.YVelosity < 0)
        {
            StateSwitcher.SwitchState<FallState>(); 
        }
    }
}
