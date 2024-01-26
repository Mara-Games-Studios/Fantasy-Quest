using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownJumpState : AirState
{
    public DownJumpState(IStateSwitcher stateSwitcher, StateMashineData data, Cat cat) : base(stateSwitcher, data, cat)
    {
    }

    public override void Enter()
    {
        base.Enter();
        CatPlayer.ActiveJumpType.Jump();
    }

    public override void Update()
    {
        base.Update();

        if (Data.YVelosity <= 0)
        {
            StateSwitcher.SwitchState<FallState>();
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
