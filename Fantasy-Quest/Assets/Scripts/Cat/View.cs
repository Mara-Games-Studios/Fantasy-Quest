using Audio;
using Sirenix.OdinInspector;
using Spine.Unity;
using UnityEngine;

namespace Cat
{
    [AddComponentMenu("Scripts/Cat/Cat.View")]
    internal class View : MonoBehaviour
    {
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

        [Button]
        public void SetEggTaken(bool withEgg)
        {
            this.withEgg = withEgg;
        }

        [Button]
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

        private State previousState = State.Staying;

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
                        walkSound.PlayClip();
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

        [Button]
        public void SetIdleAnimation()
        {
            string animation = withAcorn
                ? idleAcornAnimation.Animation.Name
                : withEgg
                    ? idleEggAnimation.Animation.Name
                    : idleAnimation.Animation.Name;
            SetAnimation(animation);
        }

        [Button]
        public void SetWalkAnimation()
        {
            string animation = withAcorn
                ? walkAcornAnimation.Animation.Name
                : withEgg
                    ? walkEggAnimation.Animation.Name
                    : walkAnimation.Animation.Name;
            SetAnimation(animation);
        }

        [Button]
        public void SetAnimation(string animation)
        {
            skeletonAnimation.AnimationName = animation;
        }
    }
}
