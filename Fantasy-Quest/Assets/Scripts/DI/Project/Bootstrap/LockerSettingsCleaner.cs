using Configs;
using VContainer;
using VContainer.Unity;

namespace DI.Project.Bootstrap
{
    internal class LockerSettingsCleaner : IInitializable
    {
        [Inject]
        private LockerApi lockerSettings;

        [Preserve]
        public LockerSettingsCleaner() { }

        public void Initialize()
        {
            lockerSettings.Api.Initialize();
        }
    }
}
