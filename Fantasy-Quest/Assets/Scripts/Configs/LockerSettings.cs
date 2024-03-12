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

        [ReadOnly]
        [SerializeField]
        private bool isCatInteractionLocked = false;
        public bool IsCatInteractionLocked => isCatInteractionLocked;

        public void LockAll()
        {
            isDialogueBubbleLocked = true;
            isCatMovementLocked = true;
            isCatInteractionLocked = true;
        }

        public void UnlockAll()
        {
            isDialogueBubbleLocked = false;
            isCatMovementLocked = false;
            isCatInteractionLocked = false;
        }
    }
}
