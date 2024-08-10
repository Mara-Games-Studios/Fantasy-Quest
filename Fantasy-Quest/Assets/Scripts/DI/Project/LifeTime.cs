using Common.DI;
using DI.Project.Bootstrap;
using DI.Project.Services;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace DI.Project
{
    [AddComponentMenu("Scripts/DI/Project/DI.Project.LifeTime")]
    internal class LifeTime : TooledLifetimeScope
    {
        [Required]
        [SerializeField]
        private SoundsManager soundsManager;

        protected override void Configure(IContainerBuilder builder)
        {
            _ = builder.RegisterEntryPoint<LocalizationInitializer>();
            _ = builder.RegisterEntryPoint<ScreenRatioSetter>();

            _ = builder.Register<CursorController>(Lifetime.Singleton);

            _ = builder.RegisterComponent(soundsManager);
        }
    }
}
