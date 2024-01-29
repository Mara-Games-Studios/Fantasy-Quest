using UnityEngine;

namespace Global
{
    internal interface ISingleton<T>
        where T : MonoBehaviour, ISingleton<T>
    {
        public static T Instance { get; private set; } = null;

        public static void InitSingleton(ISingleton<T> self)
        {
            if (ISingleton<T>.Instance == null)
            {
                ISingleton<T>.Instance = self as T;
                GameObject.DontDestroyOnLoad((self as MonoBehaviour).gameObject);
            }
            else
            {
                Debug.LogError(
                    $"Trying to initialize two singleton of {self.GetType()}",
                    self as Object
                );
            }
        }
    }
}
