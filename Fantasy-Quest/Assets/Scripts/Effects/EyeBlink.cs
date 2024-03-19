using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Effects
{
    [AddComponentMenu("Scripts/Effects/Effects.EyeBlink")]
    internal class EyeBlink : MonoBehaviour, IEffect
    {
        private enum EffectType
        {
            OpenEyes,
            CloseEyes,
            FullBlink
        }

        [SerializeField]
        [Required]
        private Animator anim;

        [SerializeField]
        private EffectType effectType = EffectType.OpenEyes;

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
        public void DoEffect()
        {
            switch (effectType)
            {
                case EffectType.OpenEyes:
                    _ = StartCoroutine(OpenEyesCoroutine());
                    break;
                case EffectType.CloseEyes:
                    _ = StartCoroutine(CloseEyesCoroutine());
                    break;
                case EffectType.FullBlink:
                    _ = StartCoroutine(FullBlinkCoroutine());
                    break;
                default:
                    break;
            }
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
            OnEffectEnded?.Invoke();
        }

        private IEnumerator OpenEyesCoroutine()
        {
            anim.SetTrigger(OPEN);
            yield return new WaitForSeconds(duration);
            OnEffectEnded?.Invoke();
        }

        private IEnumerator FullBlinkCoroutine()
        {
            anim.SetTrigger(CLOSE);
            yield return new WaitForSeconds(duration);
            anim.SetTrigger(OPEN);
            yield return new WaitForSeconds(duration);
            OnEffectEnded?.Invoke();
        }
    }
}
