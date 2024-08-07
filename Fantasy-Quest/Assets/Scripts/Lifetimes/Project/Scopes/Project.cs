using Lifetimes.Project.Bootstrap;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Lifetimes
{
    [AddComponentMenu("Scripts/Lifetimes/Project/Scope/Lifetimes.Project.Scopes.ProjectLifetime")]
    internal class ProjectLifetime : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            _ = builder.RegisterEntryPoint<LocalizationInitializer>();
            _ = builder.RegisterEntryPoint<ScreenRatioSetter>();
        }
    }
}
