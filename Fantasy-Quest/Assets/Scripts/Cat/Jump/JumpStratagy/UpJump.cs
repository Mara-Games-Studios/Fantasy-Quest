using UnityEngine;

public class UpJump : IJumpable
{
    private Transform target;
    private StateMashineData data;
    private UpJumpConfig upJumpConfig;

    public UpJump(Transform targetTransform, UpJumpConfig config, StateMashineData data)
    {
        this.data = data;
        target = targetTransform;
        upJumpConfig = config;
    }

    public void Jump()
    {
        if ((target.transform.localScale.x) / (Mathf.Abs(target.transform.localScale.x)) > 0)
        {
            data.XVelosity = upJumpConfig.StartXVelocity;
        }
        else
        {
            data.XVelosity = upJumpConfig.StartXVelocity * (-1);
        }
        data.YVelosity = upJumpConfig.StartYVelocity;
    }

    public void Update()
    {
        target.Translate(GetConvertedVelocity() * Time.deltaTime);
        data.YVelosity -= upJumpConfig.BaseGravity * Time.deltaTime;
        if ((target.transform.localScale.x) / (Mathf.Abs(target.transform.localScale.x)) > 0)
        {
            if (data.XVelosity > 0)
            {
                data.XVelosity -= upJumpConfig.XMoveResistance * Time.deltaTime;
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
                data.XVelosity += upJumpConfig.XMoveResistance * Time.deltaTime;
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
