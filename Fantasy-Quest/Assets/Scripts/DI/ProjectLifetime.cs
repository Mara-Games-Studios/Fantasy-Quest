using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace DI
{
    [AddComponentMenu("Scripts/DI/DI.ProjectLifetime")]
    internal class ProjectLifetime : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder) { }
    }
}
