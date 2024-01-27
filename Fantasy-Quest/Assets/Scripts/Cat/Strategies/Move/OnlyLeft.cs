using Cat.StateMachine;
using UnityEngine;

namespace Cat.Strategies.Move
{
    public class OnlyLeft : BaseMovement, IMoveable
    {
        public OnlyLeft(Transform target, StateMachineData data)
            : base(target, data) { }

        public void Move(Vector2 direction)
        {
            if (direction.x < 0 || direction.y != 0)
            {
                Target.Translate(direction * Time.deltaTime);
            }
        }

        public override void Update()
        {
            base.Update();
            Move(GetConvertedVelocity());
        }
    }
}
