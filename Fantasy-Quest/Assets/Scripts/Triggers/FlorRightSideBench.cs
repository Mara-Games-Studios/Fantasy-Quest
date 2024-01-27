using Cat;
using Cat.Strategies.Jump;
using UnityEngine;

public class FlorRightSideBench : MonoBehaviour
{
    [SerializeField]
    private UpJumpConfig upJumpConfig;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<CatImpl>(out CatImpl cat))
        {
            cat.ChangeUpJumpType(new Up(cat.transform, upJumpConfig, cat.StateMachine.Data));
            cat.ChangeDownJumpType(new NoJump());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<CatImpl>(out CatImpl cat))
        {
            if (cat.GroundChecker.IsTouch)
            {
                cat.ChangeUpJumpType(new NoJump());
            }
        }
    }
}
