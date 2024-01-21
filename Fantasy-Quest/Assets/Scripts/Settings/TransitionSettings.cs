using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(
        fileName = "Transition Settings",
        menuName = "Settings/Create Transition Settings",
        order = 1
    )]
    internal class TransitionSettings : SingletonScriptableObject<TransitionSettings>
    {
        [Min(0)]
        [SerializeField]
        private float minLoadingDuration;
        public float MinLoadingDuration => minLoadingDuration;
    }
}
