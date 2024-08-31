using System.Collections.Generic;
using Audio;
using Cutscene;
using Cysharp.Threading.Tasks;
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

        [Required]
        [SerializeField]
        private List<SoundPlayer> symonSpeech;

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
                SoundPlayer currentSound = symonSpeech[Random.Range(0, symonSpeech.Count)];
                currentSound.PlayClip();
                await SymonTalkAnimation(currentSound);
                canTalk = true;
            }
        }

        private async UniTask SymonTalkAnimation(SoundPlayer currentSound)
        {
            _ = skeletonAnimation.AnimationState.SetAnimation(0, talkAnimation, true);
            await UniTask.WaitForSeconds(currentSound.AudioClip.length);
            _ = skeletonAnimation.AnimationState.SetAnimation(0, idleAnimation, true);
        }
    }
}
