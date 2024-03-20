using Configs.Progression;
using Cutscene;
using Dialogue;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LevelSpecific.ForestEdge
{
    [AddComponentMenu("Scripts/LevelSpecific/ForestEdge/LevelSpecific.ForestEdge.SymonSpeakLogic")]
    internal class SymonSpeakLogic : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private ChainSpeaker introductionSpeak;

        [Required]
        [SerializeField]
        private Start cutsceneStarter;

        public void Speak()
        {
            if (!ProgressionConfig.Instance.ForestEdgeLevel.FirstDialoguePassed)
            {
                cutsceneStarter.StartCutscene();
                ProgressionConfig.Instance.ForestEdgeLevel.FirstDialoguePassed = true;
                gameObject.SetActive(false);
                return;
            }
        }
    }
}
