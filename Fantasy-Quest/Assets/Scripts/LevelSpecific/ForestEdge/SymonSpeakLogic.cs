using Common.DI;
using Configs.Progression;
using Cutscene;
using Cysharp.Threading.Tasks;
using Dialogue;
using Sirenix.OdinInspector;
using Symon;
using UnityEngine;

namespace LevelSpecific.ForestEdge
{
    [AddComponentMenu("Scripts/LevelSpecific/ForestEdge/LevelSpecific.ForestEdge.SymonSpeakLogic")]
    internal class SymonSpeakLogic : InjectingMonoBehaviour
    {
        [Required]
        [SerializeField]
        private Start introCutscene;

        [Required]
        [SerializeField]
        private Start explanationCutscene;

        [Required]
        [SerializeField]
        private ChainSpeaker theeItemsSpeaker;

        [Required]
        [SerializeField]
        private ChainSpeaker whereBagSpeaker;

        [Required]
        [SerializeField]
        private SkeletonManager symonSkeletonManager;

        [Required]
        [SerializeField]
        private ToCatTurner toCatTurner;
        private bool isTalking = false;

        private ForestEdgeLevel EdgeConfig => ProgressionConfig.Instance.ForestEdgeLevel;

        // Called by trigger
        public void Speak()
        {
            if (!EdgeConfig.FirstDialoguePassed)
            {
                introCutscene.StartCutscene();
                EdgeConfig.FirstDialoguePassed = true;
                return;
            }

            if (EdgeConfig.FirstDialoguePassed && !EdgeConfig.BagTaken && !isTalking)
            {
                _ = WhereBagTalk();
                return;
            }

            if (!EdgeConfig.ExplanationListened && EdgeConfig.BagTaken)
            {
                explanationCutscene.StartCutscene();
                EdgeConfig.ExplanationListened = true;
                return;
            }

            if (!EdgeConfig.AllItemTaken && EdgeConfig.BagTaken)
            {
                _ = Find3ItemsBagTalk();
                return;
            }
        }

        private async UniTask WhereBagTalk()
        {
            isTalking = true;
            symonSkeletonManager.TellDownWithBread(whereBagSpeaker.Duration);
            await whereBagSpeaker.JustTellRoutine();
            isTalking = false;
        }

        private async UniTask Find3ItemsBagTalk()
        {
            isTalking = true;
            symonSkeletonManager.TellDownWithBackPack(theeItemsSpeaker.Duration);
            await theeItemsSpeaker.JustTellRoutine();
            isTalking = false;
        }
    }
}
