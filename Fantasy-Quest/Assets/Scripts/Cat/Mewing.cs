using Audio;
using Common.DI;
using Configs;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using Spine.Unity;
using UnityEngine;
using VContainer;

namespace Cat
{
    [AddComponentMenu("Scripts/Cat/Cat.Mewing")]
    internal class Mewing : InjectingMonoBehaviour
    {
        [Inject]
        private LockerApi lockerSettings;

        [SerializeField]
        private SkeletonAnimation catSkeleton;

        [SerializeField]
        private AnimationReferenceAsset meowAnimation;

        [SerializeField]
        private SoundPlayer meowSound;

        [Button]
        public void Meow()
        {
            _ = CatMeowingTask();
        }

        public async UniTask CatMeowingTask()
        {
            if (lockerSettings.Api.IsCatInteractionLocked)
            {
                return;
            }

            lockerSettings.Api.LockAll(this);
            _ = catSkeleton.AnimationState.SetAnimation(0, meowAnimation, false);
            meowSound.PlayClip();
            await UniTask.WaitForSeconds(meowAnimation.Animation.Duration);
            lockerSettings.Api.UnlockAll(this);
        }
    }
}
