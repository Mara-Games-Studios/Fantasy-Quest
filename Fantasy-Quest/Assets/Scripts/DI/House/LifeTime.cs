using Common.DI;
using Subtitles;
using TNRD;
using UnityEngine;
using VContainer;

namespace DI.House
{
    [AddComponentMenu("Scripts/DI/House/DI.House.LifeTime")]
    internal class LifeTime : TooledLifetimeScope
    {
        [SerializeField]
        private SerializableInterface<ISubtitlesView> subtitlesView;

        protected override void Configure(IContainerBuilder builder)
        {
            _ = builder.RegisterInstance(subtitlesView.Value);
        }
    }
}
