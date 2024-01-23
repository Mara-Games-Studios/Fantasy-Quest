using UnityEngine;

public class RightTrigger : MonoBehaviour
{
    [SerializeField] private Factory factory;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Cat>(out Cat cat))
        {
            cat.ChangeMovementType(factory.GetMoveType<OnleLeftMove>());
            cat.ChangeDownJumpType(factory.GetJumpType<DownJump>());
            //cat.ChangeUpJumpType(factory.GetJumpType<UpJump>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Cat>(out Cat cat))
        {
            cat.ChangeMovementType(factory.GetMoveType<Movement>());
            cat.ChangeDownJumpType(factory.GetJumpType<NoJump>());
        }
    }
}
