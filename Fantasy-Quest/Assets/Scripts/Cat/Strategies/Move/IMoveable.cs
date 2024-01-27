using UnityEngine;

namespace Cat.Strategies.Move
{
    public interface IMoveable
    {
        void Move(Vector2 direction);
        void Update();
    }
}
