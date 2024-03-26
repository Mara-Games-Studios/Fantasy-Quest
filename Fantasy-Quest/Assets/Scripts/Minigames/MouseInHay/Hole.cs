using System.Collections;
using Common;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Minigames.MouseInHay
{
    [AddComponentMenu("Scripts/Minigames/MouseInHay/Minigames.MouseInHay.Hole")]
    internal class Hole : MonoBehaviour
    {
        [ReadOnly]
        [SerializeField]
        private bool isShowed = false;

        [Required]
        [SerializeField]
        private Animator animator;

        [SerializeField]
        private float hideShowAnimationDuration;

        [SerializeField]
        private string isShowedAnimationLabel;

        [SerializeField]
        private string hiddenAnimationStateLabel;

        private Coroutine showCoroutine;

        private void Update()
        {
            animator.SetBool(isShowedAnimationLabel, isShowed);
        }

        public void ShowMouse(float time)
        {
            showCoroutine = StartCoroutine(ShowRoutine(time));
        }

        private IEnumerator ShowRoutine(float time)
        {
            isShowed = true;
            yield return new WaitForSeconds(time - (hideShowAnimationDuration * 2));
            HideMouse();
        }

        public Result TryGrabMouse()
        {
            if (isShowed)
            {
                _ = this.KillCoroutine(showCoroutine);
                HideMouse();
                return new SuccessResult();
            }
            return new FailResult("No mouse in hole");
        }

        public void HideMouse()
        {
            isShowed = false;
        }
    }
}
