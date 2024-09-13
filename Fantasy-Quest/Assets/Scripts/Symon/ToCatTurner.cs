using Spine.Unity;
using UnityEngine;

namespace Symon
{
    [AddComponentMenu("Scripts/Symon/Symon.ToCatTurner")]
    internal class ToCatTurner : MonoBehaviour
    {
        [SerializeField]
        private Cat.Movement catMovement;

        [SerializeField]
        private SkeletonAnimation symonSkeleton;

        public void TurnToCat()
        {
            if (transform.position.x > catMovement.transform.position.x)
            {
                symonSkeleton.skeleton.ScaleX = 1;
            }
            else
            {
                symonSkeleton.skeleton.ScaleX = -1;
            }
        }
    }
}
