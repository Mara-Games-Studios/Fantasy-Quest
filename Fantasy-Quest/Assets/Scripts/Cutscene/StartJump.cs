using Interaction.Item;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Cutscene
{
    [RequireComponent(typeof(Collider2D))]
    [AddComponentMenu("Scripts/Cutscene/Cutscene.StartJump")]
    internal class StartJump : MonoBehaviour, IJumpTranstition
    {
        [SerializeField]
        private bool canJumpBoth;

        [HideIf("@downJump && !canJumpBoth")]
        [SerializeField]
        private Start upJump;

        [HideIf("@upJump && !canJumpBoth")]
        [SerializeField]
        private Start downJump;

        public void JumpUp()
        {
            if (upJump != null)
            {
                upJump.StartCutscene();
            }
        }

        public void JumpDown()
        {
            if (downJump != null)
            {
                downJump.StartCutscene();
            }
        }
    }
}
