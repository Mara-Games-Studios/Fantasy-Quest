using Effects;
using Interaction;
using UnityEngine;

namespace LevelSpecific.ForestEdge
{
    [RequireComponent(typeof(Collider2D))]
    [AddComponentMenu("Scripts/LevelSpecific/ForestEdge/LevelSpecific.ForestEdge.ActivateHihitun")]
    internal class ActivateHihitun : MonoBehaviour
    {
        [SerializeField]
        private GameObject objectToShow;

        [SerializeField]
        private Fade hihitunLastPlace;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out InteractionImpl interaction))
            {
                gameObject.SetActive(false);
                hihitunLastPlace.Disappear();
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
