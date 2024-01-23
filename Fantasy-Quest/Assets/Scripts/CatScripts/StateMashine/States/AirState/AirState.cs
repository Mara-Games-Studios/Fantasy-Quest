using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirState : MoveState
{
    private readonly AirConfig airConfig;
    protected IJumpable CurrentJumpType;

    public AirState(IStateSwitcher stateSwitcher, StateMashineData data, Cat cat) : base(stateSwitcher, data, cat)
    {
        airConfig = cat.Config.AirConfig;
        CurrentJumpType = new DownJump(cat.transform, data);
    }

    public override void Enter()
    {
        base.Enter();

        Data.Speed = airConfig.Speed;
    }

    public override void Update()
    {
        base.Update();

        if (CurrentJumpType == null)
            return;

        CurrentJumpType.Update();
        Data.YVelosity -= airConfig.BaseGravity *Time.deltaTime;

        if((CatPlayer.transform.localScale.x) / (Mathf.Abs(CatPlayer.transform.localScale.x)) > 0)
        {
            if (Data.XVelosity > 0)
            {
                Data.XVelosity -= airConfig.XMoveResistance * Time.deltaTime;
            }
            else
            {
                Data.XVelosity = 0;
            }
        }
        else
        {
            if (Data.XVelosity < 0)
            {
                Data.XVelosity += airConfig.XMoveResistance * Time.deltaTime;
            }
            else
            {
                Data.XVelosity = 0;
            }
            Debug.Log("Data.XVelosity" + Data.XVelosity);
        }
        
    }
}
