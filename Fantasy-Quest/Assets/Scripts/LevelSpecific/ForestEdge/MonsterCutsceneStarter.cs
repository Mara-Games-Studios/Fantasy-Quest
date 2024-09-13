using Configs.Progression;
using Interaction;
using UnityEngine;

namespace LevelSpecific.ForestEdge
{
    [AddComponentMenu(
        "Scripts/LevelSpecific/ForestEdge/LevelSpecific.ForestEdge.MonsterCutsceneStarter"
    )]
    internal class MonsterCutsceneStarter : MonoBehaviour
    {
        [SerializeField]
        private Cutscene.Start cutsceneStarter;

        private ForestEdgeLevel EdgeConfig => ProgressionConfig.Instance.ForestEdgeLevel;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (
                collision.TryGetComponent(out InteractionImpl interaction)
                && !EdgeConfig.MonsterCutsceneTriggered
                && EdgeConfig.ExplanationListened
            )
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
            cutsceneStarter.StartCutscene();
            EdgeConfig.MonsterCutsceneTriggered = true;
        }
    }
}
