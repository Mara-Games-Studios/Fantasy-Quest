using UnityEngine;

public class OnlyRightMove : BaseMovement, IMoveable
{
    public OnlyRightMove(Transform targetTransform, StateMashineData data) : base(targetTransform, data)
    {
    }

    public void Move( Vector2 MoveDirection)
    {        
        if (MoveDirection.x > 0 || MoveDirection.y !=0)
        {
            Target.Translate( MoveDirection * Time.deltaTime);
        }
    }

    public override void Update()
    {
        base.Update();
        Move(GetConvertedVelocity());
    }
}
