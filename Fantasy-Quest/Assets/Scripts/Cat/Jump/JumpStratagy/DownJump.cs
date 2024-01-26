using UnityEngine;

public class DownJump :  IJumpable
{
    private Transform target;
    private  StateMashineData data;
    private DownJumpConfig downJumpConfig;

    public DownJump(Transform targetTransform, DownJumpConfig config, StateMashineData data)
    {
        target = targetTransform;
        downJumpConfig = config;
        this.data = data;
    }

    public void Jump()
    {
        if((target.transform.localScale.x) / (Mathf.Abs(target.transform.localScale.x)) > 0)
        {
            data.XVelosity = downJumpConfig.StartXVelocity;
        }
        else
        {
            data.XVelosity = downJumpConfig.StartXVelocity * (-1);
        }
       
        data.YVelosity = downJumpConfig.StartYVelocity;
    }

    public  void Update()
    {
        target.Translate(GetConvertedVelocity() * Time.deltaTime);
        data.YVelosity -= downJumpConfig.BaseGravity * Time.deltaTime;
        // data.XVelosity -= downJumpConfig.XMoveResistance * Time.deltaTime;
        if ((target.transform.localScale.x) / (Mathf.Abs(target.transform.localScale.x)) > 0)
        {
            if (data.XVelosity > 0)
            {
                data.XVelosity -= downJumpConfig.XMoveResistance * Time.deltaTime;
            }
            else
            {
                data.XVelosity = 0;
            }
        }
        else
        {
            if (data.XVelosity < 0)
            {
                data.XVelosity += downJumpConfig.XMoveResistance * Time.deltaTime;
            }
            else
            {
                data.XVelosity = 0;
            }
        }
    }

    protected Vector2 GetConvertedVelocity()
    {
        return new Vector2(data.XVelosity, data.YVelosity);
    }
}
