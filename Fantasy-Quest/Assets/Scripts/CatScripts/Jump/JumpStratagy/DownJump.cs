using UnityEngine;

public class DownJump :  IJumpable
{
    protected Transform Target;
    protected StateMashineData Data;

    public DownJump(Transform targetTransform, StateMashineData data)
    {
        Target = targetTransform;
        this.Data = data;
    }

    public void Jump(StateMashineData data, float xStartVeocity, float yStartVeocity)
    {
        data.XVelosity = xStartVeocity * (Target.localScale.x) / (Mathf.Abs(Target.localScale.x));
        data.YVelosity = yStartVeocity;
    }

    public  void Update()
    {
        Target.Translate(GetConvertedVelocity() * Time.deltaTime);
    }

    protected Vector2 GetConvertedVelocity()
    {
        //return new Vector2(Mathf.Abs(Data.XVelosity), Data.YVelosity);
        return new Vector2(Data.XVelosity, Data.YVelosity);
    }
}
