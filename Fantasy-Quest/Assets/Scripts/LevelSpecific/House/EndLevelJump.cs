using Cat.Jump;
using Common;
using Common.DI;
using Configs;
using Configs.Progression;
using DG.Tweening;
using Effects;
using Interaction;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace LevelSpecific.House
{
    [RequireComponent(typeof(Collider2D))]
    [AddComponentMenu("Scripts/LevelSpecific/House/LevelSpecific.House.EndLevelJump")]
    internal class EndLevelJump : InjectingMonoBehaviour, IJumpUp
    {
        [Inject]
        private LockerApi lockerSettings;

        [Required]
        [SerializeField]
        private Fade catFade;

        [Required]
        [SerializeField]
        private Cat.Movement catMovement;

        [Required]
        [SerializeField]
        private Cat.Jump.Trigger jumpTrigger;

        [Required]
        [SerializeField]
        private Transition.End.Invoker endInvoker;

        [Required]
        [SerializeField]
        private GroundMask groundMask;

        [Required]
        [SerializeField]
        private ProgressionConfig progressionConfig;

        [Scene]
        [SerializeField]
        private string sceneToLoadInProgression;

        public void JumpUp()
        {
            if (catMovement.Vector == Cat.Vector.Left)
            {
                jumpTrigger.JumpUp();
                return;
            }

            lockerSettings.Api.LockAll();
            groundMask.SetAvailable(true);
            float fadeDuration = jumpTrigger.JumpDuration * 0.8f;
            jumpTrigger.JumpUp();
            catFade.SetFadeDuration(fadeDuration);
            catFade.Disappear();
            _ = DOVirtual.DelayedCall(
                fadeDuration,
                () =>
                {
                    endInvoker.Invoke();
                    progressionConfig.SetSceneToLoad(sceneToLoadInProgression);
                },
                false
            );
        }
    }
}
