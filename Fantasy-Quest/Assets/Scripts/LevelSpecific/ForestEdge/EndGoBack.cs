using Cinemachine;
using Configs;
using Sirenix.OdinInspector;
using Spine.Unity;
using UnityEngine;

namespace LevelSpecific.ForestEdge
{
    [AddComponentMenu("Scripts/LevelSpecific/ForestEdge/LevelSpecific.ForestEdge.EndGoBack")]
    internal class EndGoBack : MonoBehaviour
    {
        [Header("Animation")]
        [SerializeField]
        private GameObject symonMovement;

        [SerializeField]
        private Transform point;

        [SerializeField]
        private SkeletonAnimation symonSkeleton;

        [SerializeField]
        private AnimationReferenceAsset symonSeatAniamtion;

        [SerializeField]
        private Cat.MovementInvoke catMovement;

        [SerializeField]
        private SkeletonAnimation catSkeleton;

        [SerializeField]
        private AnimationReferenceAsset catIdleAniamtion;

        [SerializeField]
        private CinemachineVirtualCamera playerCam;

        [SerializeField]
        private CinemachineVirtualCamera nowhereCam;

        [Header("Trigger")]
        [SerializeField]
        private GameObject altarTrigger;

        [Button]
        public void GoBack()
        {
            symonMovement.transform.position = point.transform.position;
            symonSkeleton.skeleton.ScaleX = -1f;
            catMovement.InvokeSetOnRails();

            _ = symonSkeleton.AnimationState.SetAnimation(0, symonSeatAniamtion, false);
            _ = catSkeleton.AnimationState.SetAnimation(0, catIdleAniamtion, false);

            altarTrigger.SetActive(true);

            playerCam.Priority = 1000;
            nowhereCam.Priority = -10;

            LockerSettings.Instance.UnlockAll();
        }
    }
}
