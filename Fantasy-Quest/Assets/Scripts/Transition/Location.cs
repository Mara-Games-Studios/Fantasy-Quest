using System.Collections;
using Effects;
using Sirenix.OdinInspector;
using TNRD;
using UnityEngine;
using UnityEngine.Events;

namespace Transition
{
    [AddComponentMenu("Scripts/Transition/Transition.Location")]
    internal class Location : MonoBehaviour
    {
        [MinValue(0)]
        [SerializeField]
        private float transitionTime = 1f;

        [RequiredIn(PrefabKind.PrefabInstanceAndNonPrefabInstance)]
        [SerializeField]
        private SerializableInterface<IEffect> inEffect;
        private IEffect InEffect => inEffect.Value;

        [RequiredIn(PrefabKind.PrefabInstanceAndNonPrefabInstance)]
        [SerializeField]
        private SerializableInterface<IEffect> outEffect;
        private IEffect OutEffect => outEffect.Value;

        public UnityEvent InEffectStarted;
        public UnityEvent InEffectEnded;
        public UnityEvent OutEffectEnded;

        public void TriggerTransition()
        {
            InEffectStarted?.Invoke();
            InEffect.OnEffectEnded += OnInEffectEnded;
            OutEffect.OnEffectEnded += OnOutEffectEnded;
            InEffect.DoEffect();
        }

        public void OnInEffectEnded()
        {
            InEffectEnded?.Invoke();
            _ = StartCoroutine(WaitRoutine(transitionTime));
        }

        private IEnumerator WaitRoutine(float time)
        {
            yield return new WaitForSeconds(time);
            InEffect.RefreshEffect();
            OutEffect.DoEffect();
        }

        private void OnOutEffectEnded()
        {
            OutEffect.RefreshEffect();
            InEffect.OnEffectEnded -= OnInEffectEnded;
            OutEffect.OnEffectEnded -= OnOutEffectEnded;
            OutEffectEnded?.Invoke();
        }
    }
}
