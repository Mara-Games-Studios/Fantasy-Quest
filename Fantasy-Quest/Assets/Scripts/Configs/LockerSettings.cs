using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Configs
{
    [Serializable]
    public class LockerApi
    {
        public LockerSettings Api;
    }

    [CreateAssetMenu(fileName = "Locker Settings", menuName = "Settings/Create Locker Settings")]
    public class LockerSettings : ScriptableObject
    {
        [Serializable]
        private class LockRequest
        {
            public UnityEngine.Object Locker;
            public bool DialogueBubble;
            public bool CatMovement;
            public bool CatInteraction;
        }

        public LockerApi Api;

        [SerializeField, ReadOnly]
        private bool isDialogueBubbleLocked = false;

        [SerializeField, ReadOnly]
        private bool isCatMovementLocked = false;

        [SerializeField, ReadOnly]
        private bool isCatInteractionLocked = false;

        [SerializeField, ReadOnly]
        private List<LockRequest> lockRequests = new();

        public bool IsDialogueBubbleLocked =>
            lockRequests.Any(x => x.DialogueBubble) || isDialogueBubbleLocked;
        public bool IsCatMovementLocked =>
            lockRequests.Any(x => x.CatMovement) || isCatMovementLocked;
        public bool IsCatInteractionLocked =>
            lockRequests.Any(x => x.CatInteraction) || isCatInteractionLocked;

        public void Initialize()
        {
            lockRequests = new();
        }

        [Button]
        public void LockAll()
        {
            isDialogueBubbleLocked = true;
            isCatMovementLocked = true;
            isCatInteractionLocked = true;
        }

        [Button]
        public void UnlockAll()
        {
            isDialogueBubbleLocked = false;
            isCatMovementLocked = false;
            isCatInteractionLocked = false;
        }

        [Button]
        public void LockAllExceptBubble()
        {
            isCatMovementLocked = true;
            isCatInteractionLocked = true;
        }

        [Button]
        public void LockForCarryingItem()
        {
            isDialogueBubbleLocked = true;
            isCatInteractionLocked = true;
        }

        // With locker
        public void LockAll(UnityEngine.Object locker)
        {
            if (lockRequests.Any(x => x.Locker == locker))
            {
                Debug.LogError($"Object {locker.name} tried double lock, unlock first.");
                return;
            }

            lockRequests.Add(
                new LockRequest()
                {
                    Locker = locker,
                    DialogueBubble = true,
                    CatInteraction = true,
                    CatMovement = true
                }
            );
        }

        public void UnlockAll(UnityEngine.Object locker)
        {
            LockRequest request = lockRequests.FirstOrDefault(x => x.Locker == locker);
            if (request == null)
            {
                Debug.LogError($"Object {locker.name} tried unlock while no his lock request.");
                return;
            }
            _ = lockRequests.Remove(request);
        }

        public bool IsLockedBy(UnityEngine.Object locker)
        {
            return lockRequests.Any(x => x.Locker == locker);
        }
    }
}
