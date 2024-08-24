using System.Collections;
using System.Collections.Generic;
using Audio;
using Interaction.Item;
using Sirenix.OdinInspector;
using Spine.Unity;
using UnityEngine;

namespace Cutscene
{
    //After first cutscene played, script switches icon hint and on new interactable button plays new cutscene
    [AddComponentMenu("Scripts/Cutscene/Cutscene.CutsceneInteractTransition")]
    internal class CutsceneInteractTransition : MonoBehaviour, IInteractable
    {
        [InfoBox("CALLED BY E AND CALLED BY 1")]
        [Required]
        [SerializeField]
        private Start startCat;

        [Required]
        [SerializeField]
        private Start startHuman;

        [Required]
        [SerializeField]
        private GameObject hint;

        [Required]
        [SerializeField]
        private Sprite newIcon;

        [SerializeField]
        private SkeletonAnimation skeletonAnimation;

        [SerializeField]
        private AnimationReferenceAsset idleAniamtion;

        [SerializeField]
        private AnimationReferenceAsset talkAnimation;

        [Required]
        [SerializeField]
        private List<SoundPlayer> symonSpeech;

        [SerializeField, ReadOnly]
        private bool isMilkEmpty = false;

        [SerializeField, ReadOnly]
        private bool canCatInteract;

        [SerializeField, ReadOnly]
        private bool canTalk = true;

        public void InteractByCat()
        {
            if (canCatInteract && !isMilkEmpty)
            {
                startCat.StartCutscene();
                isMilkEmpty = true;
            }
            else if (isMilkEmpty && canTalk)
            {
                canTalk = false;
                SoundPlayer currentSound = symonSpeech[Random.Range(0, symonSpeech.Count)];
                currentSound.PlayClip();
                _ = StartCoroutine(SymonTalkAnimation(currentSound.AudioClip.length));
            }
        }

        public void InteractByHuman()
        {
            if (!canCatInteract)
            {
                startHuman.StartCutscene();
                if (hint != null)
                {
                    ChangeShortcutIcon();
                }

                canCatInteract = true;
            }
        }

        private IEnumerator SymonTalkAnimation(float duration)
        {
            StartSpeakAnimation();
            yield return new WaitForSeconds(duration);
            canTalk = true;
            StopSpeakAnimation();
        }

        private void ChangeShortcutIcon()
        {
            hint.GetComponent<SpriteRenderer>().sprite = newIcon;
        }

        protected void StartSpeakAnimation()
        {
            _ = skeletonAnimation.AnimationState.SetAnimation(0, talkAnimation, true);
        }

        protected void StopSpeakAnimation()
        {
            _ = skeletonAnimation.AnimationState.SetAnimation(0, idleAniamtion, true);
        }
    }
}
