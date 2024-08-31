using System.Collections.Generic;
using Cutscene;
using Cysharp.Threading.Tasks;
using Dialogue;
using Interaction;
using Sirenix.OdinInspector;
using Spine.Unity;
using UnityEngine;

namespace LevelSpecific.House
{
    [AddComponentMenu("Scripts/LevelSpecific/House/LevelSpecific.House.Bowl")]
    internal class Bowl : MonoBehaviour, IInteractable
    {
        [Required]
        [SerializeField]
        private Start fillMilkCutscene;

        [Required]
        [SerializeField]
        private Start drinkMilkCutscene;

        [Required]
        [SerializeField]
        private Cat.Mewing meowing;

        [SerializeField]
        private SkeletonAnimation skeletonAnimation;

        [SerializeField]
        private AnimationReferenceAsset idleAnimation;

        [SerializeField]
        private AnimationReferenceAsset talkAnimation;

        [SerializeField]
        private List<ChainSpeaker> speakers;

        private bool isMilkFilled = true;
        private bool firstTry = true;
        private bool canTalk = true;

        public void Interact()
        {
            _ = MeowAndDo();
        }

        private async UniTaskVoid MeowAndDo()
        {
            if (firstTry)
            {
                await meowing.CatMeowingTask();
                fillMilkCutscene.StartCutscene();
                firstTry = false;
                return;
            }

            if (isMilkFilled)
            {
                drinkMilkCutscene.StartCutscene();
                isMilkFilled = false;
                return;
            }

            if (!isMilkFilled && canTalk)
            {
                await meowing.CatMeowingTask();
                canTalk = false;
                ChainSpeaker speech = speakers[Random.Range(0, speakers.Count)];
                speech.JustTell();
                await SymonTalkAnimation(speech.Duration);
                canTalk = true;
            }
        }

        private async UniTask SymonTalkAnimation(float talkDuration)
        {
            _ = skeletonAnimation.AnimationState.SetAnimation(0, talkAnimation, true);
            await UniTask.WaitForSeconds(talkDuration);
            _ = skeletonAnimation.AnimationState.SetAnimation(0, idleAnimation, true);
        }
    }
}
