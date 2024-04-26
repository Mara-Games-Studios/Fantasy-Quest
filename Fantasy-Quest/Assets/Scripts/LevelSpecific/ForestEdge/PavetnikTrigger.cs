using Configs.Progression;
using Cutscene;
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

        [SerializeField]
        private Start winEncounter;

        private bool isEncountred = false;

        private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
        {
            if (collision.TryGetComponent<InteractionImpl>(out _))
            {
                if (!isEncountred)
                {
                    beforeEncounter.Move();
                    isEncountred = true;
                }
                else if (
                    isEncountred && ProgressionConfig.Instance.ForestEdgeLevel.MouseInHayGamePassed
                )
                {
                    winEncounter.StartCutscene();
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
