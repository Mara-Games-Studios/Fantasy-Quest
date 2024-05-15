using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;

namespace UI
{
    [AddComponentMenu("Scripts/UI/UI.SettingsUpdater")]
    internal class SettingsUpdater : MonoBehaviour
    {
        private bool active = false;

        private void Start()
        {
            ChangeLocale(PlayerPrefs.GetInt("LocaleKey", 2));
        }

        public void ChangeLocale(int localeID)
        {
            Debug.Log("Locale changed to " + localeID);
            if (active)
            {
                return;
            }
            _ = StartCoroutine(SetLocale(localeID));
        }

        private IEnumerator SetLocale(int localeID)
        {
            active = true;
            yield return LocalizationSettings.InitializationOperation;
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[
                localeID
            ];
            PlayerPrefs.SetInt("LocaleKey", localeID);
            active = false;
        }
    }
}
