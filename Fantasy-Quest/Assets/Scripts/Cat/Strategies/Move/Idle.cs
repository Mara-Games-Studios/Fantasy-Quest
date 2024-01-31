using Cat.StateMachine;
using UnityEngine;

namespace Cat.Strategies.Move
{
    public class Idle : BaseMovement, IMoveable
    {
        public Idle(Transform target, StateMachineData data)
            : base(target, data) { }

        public void Move(Vector2 direction) { }
    }
}
