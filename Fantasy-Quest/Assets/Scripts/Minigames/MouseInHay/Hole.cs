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
        private string isShowedAnimationLabel;

        [SerializeField]
        private string hiddenAnimationStateLabel;

        private void Update()
        {
            animator.SetBool(isShowedAnimationLabel, isShowed);
        }

        public IEnumerator ShowMouse(float time)
        {
            isShowed = true;
            yield return new WaitForSeconds(time);
            HideMouse();
        }

        public Result TryGrabMouse()
        {
            if (isShowed)
            {
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
