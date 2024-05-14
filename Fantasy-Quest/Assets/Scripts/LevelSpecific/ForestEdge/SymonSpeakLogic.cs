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
        private Start introCustcene;

        [Required]
        [SerializeField]
        private Start explanationCutscene;

        [Required]
        [SerializeField]
        private Start hintCutscene;

        //[Required]
        //[SerializeField]
        //private Start tryToAltar;

        //[Required]
        //[SerializeField]
        //private Start finilizationCutscene;

        public void Speak()
        {
            if (!ProgressionConfig.Instance.ForestEdgeLevel.FirstDialoguePassed)
            {
                introCustcene.StartCutscene();
                ProgressionConfig.Instance.ForestEdgeLevel.FirstDialoguePassed = true;
                return;
            }
            else if (
                !ProgressionConfig.Instance.ForestEdgeLevel.ExplanationListened
                && ProgressionConfig.Instance.ForestEdgeLevel.BagTaken
            )
            {
                explanationCutscene.StartCutscene();
                ProgressionConfig.Instance.ForestEdgeLevel.ExplanationListened = true;
                return;
            }
            else if (
                !ProgressionConfig.Instance.ForestEdgeLevel.AllItemTaken
                && ProgressionConfig.Instance.ForestEdgeLevel.BagTaken
            )
            {
                hintCutscene.StartCutscene();
                return;
            }
            //else if (
            //    ProgressionConfig.Instance.ForestEdgeLevel.AllItemTaken
            //    && !ProgressionConfig.Instance.ForestEdgeLevel.AltarGamePassed
            //)
            //{
            //    tryToAltar.StartCutscene();
            //    return;
            //}
        }
    }
}
