using Cat.Strategies.Jump;
using Cat.Strategies.Move;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Cat.Trigger
{
    [AddComponentMenu("Scripts/Cat/Trigger/CatStrategyChangerTriger")]
    public class CatStrategyChangerTriger : MonoBehaviour
    {
        [Header("Change Jump strategy")]
        [SerializeField]
        private bool changeJumpUp = true;

        [SerializeField]
        private bool changeJumpDown = true;

        [Header("Change Move strategy")]
        [SerializeField]
        private bool changeAnyWayMove = true;

        [SerializeField]
        private bool changeOnlyLeftMove = true;

        [SerializeField]
        private bool changeOnlyRightMove = true;

        [SerializeField]
        private bool changeIdle = true;

        [Header("Set Jump strategy")]
        [ShowIf(nameof(changeJumpDown))]
        [SerializeField]
        private DownJumpConfig downJumpConfig;

        [ShowIf(nameof(changeJumpUp))]
        [SerializeField]
        private UpJumpConfig upJumpConfig;

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out CatImpl cat))
            {
                if (cat.GroundChecker.IsTouch)
                {
                    ChangeMove(cat);
                    ChangeJump(cat);
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
                    ChangeDownJumpStrategy(cat, new NoJump());
                    ChangeUpJumpStrategy(cat, new NoJump());
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

        private void ChangeJump(CatImpl cat)
        {
            if (changeJumpDown == true)
            {
                ChangeDownJumpStrategy(
                    cat,
                    new Down(cat.transform, downJumpConfig, cat.StateMachine.Data)
                );
            }
            else
            {
                ChangeDownJumpStrategy(cat, new NoJump());
            }

            if (changeJumpUp == true)
            {
                ChangeUpJumpStrategy(
                    cat,
                    new Up(cat.transform, upJumpConfig, cat.StateMachine.Data)
                );
            }
            else
            {
                ChangeUpJumpStrategy(cat, new NoJump());
            }
        }

        private void ChangeMoveType(CatImpl cat, IMoveable moveStrategy)
        {
            cat.ChangeMovementType(moveStrategy);
        }

        private void ChangeDownJumpStrategy(CatImpl cat, IJumpable jumpStrategy)
        {
            cat.ChangeDownJumpType(jumpStrategy);
        }

        private void ChangeUpJumpStrategy(CatImpl cat, IJumpable jumpStrategy)
        {
            cat.ChangeUpJumpType(jumpStrategy);
        }
    }
}
