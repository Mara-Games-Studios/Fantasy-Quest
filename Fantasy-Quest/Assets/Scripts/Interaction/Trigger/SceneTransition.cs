using Common.DI;
using Configs;
using UnityEngine;
using UnityEngine.Events;
using VContainer;

namespace Interaction.Trigger
{
    [RequireComponent(typeof(Collider2D))]
    [AddComponentMenu("Scripts/Interaction/Trigger/Interaction.Trigger.SceneTransition")]
    internal class SceneTransition : InjectingMonoBehaviour, IInteractable
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

        public void Interact()
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
