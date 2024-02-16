using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(
        fileName = "Transition Settings",
        menuName = "Settings/Create Transition Settings"
    )]
    internal class TransitionSettings : SingletonScriptableObject<TransitionSettings>
    {
        [Min(0)]
        [SerializeField]
        private float minLoadingDuration;
        public float MinLoadingDuration => minLoadingDuration;
    }
}
