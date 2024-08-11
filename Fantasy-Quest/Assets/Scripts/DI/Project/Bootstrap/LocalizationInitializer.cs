﻿using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Localization.Settings;
using VContainer;
using VContainer.Unity;

namespace DI.Project.Bootstrap
{
    public class LocalizationInitializer : IInitializable
    {
        [Preserve]
        public LocalizationInitializer() { }

        public void Initialize()
        {
            int locale = PlayerPrefs.GetInt("LocaleKey", 0);
            _ = SetLocale(locale);
        }

        private async UniTaskVoid SetLocale(int localeID)
        {
            _ = await LocalizationSettings.InitializationOperation;
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[
                localeID
            ];
            PlayerPrefs.SetInt("LocaleKey", localeID);
        }
    }
}
