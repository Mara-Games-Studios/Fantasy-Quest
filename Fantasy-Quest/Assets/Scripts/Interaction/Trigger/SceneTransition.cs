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

        public UnityEvent Triggered;

        public void Interact()
        {
            if (!lockerSettings.Api.IsCatInteractionLocked)
            {
                Triggered?.Invoke();
            }
        }
    }
}
