using Configs.Progression;
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

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (
                collision.TryGetComponent(out Cat.Movement _)
                && !ProgressionConfig.Instance.ForestEdgeLevel.MonsterCutsceneTriggered
            )
            {
                // TODO: create cutscene
                //cutsceneStarter.StartCutscene();
                Debug.Log("Squirrel game unlocked");
                ProgressionConfig.Instance.ForestEdgeLevel.MonsterCutsceneTriggered = true;
            }
        }
    }
}
