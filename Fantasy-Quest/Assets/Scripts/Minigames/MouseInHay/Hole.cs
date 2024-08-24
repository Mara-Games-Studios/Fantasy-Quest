using System.Collections;
using Common;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

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

        public UnityEvent OnHoleHit;

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
            yield return new WaitForSeconds(time);
            HideMouse();
        }

        public bool TryGrabMouse()
        {
            OnHoleHit.Invoke();
            if (isShowed)
            {
                _ = this.KillCoroutine(showCoroutine);
                HideMouse();
                return true;
            }
            return false;
        }

        public void HideMouse()
        {
            isShowed = false;
        }
    }
}
