using System.Collections;
using Effects;
using Effects.Screen;
using Sirenix.OdinInspector;
using TNRD;
using UnityEngine;
using UnityEngine.Events;

namespace Transition
{
    [AddComponentMenu("Scripts/Transition/Transition.Location")]
    internal class Location : MonoBehaviour
    {
        [SerializeField]
        private bool playSecondEffect = true;

        [MinValue(0)]
        [SerializeField]
        private float transitionTime = 1f;

        [RequiredIn(PrefabKind.PrefabInstanceAndNonPrefabInstance)]
        [SerializeField]
        private SerializableInterface<IEffect> inEffect;
        private IEffect InEffect => inEffect.Value;

        [SerializeField]
        private Transform inPoint;

        [RequiredIn(PrefabKind.PrefabInstanceAndNonPrefabInstance)]
        [SerializeField]
        private SerializableInterface<IEffect> outEffect;
        private IEffect OutEffect => outEffect.Value;

        [SerializeField]
        private Transform outPoint;

        public UnityEvent InEffectStarted;
        public UnityEvent InEffectEnded;
        public UnityEvent OutEffectEnded;

        public void TriggerTransition()
        {
            InEffectStarted?.Invoke();
            InEffect.OnEffectEnded += OnInEffectEnded;
            OutEffect.OnEffectEnded += OnOutEffectEnded;
            if (inPoint != null && InEffect is CutoutMask cutout)
            {
                cutout.SetDestinationPoint(inPoint);
            }
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
            if (playSecondEffect)
            {
                if (outPoint != null && OutEffect is CutoutMask cutout)
                {
                    cutout.SetDestinationPoint(outPoint);
                }
                OutEffect.DoEffect();
            }
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
