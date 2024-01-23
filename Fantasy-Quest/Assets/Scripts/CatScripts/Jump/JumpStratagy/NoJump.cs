using UnityEngine;

public class NoJump : IJumpable
{
    protected Transform Target;
    protected StateMashineData Data;

    public NoJump(Transform targetTransform, StateMashineData data)
    {
        Target = targetTransform;
        this.Data = data;
    }

    public void Jump(StateMashineData data, float xStartVeocity, float yStartVeocity)
    {
        
    }

    public void Update()
    {
        
    }

    protected Vector2 GetConvertedVelocity()
    {
        return new Vector2(Data.XVelosity, Data.YVelosity);
    }
}
