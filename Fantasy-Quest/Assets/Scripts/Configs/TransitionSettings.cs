using System.Collections.Generic;
using TNRD;
using UI;
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
        private float loadingDuration;
        public float LoadingDuration => loadingDuration;

        [Min(0)]
        [SerializeField]
        private float fadingDuration;
        public float FadingDuration => fadingDuration;

        [SerializeField]
        private int currentFilling = 0;
        public int CurrentFilling
        {
            get => currentFilling;
            set => currentFilling = value;
        }

        [SerializeField]
        private List<SerializableInterface<IFadingUI>> uiToShow = new();
        public List<SerializableInterface<IFadingUI>> UiToShow => uiToShow;
    }
}
