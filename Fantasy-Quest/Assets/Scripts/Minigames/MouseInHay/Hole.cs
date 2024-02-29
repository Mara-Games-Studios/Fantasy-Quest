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
        private Coroutine showCoroutine;
        public UnityEvent OnMouseShowed;
        public UnityEvent OnMouseHidden;

        public void ShowMouse(float time)
        {
            isShowed = true;
            OnMouseShowed?.Invoke();
            showCoroutine = StartCoroutine(ShowRoutine(time));
        }

        private IEnumerator ShowRoutine(float time)
        {
            yield return new WaitForSeconds(time);
            EndShowMouse();
        }

        public Result TryGrabMouse()
        {
            if (isShowed)
            {
                StopCoroutine(showCoroutine);
                EndShowMouse();
                return new SuccessResult();
            }
            return new FailResult("No mouse in hole");
        }

        private void EndShowMouse()
        {
            OnMouseHidden?.Invoke();
            isShowed = false;
        }
    }
}
