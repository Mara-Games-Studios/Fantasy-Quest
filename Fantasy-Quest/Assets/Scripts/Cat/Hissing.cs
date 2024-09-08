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
    [AddComponentMenu("Scripts/Cat/Cat.Hissing")]
    internal class Hissing : InjectingMonoBehaviour
    {
        [Inject]
        private LockerApi lockerSettings;

        [SerializeField]
        private SkeletonAnimation catSkeleton;

        [SerializeField]
        private AnimationReferenceAsset hissAnimation;

        [SerializeField]
        private SoundPlayer hissSound;
        public bool ShouldPlay = false;

        [Button]
        public void Hiss()
        {
            _ = CatHissingTask();
        }

        public void PlayAfterTransition()
        {
            ShouldPlay = true;
        }

        public async UniTask CatHissingTask()
        {
            if (ShouldPlay)
            {
                lockerSettings.Api.LockAll(this);
                _ = catSkeleton.AnimationState.SetAnimation(0, hissAnimation, false);
                hissSound.PlayClip();
                await UniTask.WaitForSeconds(hissAnimation.Animation.Duration);
                lockerSettings.Api.UnlockAll(this);
                ShouldPlay = false;
            }
        }
    }
}
