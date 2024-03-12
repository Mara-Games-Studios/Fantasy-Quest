using System.Collections;
using Cinemachine;
using Configs;
using Effects;
using Interaction.Item;
using Sirenix.OdinInspector;
using TNRD;
using UnityEngine;

namespace Levels.SublevelSwitchers
{
    [AddComponentMenu("Scripts/Levels/SublevelSwitchers/Levels.SublevelSwitchers.ToBarn")]
    internal class ToBarn : MonoBehaviour, ISceneTransition
    {
        [Required]
        [SerializeField]
        private Cat.Movement catMovement;

        [Required]
        [SerializeField]
        private Rails.Point railsPointToBind;

        [Required]
        [SerializeField]
        private CinemachineVirtualCamera virtualCamera;

        [SerializeField]
        private int newCameraPriority = 1000;

        [SerializeField]
        private float transitionTime = 2f;

        [Required]
        [SerializeField]
        private SerializableInterface<IEffect> inEffect;
        private IEffect InEffect => inEffect.Value;

        [Required]
        [SerializeField]
        private SerializableInterface<IEffect> outEffect;
        private IEffect OutEffect => outEffect.Value;

        private void Awake()
        {
            InEffect.OnEffectEnded += OnInEffectEnded;
            OutEffect.OnEffectEnded += OnOutEffectEnded;
        }

        public void ToNewScene()
        {
            LockerSettings.Instance.LockAll();
            InEffect.DoEffect();
        }

        public void OnInEffectEnded()
        {
            virtualCamera.Priority = newCameraPriority;
            catMovement.RemoveFromRails();
            catMovement.SetOnRails(railsPointToBind);
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
            LockerSettings.Instance.UnlockAll();
        }
    }
}
