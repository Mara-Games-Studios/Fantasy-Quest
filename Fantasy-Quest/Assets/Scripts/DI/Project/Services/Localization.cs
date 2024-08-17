using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Localization.Settings;
using VContainer;

namespace DI.Project.Services
{
    public class Localization
    {
        public static string LOCALE_ID_LABEL = "LocaleKey";

        [Preserve]
        public Localization() { }

        public int GetLocaleID()
        {
            return PlayerPrefs.GetInt(LOCALE_ID_LABEL, 0);
        }

        public async UniTaskVoid SetLocale(int localeID)
        {
            _ = await LocalizationSettings.InitializationOperation;
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[
                localeID
            ];
            PlayerPrefs.SetInt(LOCALE_ID_LABEL, localeID);
        }
    }
}
