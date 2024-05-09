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
        [SpineAnimation]
        private string idleAnimation;

        [SerializeField]
        [SpineAnimation]
        private string walkAnimation;

        [SerializeField]
        [SpineAnimation]
        private string idleEggAnimation;

        [SerializeField]
        [SpineAnimation]
        private string walkEggAnimation;

        [SerializeField]
        [SpineAnimation]
        private string idleAcornAnimation;

        [SerializeField]
        [SpineAnimation]
        private string walkAcornAnimation;

        [ReadOnly]
        [SerializeField]
        private bool withEgg = false;

        [ReadOnly]
        [SerializeField]
        private bool withAcorn = false;

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

        private void StateChanged(State state)
        {
            string animationName = state switch
            {
                State.Moving
                    => withAcorn
                        ? walkAcornAnimation
                        : withEgg
                            ? walkEggAnimation
                            : walkAnimation,
                State.Staying
                    => withAcorn
                        ? idleAcornAnimation
                        : withEgg
                            ? idleEggAnimation
                            : idleAnimation,
                _ => throw new System.ArgumentException()
            };
            SetAnimation(animationName);
        }

        private void SetAnimation(string animation)
        {
            if (skeletonAnimation.AnimationName != animation)
            {
                skeletonAnimation.AnimationName = animation;
            }
        }
    }
}
