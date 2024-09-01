using UnityEngine;

namespace Common
{
    public static class CoroutineTools
    {
        public static bool KillCoroutine(this MonoBehaviour handler, Coroutine coroutine)
        {
            if (coroutine != null)
            {
                handler.StopCoroutine(coroutine);
                return true;
            }
            return false;
        }
    }
}
