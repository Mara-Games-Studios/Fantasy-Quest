using Bootstrap.Project;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Lifetime
{
    [AddComponentMenu("Scripts/Lifetime/Lifetime.Project")]
    internal class Project : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            _ = builder.RegisterEntryPoint<LocalizationInitializer>();
            _ = builder.RegisterEntryPoint<ScreenRatioSetter>();
        }
    }
}
