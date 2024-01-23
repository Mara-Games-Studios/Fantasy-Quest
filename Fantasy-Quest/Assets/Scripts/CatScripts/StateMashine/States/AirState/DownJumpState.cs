using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownJumpState : AirState
{
    private readonly DownJumpConfig downJumpConfig;

    public DownJumpState(IStateSwitcher stateSwitcher, StateMashineData data, Cat cat) : base(stateSwitcher, data, cat)
    {
        downJumpConfig = cat.Config.AirConfig.JumpDownStateConfig;
    }

    public override void Enter()
    {
        base.Enter();
        CatPlayer.CurrentDownJumpType.Jump(Data, downJumpConfig.StartXVelocity, downJumpConfig.StartYVelocity);
    }

    public override void Update()
    {
        base.Update();

        if (Data.YVelosity < 0)
        {
            StateSwitcher.SwitchState<FallState>();
        }
    }

    public override void Exit()
    {
        base.Exit();
        CurrentJumpType = CatPlayer.CurrentDownJumpType;
    }
}
