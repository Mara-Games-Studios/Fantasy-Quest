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
        data.XVelosity = downJumpConfig.StartXVelocity ;
        data.YVelosity = downJumpConfig.StartYVelocity;
    }

    public  void Update()
    {
        target.Translate(GetConvertedVelocity() * Time.deltaTime);
        data.YVelosity -= downJumpConfig.BaseGravity * Time.deltaTime;
        data.XVelosity -= downJumpConfig.XMoveResistance * Time.deltaTime;
    }

    protected Vector2 GetConvertedVelocity()
    {
        return new Vector2(data.XVelosity, data.YVelosity);
    }
}
