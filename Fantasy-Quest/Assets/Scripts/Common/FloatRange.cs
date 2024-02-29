using System;
using Sirenix.OdinInspector;

namespace Common
{
    [Serializable]
    public struct FloatRange
    {
        [MaxValue(nameof(Max))]
        public float Min;

        [MinValue(nameof(Min))]
        public float Max;

        public float GetRandomFloatInRange()
        {
            return UnityEngine.Random.Range(Min, Max);
        }
    }
}
