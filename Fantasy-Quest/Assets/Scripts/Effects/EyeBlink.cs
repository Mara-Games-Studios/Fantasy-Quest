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

        private const string SPEED_MULTIPLIER = "SpeedMultiplier";
        private const string OPEN_ANIM = "Open";
        private const string CLOSE_ANIM = "Close";
        private const string DEFAULT_ANIM = "Default";

        public event Action OnEffectEnded;

        private void Awake()
        {
            anim.SetFloat(SPEED_MULTIPLIER, 1 / duration);
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
            anim.SetTrigger(DEFAULT_ANIM);
        }

        private IEnumerator CloseEyesCoroutine()
        {
            anim.SetTrigger(CLOSE_ANIM);
            yield return new WaitForSeconds(duration);
            OnEffectEnded?.Invoke();
        }

        private IEnumerator OpenEyesCoroutine()
        {
            anim.SetTrigger(OPEN_ANIM);
            yield return new WaitForSeconds(duration);
            OnEffectEnded?.Invoke();
            anim.SetTrigger(DEFAULT_ANIM);
        }

        private IEnumerator FullBlinkCoroutine()
        {
            anim.SetTrigger(CLOSE_ANIM);
            yield return new WaitForSeconds(duration);
            anim.SetTrigger(OPEN_ANIM);
            yield return new WaitForSeconds(duration);
            OnEffectEnded?.Invoke();
            anim.SetTrigger(DEFAULT_ANIM);
        }
    }
}
