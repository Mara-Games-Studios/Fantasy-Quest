using UnityEngine;

namespace Common
{
    internal interface ISceneSingleton<T>
        where T : MonoBehaviour, ISceneSingleton<T>
    {
        public static T Instance { get; private set; } = null;
        public void MigrateSingleton(T instance);
        public static void SetInstance(ISceneSingleton<T> instance)
        {
            Instance = instance as T;
        }
    }

    internal static class SceneSingletonTools
    {
        public static void InitSingleton<T>(this ISceneSingleton<T> self)
            where T : MonoBehaviour, ISceneSingleton<T>
        {
            if (self is not T)
            {
                Debug.LogError(
                    $"{nameof(self)} is not {nameof(ISceneSingleton<T>)},"
                        + $" {nameof(ISceneSingleton<T>)} should be implemented only by {nameof(T)}"
                );
                return;
            }

            if (self == ISceneSingleton<T>.Instance)
            {
                Debug.LogError(
                    $"{nameof(self)} is already Singletons instance, remove repeated call."
                );
                return;
            }

            if (ISceneSingleton<T>.Instance != null)
            {
                self.MigrateSingleton(ISceneSingleton<T>.Instance);
                Object.Destroy(ISceneSingleton<T>.Instance.gameObject);
            }

            ISceneSingleton<T>.SetInstance(self as T);
            Object.DontDestroyOnLoad((self as MonoBehaviour).gameObject);
        }
    }
}
