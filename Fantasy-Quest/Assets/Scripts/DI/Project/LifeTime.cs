﻿using Common.DI;
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
            builder.UseEntryPoints(
                (entryPoints) =>
                {
                    _ = entryPoints.Add<Bootstrap.Localization>();
                    _ = entryPoints.Add<ScreenRatio>();
                }
            );

            _ = builder.Register<Services.Cursor>(Lifetime.Singleton);
            _ = builder.Register<Services.Localization>(Lifetime.Singleton);

            _ = builder.RegisterComponent(soundsManager);
        }
    }
}
