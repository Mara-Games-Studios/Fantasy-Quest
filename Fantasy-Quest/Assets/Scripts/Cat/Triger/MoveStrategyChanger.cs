using Cat.Strategies.Move;
using UnityEngine;

namespace Cat.Trigger
{
    [AddComponentMenu("Scripts/Cat/Trigger/MoveStrategyChanger")]
    public class MoveStrategyChanger : MonoBehaviour
    {
        [Header("Change Move strategy")]
        [SerializeField]
        private bool changeAnyWayMove = true;

        [SerializeField]
        private bool changeOnlyLeftMove = true;

        [SerializeField]
        private bool changeOnlyRightMove = true;

        [SerializeField]
        private bool changeIdle = true;

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out CatImpl cat))
            {
                if (cat.GroundChecker.IsTouch)
                {
                    ChangeMove(cat);
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out CatImpl cat))
            {
                if (cat.GroundChecker.IsTouch)
                {
                    ChangeMoveType(cat, new AnyWay(cat.transform, cat.StateMachine.Data));
                }
            }
        }

        private void ChangeMove(CatImpl cat)
        {
            if (changeAnyWayMove == true)
            {
                ChangeMoveType(cat, new AnyWay(cat.transform, cat.StateMachine.Data));
            }
            else if (changeOnlyLeftMove == true)
            {
                ChangeMoveType(cat, new OnlyLeft(cat.transform, cat.StateMachine.Data));
            }
            else if (changeOnlyRightMove == true)
            {
                ChangeMoveType(cat, new OnlyRight(cat.transform, cat.StateMachine.Data));
            }
            else if (changeIdle == true)
            {
                ChangeMoveType(cat, new Idle(cat.transform, cat.StateMachine.Data));
            }
        }

        private void ChangeMoveType(CatImpl cat, IMoveable moveStrategy)
        {
            cat.ChangeMovementType(moveStrategy);
        }
    }
}
