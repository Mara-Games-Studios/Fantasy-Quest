using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallState : AirState
{
    private readonly GroundChecker groundChecker;
    public FallState(IStateSwitcher stateSwitcher, StateMashineData data, Cat cat) : base(stateSwitcher, data, cat)
    {
        groundChecker = cat.GroundChecker;
    }

    public override void Update()
    {
        base.Update();

        if (groundChecker.IsTouch)
        {
            Data.YVelosity = 0f;
            Data.XVelosity = 0f;
            StateSwitcher.SwitchState<IdleState>();                     
        }
    }
}
