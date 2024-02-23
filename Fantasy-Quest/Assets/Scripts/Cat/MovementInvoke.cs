using Rails;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Cat
{
    [AddComponentMenu("Scripts/Cat/Cat.MovementInvoke")]
    internal class MovementInvoke : MonoBehaviour
    {
        [SerializeField]
        private Movement movement;

        [SerializeField]
        private bool invokeSetOnRails = false;

        [ShowIf(nameof(invokeSetOnRails))]
        [SerializeField]
        private bool removeFromRailsBeforeSetOnRails = true;

        [ShowIf(nameof(invokeSetOnRails))]
        [SerializeField]
        private bool byPoint = true;

        private bool InvokeAndSetOnRails => invokeSetOnRails && byPoint;
        private bool InvokeAndNotSetOnRails => invokeSetOnRails && !byPoint;

        [ShowIf(nameof(InvokeAndSetOnRails))]
        [SerializeField]
        private Point railsPoint;

        [ShowIf(nameof(InvokeAndNotSetOnRails))]
        [SerializeField]
        private RailsImpl railsImpl;

        [ShowIf(nameof(InvokeAndNotSetOnRails))]
        [SerializeField]
        private float point;

        public void InvokeSetOnRails()
        {
            if (!invokeSetOnRails)
            {
                Debug.LogError(
                    $"Try to invoke {nameof(InvokeSetOnRails)} while {nameof(invokeSetOnRails)} is false.",
                    gameObject
                );
                return;
            }

            if (removeFromRailsBeforeSetOnRails)
            {
                movement.RemoveFromRails();
            }

            if (byPoint)
            {
                movement.SetOnRails(railsPoint);
            }
            else
            {
                movement.SetOnRails(railsImpl, point);
            }
        }

        public void InvokeRemoveFromRails()
        {
            movement.RemoveFromRails();
        }
    }
}
