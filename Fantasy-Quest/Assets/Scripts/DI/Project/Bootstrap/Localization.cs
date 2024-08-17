using VContainer;
using VContainer.Unity;

namespace DI.Project.Bootstrap
{
    internal class Localization : IInitializable
    {
        [Inject]
        private Services.Localization localizationService;

        [Preserve]
        public Localization() { }

        public void Initialize()
        {
            int localeId = localizationService.GetLocaleID();
            _ = localizationService.SetLocale(localeId);
        }
    }
}
