using System;
using Cat;
using Cysharp.Threading.Tasks;
using Interaction;
using Spine.Unity;
using UnityEngine;
using UnityEngine.Events;

namespace LevelSpecific.House
{
    [RequireComponent(typeof(Animator))]
    [AddComponentMenu("Scripts/LevelSpecific/House/LevelSpecific.House.Knot")]
    internal class Knot : MonoBehaviour, IInteractable
    {
        [SerializeField]
        private Movement catMovement;

        [SerializeField]
        private Animator animator;

        [SerializeField]
        private int animationLayer;

        [SerializeField]
        private SkeletonAnimation skeletonAnimation;

        [SerializeField]
        private float knotStartPosition;

        [SerializeField]
        private float knotEndPosition;

        private float knotPosition;

        [SerializeField]
        private bool isKnotMoving = false;

        public UnityEvent OnKnotHinted;

        public void Interact()
        {
            if (!isKnotMoving)
            {
                knotPosition = gameObject.transform.position.x;
                isKnotMoving = true;
                OnKnotHinted?.Invoke();
                Cat.Vector vector = catMovement.Vector;

                if (vector == Cat.Vector.Right && knotPosition < knotEndPosition)
                {
                    knotPosition++;
                    skeletonAnimation.AnimationName = "MoveRight";
                    animator.Play("MoveToRight");
                    _ = UnlockInteraction();
                }
                else if (vector == Cat.Vector.Left && knotPosition > knotStartPosition)
                {
                    knotPosition--;
                    skeletonAnimation.AnimationName = "MoveLeft";
                    animator.Play("MoveToLeft");
                    _ = UnlockInteraction();
                }
            }
        }

        private async UniTask UnlockInteraction()
        {
            await UniTask.Delay(
                TimeSpan.FromSeconds(animator.GetCurrentAnimatorStateInfo(animationLayer).length)
            );
            isKnotMoving = false;
            return;
        }
    }
}
