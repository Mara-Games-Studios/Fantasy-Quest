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
        private bool byPoint = true;

        [ShowIf(nameof(invokeSetOnRails))]
        [ShowIf(nameof(byPoint))]
        [SerializeField]
        private Point railsPoint;

        [ShowIf(nameof(invokeSetOnRails))]
        [HideIf(nameof(byPoint))]
        [SerializeField]
        private RailsImpl railsImpl;

        [ShowIf(nameof(invokeSetOnRails))]
        [HideIf(nameof(byPoint))]
        [SerializeField]
        private float point;

        public void InvokeSetOnRails()
        {
            if (!invokeSetOnRails)
            {
                Debug.LogError("Try to invoke InvokeSetOnRails while invokeSetOnRails if false");
                return;
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
