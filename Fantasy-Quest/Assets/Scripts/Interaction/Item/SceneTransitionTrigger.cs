using Common.DI;
using Configs;
using UnityEngine;
using UnityEngine.Events;
using VContainer;

namespace Interaction.Item
{
    [AddComponentMenu("Scripts/Interaction/Item/Interaction.Item.SceneTransitionTrigger")]
    internal class SceneTransitionTrigger : InjectingMonoBehaviour, IInteractable
    {
        [Inject]
        private LockerApi lockerSettings;

        [SerializeField]
        private bool ignoreLock = false;
        private bool lockTriggering = false;

        public UnityEvent Triggered;

        // Called by unity events
        public void SetLockTriggering(bool lockTriggering)
        {
            this.lockTriggering = lockTriggering;
        }

        public void InteractionByCat()
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
