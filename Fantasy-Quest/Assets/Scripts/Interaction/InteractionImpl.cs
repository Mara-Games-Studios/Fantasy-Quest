using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Audio;
using Cat.Jump;
using Common.DI;
using Configs;
using Cysharp.Threading.Tasks;
using Dialogue;
using Interaction.Item;
using Spine.Unity;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

namespace Interaction
{
    [RequireComponent(typeof(Rigidbody2D))]
    [AddComponentMenu("Scripts/Interaction/Interaction")]
    internal class InteractionImpl : InjectingMonoBehaviour
    {
        [Inject]
        private LockerApi lockerSettings;

        [SerializeField]
        private Rigidbody2D playerRigidBody;

        [Header("Filter Settings")]
        [SerializeField]
        private float colDistance = 0.0f;

        [SerializeField]
        private ContactFilter2D contactFilter;

        [Header("Animations")]
        [SerializeField]
        private SkeletonAnimation catSkeleton;

        [SerializeField]
        private AnimationReferenceAsset idleAnim;

        [SerializeField]
        private AnimationReferenceAsset canDoAnim;

        [SerializeField]
        private SoundPlayer canDoSound;

        [SerializeField]
        private AnimationReferenceAsset cantDoAnim;

        [SerializeField]
        private SoundPlayer cantDoSound;

        private GameplayInput playerInput;
        private bool canDoSomething = true;

        private void Awake()
        {
            playerInput = new GameplayInput();
        }

        private void OnEnable()
        {
            playerInput.Enable();
            // E
            playerInput.Player.CatInteraction.performed += CallInteraction;
            // M
            playerInput.Player.CatMeow.performed += JustMeow;

            // W or ArrUP
            playerInput.Player.UpJump.performed += TransitionUp;
            // S or ArrDown
            playerInput.Player.DownJump.performed += TransitionDown;
        }

        public void JustMeow(InputAction.CallbackContext context)
        {
            // M
            _ = CatMeowingTask();
        }

        private async UniTaskVoid CatMeowingTask()
        {
            if (!lockerSettings.Api.IsCatInteractionLocked)
            {
                canDoSomething = false;
                lockerSettings.Api.LockAllExceptBubble();
                _ = catSkeleton.AnimationState.SetAnimation(0, canDoAnim, false);
                canDoSound.PlayClip();
                await UniTask.WaitForSeconds(canDoAnim.Animation.Duration);
                canDoSomething = true;
                lockerSettings.Api.UnlockAll();
            }
        }

        private IEnumerator CatHissingRoutine()
        {
            canDoSomething = false;
            lockerSettings.Api.LockAllExceptBubble();
            _ = catSkeleton.AnimationState.SetAnimation(0, cantDoAnim, false);
            cantDoSound.PlayClip();
            yield return new WaitForSeconds(cantDoAnim.Animation.Duration);
            _ = catSkeleton.AnimationState.SetAnimation(0, idleAnim, false);
            canDoSomething = true;
            lockerSettings.Api.UnlockAll();
        }

        public void CallInteraction(InputAction.CallbackContext context)
        {
            // E
            CastInterfaces<ISceneTransition>(true)
                .ForEach(x => x.ToNewScene());
            CastInterfaces<ICarryable>().ForEach(x => x.CarryByCat());
            CastInterfaces<IInteractable>().ForEach(x => x.InteractionByCat());
            _ = StartCoroutine(InteractHumanRoutine());
        }

        private IEnumerator InteractHumanRoutine()
        {
            List<ISpeakable> speakers = CastInterfaces<ISpeakable>();
            List<ICarryable> carriers = CastInterfaces<ICarryable>();
            List<IInteractable> interactors = CastInterfaces<IInteractable>();
            if (!lockerSettings.Api.IsCatInteractionLocked)
            {
                if (
                    (speakers.Count > 0 || carriers.Count > 0 || interactors.Count > 0)
                    && canDoSomething
                ) // Can Do something
                {
                    yield return CatMeowingTask();
                }
                else if (canDoSomething) //Can't Do Anything
                {
                    yield return CatHissingRoutine();
                }
            }

            speakers.ForEach(x => x.Speak());
            carriers.ForEach(x => x.CarryByHuman());
        }

        public void TransitionUp(InputAction.CallbackContext context)
        {
            CastInterfaces<IJumpTrigger>().ForEach(x => x.JumpUp());
        }

        public void TransitionDown(InputAction.CallbackContext context)
        {
            CastInterfaces<IJumpTrigger>().ForEach(x => x.JumpDown());
        }

        private List<T> CastInterfaces<T>(bool ignore = false)
        {
            if (lockerSettings.Api.IsCatInteractionLocked && !ignore)
            {
                return new List<T>();
            }

            List<RaycastHit2D> hits = new();

            Vector2 direction = (Vector2)playerRigidBody.transform.forward;
            _ = playerRigidBody.Cast(direction, contactFilter, hits, colDistance);

            List<T> founded = new();
            foreach (Transform hit in hits.Select(x => x.transform))
            {
                if (hit.TryGetComponent(out T reference))
                {
                    founded.Add(reference);
                }
            }
            return founded;
        }

        private void OnDisable()
        {
            playerInput.Player.CatInteraction.performed -= CallInteraction;
            playerInput.Player.CatMeow.performed -= JustMeow;

            playerInput.Player.UpJump.performed -= TransitionUp;
            playerInput.Player.DownJump.performed -= TransitionDown;
            playerInput.Disable();
        }
    }
}
