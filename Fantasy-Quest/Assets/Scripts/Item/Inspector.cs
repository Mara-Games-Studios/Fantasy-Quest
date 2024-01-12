#if UNITY_EDITOR

using System;
using Sirenix.OdinInspector;

namespace Item
{
    internal partial class ItemImpl
    {
        [Button]
        public void SetHashCode()
        {
            uid = Guid.NewGuid().ToString();
        }
    }
}

#endif
