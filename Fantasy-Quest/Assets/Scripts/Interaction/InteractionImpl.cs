using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Audio;
using Configs;
using Dialogue;
using Interaction.Item;
using Spine.Unity;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Interaction
{
    [RequireComponent(typeof(Rigidbody2D))]
    [AddComponentMenu("Scripts/Interaction/Interaction")]
    internal class InteractionImpl : MonoBehaviour
    {
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
        private bool canDoSmth = true;

        private void Awake()
        {
            playerInput = new GameplayInput();
        }

        private void OnEnable()
        {
            playerInput.Enable();
            // E
            playerInput.Player.CatInteraction.performed += InteractCat;
            playerInput.Player.CatInteraction.performed += InteractHuman;
            // M
            playerInput.Player.CatMeow.performed += JustMeow;

            // W or ArrUP
            playerInput.Player.UpJump.performed += TransitionUp;
            // S or ArrDown
            playerInput.Player.DownJump.performed += TransitionDown;
            // 2
            playerInput.Player.CallHumanMove.performed += CallHumanMove;
        }

        private void CallHumanMove(InputAction.CallbackContext context)
        {
            // 2
            CastInterfaces<ICallHumanMove>()
                .ForEach(x => x.CallHumanMove());
        }

        public void InteractHuman(InputAction.CallbackContext context)
        {
            // E
            InteractHumanRoutine();
        }

        private void InteractHumanRoutine()
        {
            List<ISpeakable> speakers = CastInterfaces<ISpeakable>();
            List<ICarryable> carriers = CastInterfaces<ICarryable>();
            List<IInteractable> interactors = CastInterfaces<IInteractable>();
            if (!LockerSettings.Instance.IsCatInteractionLocked)
            {
                if (
                    (speakers.Count > 0 || carriers.Count > 0 || interactors.Count > 0) && canDoSmth
                ) // Can Do something
                {
                    _ = StartCoroutine(CatMeowingCoroutine());
                }
                else if (canDoSmth) //Can't Do Anything
                {
                    _ = StartCoroutine(CatHissingCoroutine());
                }
            }

            speakers.ForEach(x => x.Speak());
            carriers.ForEach(x => x.CarryByHuman());
            interactors.ForEach(x => x.InteractByHuman());
        }

        private IEnumerator CatMeowingCoroutine()
        {
            if (!LockerSettings.Instance.IsCatInteractionLocked)
            {
                canDoSmth = false;
                LockerSettings.Instance.LockAllExceptBubble();
                _ = catSkeleton.AnimationState.SetAnimation(0, canDoAnim, false);
                canDoSound.PlayClip();
                yield return new WaitForSeconds(canDoAnim.Animation.Duration);
                _ = catSkeleton.AnimationState.SetAnimation(0, idleAnim, false);
                canDoSmth = true;
                LockerSettings.Instance.UnlockAll();
            }
        }

        private IEnumerator CatHissingCoroutine()
        {
            canDoSmth = false;
            LockerSettings.Instance.LockAllExceptBubble();
            _ = catSkeleton.AnimationState.SetAnimation(0, cantDoAnim, false);
            cantDoSound.PlayClip();
            yield return new WaitForSeconds(cantDoAnim.Animation.Duration);
            _ = catSkeleton.AnimationState.SetAnimation(0, idleAnim, false);
            canDoSmth = true;
            LockerSettings.Instance.UnlockAll();
        }

        public void JustMeow(InputAction.CallbackContext context)
        {
            // M
            _ = StartCoroutine(CatMeowingCoroutine());
            CastInterfaces<ISpeakable>().ForEach(x => x.Speak());
        }

        public void InteractCat(InputAction.CallbackContext context)
        {
            // E
            CastInterfaces<ISceneTransition>(true)
                .ForEach(x => x.ToNewScene());
            CastInterfaces<ICarryable>().ForEach(x => x.CarryByCat());
            CastInterfaces<IInteractable>().ForEach(x => x.InteractByCat());
        }

        public void TransitionUp(InputAction.CallbackContext context)
        {
            CastInterfaces<IJumpTransition>().ForEach(x => x.JumpUp());
        }

        public void TransitionDown(InputAction.CallbackContext context)
        {
            CastInterfaces<IJumpTransition>().ForEach(x => x.JumpDown());
        }

        private List<T> CastInterfaces<T>(bool ignore = false)
        {
            if (LockerSettings.Instance.IsCatInteractionLocked && !ignore)
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
            playerInput.Player.CatInteraction.performed -= InteractCat;
            playerInput.Player.CatInteraction.performed -= InteractHuman;
            playerInput.Player.CatMeow.performed -= JustMeow;

            playerInput.Player.UpJump.performed -= TransitionUp;
            playerInput.Player.DownJump.performed -= TransitionDown;
            playerInput.Player.CallHumanMove.performed -= CallHumanMove;
            playerInput.Disable();
        }
    }
}
