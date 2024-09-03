using System;
using Cat;
using Configs;
using Cysharp.Threading.Tasks;
using Interaction;
using Spine.Unity;
using UnityEngine;
using UnityEngine.Events;
using VContainer;

namespace LevelSpecific.House
{
    [RequireComponent(typeof(Animator))]
    [AddComponentMenu("Scripts/LevelSpecific/House/LevelSpecific.House.Knot")]
    internal class Knot : MonoBehaviour, IInteractable
    {
        [Inject]
        private LockerApi lockerSettings;

        [SerializeField]
        private Movement catMovement;

        [SerializeField]
        private Animator animator;

        [SerializeField]
        private SkeletonAnimation skeletonAnimation;

        private int knotPosition = 1;

        public UnityEvent OnKnotHinted;

        public void Interact()
        {
            if (!lockerSettings.Api.IsCatInteractionLocked)
            {
                lockerSettings.Api.LockForCarryingItem();
                OnKnotHinted?.Invoke();
                Cat.Vector vector = catMovement.Vector;

                if (vector == Cat.Vector.Right && knotPosition < 14)
                {
                    knotPosition++;
                    skeletonAnimation.AnimationName = "MoveRight";
                    animator.SetTrigger("MoveToRight");
                }
                else if (vector == Cat.Vector.Left && knotPosition > 0)
                {
                    knotPosition--;
                    skeletonAnimation.AnimationName = "MoveLeft";
                    animator.SetTrigger("MoveToLeft");
                }
                _ = UnlockInteraction();
            }
        }

        private async UniTask UnlockInteraction()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(1));
            lockerSettings.Api.UnlockAll();
            return;
        }
    }
}
