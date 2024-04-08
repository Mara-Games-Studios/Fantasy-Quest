using System.Collections;
using Cat;
using Configs;
using Rails;
using Sirenix.OdinInspector;
using UnityEngine;
using Utils;

namespace Interaction.Item
{
    [AddComponentMenu("Scripts/Interaction/Item/Interaction.Item.Jump")]
    internal class Jump : MonoBehaviour, IJumpTransition
    {
        [Header("Jump direction")]
        [SerializeField]
        private bool upJump;

        [Header("Controllers")]
        [SerializeField]
        [RequiredIn(PrefabKind.InstanceInPrefab)]
        private TransformMover transformMove;

        [SerializeField]
        [RequiredIn(PrefabKind.InstanceInPrefab)]
        private MovementInvoke moveInvoke;

        [SerializeField]
        [RequiredIn(PrefabKind.InstanceInPrefab)]
        private ChangeWatchDir changeWatchDir;

        [SerializeField]
        [RequiredIn(PrefabKind.InstanceInPrefab)]
        private RailsImplInvoke railImpl;

        [Header("Movement return correction")]
        [SerializeField]
        private float waitOffset = 0.3f;

        private void StartJump()
        {
            transformMove.Move();
            _ = StartCoroutine(WaitForRails());
        }

        private IEnumerator WaitForRails()
        {
            float waitTime =
                transformMove.GetDuration() + railImpl.GetDuration() + changeWatchDir.GetDuration();
            yield return new WaitForSeconds(waitTime - waitOffset);
            LockerSettings.Instance.UnlockAll();
            moveInvoke.InvokeSetOnRails();
        }

        public void JumpDown()
        {
            if (!upJump)
            {
                StartJump();
            }
        }

        public void JumpUp()
        {
            if (upJump)
            {
                StartJump();
            }
        }

        [Button]
        private void FindDependencies()
        {
            moveInvoke = GetComponentInChildren<MovementInvoke>();
            railImpl = GetComponentInChildren<RailsImplInvoke>();
            transformMove = GetComponentInChildren<TransformMover>();
            changeWatchDir = GetComponentInChildren<ChangeWatchDir>();
        }
    }
}
