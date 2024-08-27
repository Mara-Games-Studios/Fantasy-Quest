using Audio;
using Common.DI;
using Configs;
using Sirenix.OdinInspector;
using Spine.Unity;
using UnityEngine;
using VContainer;

namespace Cat
{
    [AddComponentMenu("Scripts/Cat/Cat.View")]
    internal class View : InjectingMonoBehaviour
    {
        [Inject]
        private LockerApi lockerSettings;

        [SerializeField]
        private Movement catMovement;

        [SerializeField]
        private SkeletonAnimation skeletonAnimation;

        [SerializeField]
        [Required]
        private AnimationReferenceAsset idleAnimation;

        [SerializeField]
        [Required]
        private AnimationReferenceAsset walkAnimation;

        [SerializeField]
        [Required]
        private AnimationReferenceAsset idleEggAnimation;

        [SerializeField]
        [Required]
        private AnimationReferenceAsset walkEggAnimation;

        [SerializeField]
        [Required]
        private AnimationReferenceAsset idleAcornAnimation;

        [SerializeField]
        [Required]
        private AnimationReferenceAsset walkAcornAnimation;

        [ReadOnly]
        [SerializeField]
        private bool withEgg = false;

        [ReadOnly]
        [SerializeField]
        private bool withAcorn = false;

        [Required]
        [SerializeField]
        private SoundPlayer walkSound;
        private State previousState = State.Staying;

        public void SetEggTaken(bool withEgg)
        {
            this.withEgg = withEgg;
        }

        public void SetAcornTaken(bool withAcorn)
        {
            this.withAcorn = withAcorn;
        }

        private void OnEnable()
        {
            catMovement.OnStateChanged += StateChanged;
        }

        private void OnDisable()
        {
            catMovement.OnStateChanged -= StateChanged;
        }

        private void Update()
        {
            walkSound.AudioSource.mute = lockerSettings.Api.IsCatMovementLocked;
        }

        private void StateChanged(State state)
        {
            if (previousState != state)
            {
                switch (state)
                {
                    case State.Staying:
                        walkSound.StopClip();
                        break;
                    case State.Moving:
                        if (!walkSound.AudioSource.isPlaying)
                        {
                            walkSound.PlayClip();
                        }
                        break;
                }

                previousState = state;
            }

            switch (state)
            {
                case State.Moving:
                    SetWalkAnimation();
                    break;
                case State.Staying:
                    SetIdleAnimation();
                    break;
            }
            ;
        }

        public void SetIdleAnimation()
        {
            string animation = withAcorn
                ? idleAcornAnimation.Animation.Name
                : withEgg
                    ? idleEggAnimation.Animation.Name
                    : idleAnimation.Animation.Name;
            SetAnimation(animation);
        }

        public void SetWalkAnimation()
        {
            string animation = withAcorn
                ? walkAcornAnimation.Animation.Name
                : withEgg
                    ? walkEggAnimation.Animation.Name
                    : walkAnimation.Animation.Name;
            SetAnimation(animation);
        }

        public void SetAnimation(string animation)
        {
            if (skeletonAnimation.AnimationName != animation)
            {
                _ = skeletonAnimation.AnimationState.SetAnimation(0, animation, true);
            }
        }
    }
}
