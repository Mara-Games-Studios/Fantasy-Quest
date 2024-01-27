using UnityEngine;

public class RightBenchTrigger : MonoBehaviour
{
    [SerializeField]
    private DownJumpConfig downJumpConfig;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Cat>(out Cat cat))
        {
            cat.ChangeMovementType(new OnleLeftMove(cat.transform, cat.StateMashine.Data));
            cat.ChangeDownJumpType(
                new DownJump(cat.transform, downJumpConfig, cat.StateMashine.Data)
            );
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Cat>(out Cat cat))
        {
            cat.ChangeMovementType(new Movement(cat.transform, cat.StateMashine.Data));
            if (cat.GroundChecker.IsTouch)
            {
                cat.ChangeDownJumpType(new NoJump());
            }
        }
    }
}
