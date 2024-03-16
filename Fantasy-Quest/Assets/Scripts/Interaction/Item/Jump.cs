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
        [SerializeField]
        private bool upJump;

        [SerializeField]
        private TransformMover transformMove;

        [SerializeField]
        private MovementInvoke moveInvoke;

        [SerializeField]
        private ChangeWatchDir changeWatchDir;

        [SerializeField]
        private RailsImplInvoke railImpl;

        private void StartJump()
        {
            transformMove.Move();
            _ = StartCoroutine(WaitForRails());
        }

        private IEnumerator WaitForRails()
        {
            float waitTime =
                transformMove.GetDuration() + railImpl.GetDuration() + changeWatchDir.GetDuration();
            Debug.Log(waitTime);
            yield return new WaitForSeconds(waitTime);
            moveInvoke.InvokeSetOnRails();
            LockerSettings.Instance.UnlockAll();
        }

        public void JumpDown()
        {
            Debug.Log("Try to jump down");
            if (!upJump)
            {
                Debug.Log("jump down");
                StartJump();
            }
        }

        public void JumpUp()
        {
            Debug.Log("Try to jump up");
            if (upJump)
            {
                Debug.Log("jump up");
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
