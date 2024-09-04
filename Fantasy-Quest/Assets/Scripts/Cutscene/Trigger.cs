using Interaction;
using UnityEngine;

namespace Cutscene
{
    [RequireComponent(typeof(Collider2D))]
    [AddComponentMenu("Scripts/Cutscene/Cutscene.Trigger")]
    internal class Trigger : MonoBehaviour
    {
        [SerializeField]
        private Start starter;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out InteractionImpl interaction))
            {
                gameObject.SetActive(false);
                if (interaction.JumpTrigger.IsJumping)
                {
                    interaction.JumpTrigger.AddOneTimeEndJumpCallback(
                        () => starter.StartCutscene()
                    );
                }
                else
                {
                    starter.StartCutscene();
                }
            }
        }
    }
}
