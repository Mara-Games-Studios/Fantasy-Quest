using Cinemachine;
using Common.DI;
using Configs;
using Cutscene;
using Sirenix.OdinInspector;
using Spine.Unity;
using UnityEngine;
using VContainer;

namespace LevelSpecific.ForestEdge
{
    [AddComponentMenu("Scripts/LevelSpecific/ForestEdge/LevelSpecific.ForestEdge.EndGoBack")]
    internal class EndGoBack : InjectingMonoBehaviour
    {
        [Inject]
        private LockerApi lockerSettings;

        [Header("Animation")]
        [SerializeField]
        private GameObject symonMovement;

        [SerializeField]
        private Transform point;

        [SerializeField]
        private SkeletonAnimation symonSkeleton;

        [SerializeField]
        private AnimationReferenceAsset symonIdleAniamtion;

        [SerializeField]
        private Cat.MovementInvoke catMovement;

        [SerializeField]
        private SkeletonAnimation catSkeleton;

        [SerializeField]
        private AnimationReferenceAsset catIdleAniamtion;

        [Header("Cams")]
        [SerializeField]
        private CinemachineVirtualCamera playerCam;

        [SerializeField]
        private CinemachineVirtualCamera nowhereCam;

        [Header("Trigger")]
        [SerializeField]
        private GameObject altarTrigger;

        [Header("FailCutscene")]
        [SerializeField]
        private Start failedCutscene;

        [Button]
        public void GoBack()
        {
            symonMovement.transform.position = point.transform.position;
            symonSkeleton.skeleton.ScaleX = -1f;
            catMovement.InvokeSetOnRails();

            _ = symonSkeleton.AnimationState.SetAnimation(0, symonIdleAniamtion, false);
            _ = catSkeleton.AnimationState.SetAnimation(0, catIdleAniamtion, false);

            altarTrigger.SetActive(true);

            playerCam.Priority = 1000;
            nowhereCam.Priority = -10;

            failedCutscene.StartCutscene();

            lockerSettings.Api.UnlockAll();
        }
    }
}
