using Sirenix.OdinInspector;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "Locker Settings", menuName = "Settings/Create Locker Settings")]
    internal class LockerSettings : SingletonScriptableObject<LockerSettings>
    {
        [ReadOnly]
        [SerializeField]
        private bool isDialogueBubbleLocked = false;
        public bool IsDialogueBubbleLocked => isDialogueBubbleLocked;

        [ReadOnly]
        [SerializeField]
        private bool isCatMovementLocked = false;
        public bool IsCatMovementLocked => isCatMovementLocked;

        public void LockAll()
        {
            isDialogueBubbleLocked = true;
            isCatMovementLocked = true;
        }

        public void UnlockAll()
        {
            isDialogueBubbleLocked = false;
            isCatMovementLocked = false;
        }
    }
}
