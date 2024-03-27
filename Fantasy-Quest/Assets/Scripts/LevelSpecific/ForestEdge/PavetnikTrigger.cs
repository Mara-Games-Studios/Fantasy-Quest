using Configs.Progression;
using Cutscene;
using Interaction;
using UnityEngine;

namespace LevelSpecific.ForestEdge
{
    [RequireComponent(typeof(Collider2D))]
    [AddComponentMenu("Scripts/LevelSpecific/ForestEdge/LevelSpecific.ForestEdge.PavetnikTrigger")]
    internal class PavetnikTrigger : MonoBehaviour
    {
        [SerializeField]
        private Start firstEncounter;

        [SerializeField]
        private Start winEncounter;

        private bool isEncountred = false;

        private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
        {
            if (collision.TryGetComponent<InteractionImpl>(out _))
            {
                if (!isEncountred)
                {
                    firstEncounter.StartCutscene();
                    isEncountred = true;
                }
                else if (
                    isEncountred && ProgressionConfig.Instance.ForestEdgeLevel.MouseInHayGamePassed
                )
                {
                    winEncounter.StartCutscene();
                    Destroy(gameObject);
                }
            }
        }
    }
}
