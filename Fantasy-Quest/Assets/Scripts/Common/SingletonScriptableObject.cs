using System;
using UnityEngine;

namespace Configs
{
    internal class SingletonScriptableObject<T> : ScriptableObject
        where T : SingletonScriptableObject<T>
    {
        public virtual void FirstTryLoaded() { }

        private static Lazy<T> LazyInstance { get; set; } = new(GetScriptableObject);
        public static T Instance
        {
            get
            {
                if (LazyInstance.Value == null)
                {
                    LazyInstance = new Lazy<T>(GetScriptableObject);
                }
                return LazyInstance.Value;
            }
        }

        private static T GetScriptableObject()
        {
            T[] assets = Resources.LoadAll<T>("");
            if (assets == null || assets.Length < 1)
            {
                Debug.LogError($"There is no {nameof(T)} in resource folder, please create one.");
                return null;
            }
            else if (assets.Length > 1)
            {
                Debug.LogError(
                    $"More than one {nameof(T)} in resource folder, please, remove unnecessary instances."
                );
                return null;
            }
            assets[0].FirstTryLoaded();
            return assets[0];
        }
    }
}
