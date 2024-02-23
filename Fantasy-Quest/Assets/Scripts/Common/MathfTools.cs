using UnityEngine;

namespace Common
{
    public static class MathfTools
    {
        public static float Clamp01UpperExclusive(float x)
        {
            float clamped = Mathf.Clamp01(x);
            if (clamped == 1)
            {
                clamped = 0.99999f;
            }
            return clamped;
        }
    }
}
