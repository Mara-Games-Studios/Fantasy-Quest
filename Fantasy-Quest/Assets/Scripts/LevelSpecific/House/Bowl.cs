using System.Collections.Generic;
using Audio;
using Cutscene;
using Cysharp.Threading.Tasks;
using Interaction.Item;
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

        public void InteractionByCat()
        {
            if (firstTry)
            {
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
                canTalk = false;
                SoundPlayer currentSound = symonSpeech[Random.Range(0, symonSpeech.Count)];
                currentSound.PlayClip();
                _ = SymonTalkAnimation(currentSound.AudioClip.length);
            }
        }

        private async UniTaskVoid SymonTalkAnimation(float duration)
        {
            StartSpeakAnimation();
            await UniTask.WaitForSeconds(duration);
            StopSpeakAnimation();
            canTalk = true;
        }

        private void StartSpeakAnimation()
        {
            _ = skeletonAnimation.AnimationState.SetAnimation(0, talkAnimation, true);
        }

        private void StopSpeakAnimation()
        {
            _ = skeletonAnimation.AnimationState.SetAnimation(0, idleAnimation, true);
        }
    }
}
