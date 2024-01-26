using UnityEngine;

public class Movement : BaseMovement, IMoveable
{
    public Movement(Transform targetTransform, StateMashineData data) : base(targetTransform, data)
    {
    }

    public void Move(Vector2 moveDirection)
    {
        Target.Translate(moveDirection * Time.deltaTime);
    }

    public override void Update()
    {
        base.Update();
        Move(GetConvertedVelocity());
    }
}
