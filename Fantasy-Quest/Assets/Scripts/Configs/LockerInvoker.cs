using Common.DI;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace Configs
{
    [AddComponentMenu("Scripts/Configs/Configs.LockerInvoker")]
    internal class LockerInvoker : InjectingMonoBehaviour
    {
        [Inject]
        private LockerApi lockerSettings;

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
            lockerSettings.Api.LockAll();
        }

        [Button]
        public void UnLockAll()
        {
            lockerSettings.Api.UnlockAll();
        }
    }
}
