using Interaction.Item;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Cutscene
{
    [RequireComponent(typeof(BoxCollider2D))]
    [AddComponentMenu("Scripts/Cutscene/Cutscene.StartJump")]
    internal class StartJump : MonoBehaviour, IJumpTranstition
    {
        [SerializeField]
        private bool canJumpBoth;

        [SerializeField, HideIf("@downJump && !canJumpBoth")]
        private Start upJump;

        [SerializeField, HideIf("@upJump && !canJumpBoth")]
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
