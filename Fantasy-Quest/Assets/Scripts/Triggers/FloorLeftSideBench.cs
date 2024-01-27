using Cat;
using Cat.Strategies.Move;
using UnityEngine;

namespace Triggers
{
    [AddComponentMenu("Scripts/Triggers/Triggers")]
    public class FloorLeftSideBench : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out CatImpl cat))
            {
                cat.ChangeMovementType(new OnlyRight(cat.transform, cat.StateMachine.Data));
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out CatImpl cat))
            {
                cat.ChangeMovementType(new AnyWay(cat.transform, cat.StateMachine.Data));
            }
        }
    }
}
