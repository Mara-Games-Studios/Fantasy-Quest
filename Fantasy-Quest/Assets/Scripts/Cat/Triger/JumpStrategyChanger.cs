using Cat.Strategies.Jump;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Cat.Trigger
{
    [AddComponentMenu("Scripts/Cat/Trigger/JumpStrategyChanger")]
    public class JumpStrategyChanger : MonoBehaviour
    {
        [Header("Change Jump strategy")]
        [SerializeField]
        private bool changeJumpUp = true;

        [SerializeField]
        private bool changeJumpDown = true;

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
                    ChangeDownJumpStrategy(cat, new NoJump());
                    ChangeUpJumpStrategy(cat, new NoJump());
                }
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
