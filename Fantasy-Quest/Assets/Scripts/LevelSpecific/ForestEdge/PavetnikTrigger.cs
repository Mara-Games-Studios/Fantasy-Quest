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

        private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
        {
            if (collision.TryGetComponent<InteractionImpl>(out _))
            {

                beforeEncounter.Move();
            }
        }
    }
}
