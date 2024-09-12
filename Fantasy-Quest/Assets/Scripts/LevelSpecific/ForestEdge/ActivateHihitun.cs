using Audio;
using Effects;
using Interaction;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LevelSpecific.ForestEdge
{
    [RequireComponent(typeof(Collider2D))]
    [AddComponentMenu("Scripts/LevelSpecific/ForestEdge/LevelSpecific.ForestEdge.ActivateHihitun")]
    internal class ActivateHihitun : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private GameObject objectToShow;

        [Required]
        [SerializeField]
        private Fade hihitunLastPlace;

        [Required]
        [SerializeField]
        private SoundPlayer hihitunLaugh;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out InteractionImpl interaction))
            {
                gameObject.SetActive(false);
                hihitunLastPlace.Disappear();
                hihitunLaugh.PlayClip();
                if (interaction.JumpTrigger.IsJumping)
                {
                    interaction.JumpTrigger.AddOneTimeEndJumpCallback(
                        () => objectToShow.SetActive(true)
                    );
                }
                else
                {
                    objectToShow.SetActive(true);
                }
            }
        }
    }
}
