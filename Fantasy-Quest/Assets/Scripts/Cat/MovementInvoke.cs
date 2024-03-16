using Rails;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Cat
{
    [AddComponentMenu("Scripts/Cat/Cat.MovementInvoke")]
    internal class MovementInvoke : MonoBehaviour
    {
        [RequiredIn(PrefabKind.PrefabInstanceAndNonPrefabInstance)]
        [SerializeField]
        private Movement movement;

        [SerializeField]
        private bool removeFromRailsBeforeSetOnRails = true;

        [SerializeField]
        private bool byPoint = true;

        [ShowIf(nameof(byPoint))]
        [SerializeField]
        private Point railsPoint;

        [HideIf(nameof(byPoint))]
        [SerializeField]
        private RailsImpl railsImpl;

        [HideIf(nameof(byPoint))]
        [SerializeField]
        private float point;

        public void InvokeSetOnRails()
        {
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
