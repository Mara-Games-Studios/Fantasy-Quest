using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    [AddComponentMenu("Scripts/UI/UI.EyeBlink")]
    internal class EyeBlink : MonoBehaviour
    {
        [SerializeField]
        [RequiredIn(PrefabKind.PrefabInstanceAndNonPrefabInstance)]
        private Animator anim;

        [SerializeField]
        private float duration = 1f;

        public UnityEvent EyesClosed;
        public UnityEvent EyesOpened;

        private const string c_SPEEDMULTIPLIER = "SpeedMultiplier";
        private const string c_OPEN = "Open";
        private const string c_CLOSE = "Close";

        private void Awake()
        {
            anim.SetFloat(c_SPEEDMULTIPLIER, 1 / duration);
        }

        [Button]
        public void CloseEyes()
        {
            _ = StartCoroutine(CloseEyesCoroutine());
        }

        [Button]
        public void OpenEyes()
        {
            _ = StartCoroutine(OpenEyesCoroutine());
        }

        [Button]
        public void FullBlink()
        {
            _ = StartCoroutine(FullBlinkCoroutine());
        }

        private IEnumerator CloseEyesCoroutine()
        {
            anim.SetTrigger(c_CLOSE);
            yield return new WaitForSeconds(duration);
            EyesClosed?.Invoke();
        }

        private IEnumerator OpenEyesCoroutine()
        {
            anim.SetTrigger(c_OPEN);
            yield return new WaitForSeconds(duration);
            EyesOpened?.Invoke();
        }

        private IEnumerator FullBlinkCoroutine()
        {
            anim.SetTrigger(c_CLOSE);
            yield return new WaitForSeconds(duration);
            EyesClosed?.Invoke();
            anim.SetTrigger(c_OPEN);
            yield return new WaitForSeconds(duration);
            EyesOpened?.Invoke();
        }
    }
}
