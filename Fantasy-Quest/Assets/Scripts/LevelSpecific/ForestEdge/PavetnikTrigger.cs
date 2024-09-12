using Interaction;
using Sirenix.OdinInspector;
using UnityEngine;
using Utils;

namespace LevelSpecific.ForestEdge
{
    [RequireComponent(typeof(Collider2D))]
    [AddComponentMenu("Scripts/LevelSpecific/ForestEdge/LevelSpecific.ForestEdge.PavetnikTrigger")]
    internal class PavetnikTrigger : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private TransformMover beforeEncounter;

        [Required]
        [SerializeField]
        private ChickensPlaceChanger chickensPlaceChanger;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out InteractionImpl interaction))
            {
                if (interaction.JumpTrigger.IsJumping)
                {
                    interaction.JumpTrigger.AddOneTimeEndJumpCallback(() => Logic());
                }
                else
                {
                    Logic();
                }
            }
        }

        private void Logic()
        {
            chickensPlaceChanger.ShowChickensRunningFromBarn();
            beforeEncounter.Move();
        }
    }
}
