using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftTriggeer : MonoBehaviour
{
    [SerializeField] private Factory factory;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Cat>(out Cat cat))
        {
            cat.ChangeMovementType(factory.GetMoveType<OnlyRightMove>());
            //cat.ChangeDownJumpType(factory.GetJumpType<DownJump>());
            //cat.ChangeUpJumpType(factory.GetJumpType<UpJump>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Cat>(out Cat cat))
        {
            cat.ChangeMovementType(factory.GetMoveType<Movement>());
            // cat.ChangeDownJumpType(factory.GetJumpType<NoJump>());
        }
    }
}
