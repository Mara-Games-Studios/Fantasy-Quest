using Configs.Progression;
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

        public void Speak()
        {
            if (!ProgressionConfig.Instance.ForestEdgeLevel.FirstDialoguePassed)
            {
                introductionSpeak.Tell(() => { });
                ProgressionConfig.Instance.ForestEdgeLevel.FirstDialoguePassed = true;
                return;
            }
        }
    }
}
