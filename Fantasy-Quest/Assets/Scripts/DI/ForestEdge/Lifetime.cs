using Common.DI;
using Subtitles;
using TNRD;
using UnityEngine;
using VContainer;

namespace DI.ForestEdge
{
    [AddComponentMenu("Scripts/DI/ForestEdge/DI.ForestEdge.Lifetime")]
    internal class Lifetime : TooledLifetimeScope
    {
        [SerializeField]
        private SerializableInterface<ISubtitlesView> subtitlesView;

        protected override void Configure(IContainerBuilder builder)
        {
            _ = builder.RegisterInstance(subtitlesView.Value);
        }
    }
}
