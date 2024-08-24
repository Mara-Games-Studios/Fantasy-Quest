using Common.DI;
using Configs;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using VContainer;

namespace Interaction.Item
{
    [AddComponentMenu("Scripts/Interaction/Item/Interaction.Item.SceneTransitionTrigger")]
    internal class SceneTransitionTrigger : InjectingMonoBehaviour, ISceneTransition
    {
        [Inject]
        private LockerApi lockerSettings;

        [SerializeField]
        private bool ignoreLock = false;

        public UnityEvent Triggered;

        [ReadOnly]
        [SerializeField]
        private bool lockTriggering = false;

        [Button]
        public void SetLockTriggering(bool lockTriggering)
        {
            this.lockTriggering = lockTriggering;
        }

        public void ToNewScene()
        {
            if (lockTriggering)
            {
                return;
            }
            if (ignoreLock)
            {
                Triggered?.Invoke();
            }
            else if (!lockerSettings.Api.IsCatInteractionLocked)
            {
                Triggered?.Invoke();
            }
        }
    }
}
