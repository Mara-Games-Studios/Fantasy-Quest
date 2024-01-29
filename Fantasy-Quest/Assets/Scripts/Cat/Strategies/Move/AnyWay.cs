using Cat.StateMachine;
using UnityEngine;

namespace Cat.Strategies.Move
{
    public class AnyWay : BaseMovement, IMoveable
    {
        public AnyWay(Transform target, StateMachineData data)
            : base(target, data) { }

        public void Move(Vector2 direction)
        {
            Target.Translate(direction * Time.deltaTime);
        }

        public override void Update()
        {
            base.Update();
            Move(GetConvertedVelocity());
        }
    }
}
