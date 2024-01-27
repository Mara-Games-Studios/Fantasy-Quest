using Cat;
using Cat.Strategies.Jump;
using Cat.Strategies.Move;
using Sirenix.OdinInspector;
using UnityEngine;

public class RightBenchTrigger : MonoBehaviour
{
    [SerializeField]
    private bool changeHJHJH = true;

    [ShowIf(nameof(changeHJHJH))]
    [SerializeField]
    private DownJumpConfig downJumpConfig;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<CatImpl>(out CatImpl cat))
        {
            cat.ChangeMovementType(new OnlyLeft(cat.transform, cat.StateMachine.Data));
            cat.ChangeDownJumpType(new Down(cat.transform, downJumpConfig, cat.StateMachine.Data));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<CatImpl>(out CatImpl cat))
        {
            cat.ChangeMovementType(new AnyWay(cat.transform, cat.StateMachine.Data));
            if (cat.GroundChecker.IsTouch)
            {
                cat.ChangeDownJumpType(new NoJump());
            }
        }
    }
}
