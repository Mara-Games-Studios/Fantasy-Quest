using UnityEngine;

public class NoMove : BaseMovement, IMoveable
{
    public NoMove(Transform targetTransform, StateMashineData data ) : base(targetTransform, data)
    {
    }

    public void Move( Vector2 MoveDirection)
    {
        
    }
}
