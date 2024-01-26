using UnityEngine;

public class BaseMovement 
{
    protected Transform Target;
    protected StateMashineData Data;

    public BaseMovement(Transform targetTransform, StateMashineData data)
    {
        Target = targetTransform;
        this.Data = data;
    }

    private bool IsCatRotateRight => CatXScale > 0;
    private Vector3 TurnRight => new Vector3(Mathf.Abs(CatXScale), CatYScale, CatZScale);
    private Vector3 TurnLeft => new Vector3(CatXScale * (-1), CatYScale, CatZScale);
    private float CatXScale => Target.localScale.x;
    private float CatYScale => Target.localScale.y;
    private float CatZScale => Target.localScale.z;

    public virtual void Update()
    {        
        Data.XVelosity = Data.XInput * Data.Speed;

        Vector2 velocity = GetConvertedVelocity();
        GetRotation(velocity);
    }

    private void GetRotation(Vector2 velocity)
    {
        if (velocity.x > 0)
        {
            if (!IsCatRotateRight)
            {
                Target.localScale = TurnRight;
            }
        }

        if (velocity.x < 0)
        {
            if (IsCatRotateRight)
            {
                Target.transform.localScale = TurnLeft;
            }
        }
    }

    protected Vector2 GetConvertedVelocity()
    {
        return new Vector2(Data.XVelosity, Data.YVelosity);
    }
}
