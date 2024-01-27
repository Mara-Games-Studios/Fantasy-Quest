using UnityEngine;

public class FlorRightSideBench : MonoBehaviour
{
    [SerializeField]
    private UpJumpConfig upJumpConfig;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Cat>(out Cat cat))
        {
            cat.ChangeUpJumpType(new UpJump(cat.transform, upJumpConfig, cat.StateMashine.Data));
            cat.ChangeDownJumpType(new NoJump());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Cat>(out Cat cat))
        {
            if (cat.GroundChecker.IsTouch)
            {
                cat.ChangeUpJumpType(new NoJump());
            }
        }
    }
}
