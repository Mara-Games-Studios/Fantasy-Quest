using Sirenix.OdinInspector;
using UnityEngine;

namespace Configs
{
    [AddComponentMenu("Scripts/Configs/Configs.LockerInvoker")]
    internal class LockerInvoker : MonoBehaviour
    {
        [SerializeField]
        private bool invokeLockAllOnStart = false;

        [SerializeField]
        private bool invokeUnLockAllOnStart = false;

        private void Start()
        {
            if (invokeLockAllOnStart)
            {
                LockAll();
            }
            if (invokeUnLockAllOnStart)
            {
                UnLockAll();
            }
        }

        [Button]
        public void LockAll()
        {
            LockerSettings.Instance.LockAll();
        }

        [Button]
        public void UnLockAll()
        {
            LockerSettings.Instance.UnlockAll();
        }
    }
}
