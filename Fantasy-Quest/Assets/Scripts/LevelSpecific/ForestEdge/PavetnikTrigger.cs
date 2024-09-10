using Interaction;
using UnityEngine;
using Utils;

namespace LevelSpecific.ForestEdge
{
    [RequireComponent(typeof(Collider2D))]
    [AddComponentMenu("Scripts/LevelSpecific/ForestEdge/LevelSpecific.ForestEdge.PavetnikTrigger")]
    internal class PavetnikTrigger : MonoBehaviour
    {
        [SerializeField]
        private TransformMover beforeEncounter;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out InteractionImpl interaction))
            {
                gameObject.SetActive(false);
                if (interaction.JumpTrigger.IsJumping)
                {
                    interaction.JumpTrigger.AddOneTimeEndJumpCallback(Logic);
                }
                else
                {
                    Logic();
                }
            }
        }

        private void Logic()
        {
            beforeEncounter.Move();
        }
    }
}
