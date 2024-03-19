using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Effects
{
    [AddComponentMenu("Scripts/Effects/Effects.EyeBlink")]
    internal class EyeBlink : MonoBehaviour, IEffect
    {
        [SerializeField]
        [Required]
        private Animator anim;

        [SerializeField]
        private float duration = 1f;

        private const string SPEEDMULTIPLIER = "SpeedMultiplier";
        private const string OPEN = "Open";
        private const string CLOSE = "Close";
        private const string DEFAULT = "Default";

        public event Action OnEffectEnded;

        private void Awake()
        {
            anim.SetFloat(SPEEDMULTIPLIER, 1 / duration);
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
        public void DoEffect()
        {
            _ = StartCoroutine(FullBlinkCoroutine());
        }

        [Button]
        public void RefreshEffect()
        {
            anim.SetTrigger(DEFAULT);
        }

        private IEnumerator CloseEyesCoroutine()
        {
            anim.SetTrigger(CLOSE);
            yield return new WaitForSeconds(duration);
        }

        private IEnumerator OpenEyesCoroutine()
        {
            anim.SetTrigger(OPEN);
            yield return new WaitForSeconds(duration);
        }

        private IEnumerator FullBlinkCoroutine()
        {
            anim.SetTrigger(CLOSE);
            yield return new WaitForSeconds(duration);
            anim.SetTrigger(OPEN);
            yield return new WaitForSeconds(duration);
        }
    }
}
