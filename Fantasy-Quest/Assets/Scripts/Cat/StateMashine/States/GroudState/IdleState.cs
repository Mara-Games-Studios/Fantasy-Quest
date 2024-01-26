using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : GroundState
{
    public IdleState(IStateSwitcher stateSwitcher, StateMashineData data, Cat cat) : base(stateSwitcher, data, cat)
    {

    }

    public override void Enter()
    {
       
        base.Enter();
        //View.StartIdle();
    }

    public override void Update()
    {
        base.Update();

        if (!IsHorizontalMoveZero())
        {
            StateSwitcher.SwitchState<RunState>();
        }
    }

    public override void Exit()
    {
        base.Exit();
        //View.StopIdle();
    }
}
