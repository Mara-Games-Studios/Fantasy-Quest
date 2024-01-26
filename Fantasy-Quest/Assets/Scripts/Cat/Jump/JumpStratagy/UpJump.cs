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
        data.XVelosity = upJumpConfig.StartXVelocity;
        data.YVelosity = upJumpConfig.StartYVelocity;
    }

    public void Update()
    {
        target.Translate(GetConvertedVelocity() * Time.deltaTime);
        data.YVelosity -= upJumpConfig.BaseGravity * Time.deltaTime;
        data.XVelosity -= upJumpConfig.XMoveResistance * Time.deltaTime;
        //if ((CatPlayer.transform.localScale.x) / (Mathf.Abs(CatPlayer.transform.localScale.x)) > 0)
        //{
        //    if (Data.XVelosity > 0)
        //    {
        //        Data.XVelosity -= airConfig.XMoveResistance * Time.deltaTime;
        //    }
        //    else
        //    {
        //        Data.XVelosity = 0;
        //    }
        //}
        //else
        //{
        //    if (Data.XVelosity < 0)
        //    {
        //        Data.XVelosity += airConfig.XMoveResistance * Time.deltaTime;
        //    }
        //    else
        //    {
        //        Data.XVelosity = 0;
        //    }
        //}
    }

    protected Vector2 GetConvertedVelocity()
    {
        return new Vector2(data.XVelosity, data.YVelosity);
    }

}
