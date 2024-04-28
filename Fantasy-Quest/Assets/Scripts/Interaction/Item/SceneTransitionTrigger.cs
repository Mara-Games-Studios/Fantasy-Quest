using Configs;
using UnityEngine;
using UnityEngine.Events;

namespace Interaction.Item
{
    [AddComponentMenu("Scripts/Interaction/Item/Interaction.Item.SceneTransitionTrigger")]
    internal class SceneTransitionTrigger : MonoBehaviour, ISceneTransition
    {
        [SerializeField]
        private bool ignoreLock = false;

        public UnityEvent Triggered;

        public void ToNewScene()
        {
            if (ignoreLock)
            {
                Triggered?.Invoke();
            }
            else if (!LockerSettings.Instance.IsCatInteractionLocked)
            {
                Triggered?.Invoke();
            }
        }
    }
}
