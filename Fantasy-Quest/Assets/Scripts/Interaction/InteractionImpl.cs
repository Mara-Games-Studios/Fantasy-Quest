using System.Collections.Generic;
using System.Linq;
using Cat;
using Common.DI;
using Configs;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
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

        [Required]
        [SerializeField]
        private Mewing meowing;

        [Required]
        [SerializeField]
        private Rigidbody2D playerRigidBody;

        [SerializeField]
        private ContactFilter2D contactFilter;

        private GameplayInput playerInput;

        private void Awake()
        {
            playerInput = new GameplayInput();
        }

        private void OnEnable()
        {
            playerInput.Enable();
            playerInput.Player.CatInteraction.performed += CallInteraction;
            playerInput.Player.CatMeow.performed += JustMeow;

            playerInput.Player.UpJump.performed += JumpUp;
            playerInput.Player.DownJump.performed += JumpDown;
        }

        public void JustMeow(InputAction.CallbackContext context)
        {
            _ = meowing.CatMeowingTask();
        }

        public void CallInteraction(InputAction.CallbackContext context)
        {
            CastInterfaces<IInteractable>().ForEach(x => x.Interact());
        }

        public void JumpUp(InputAction.CallbackContext context)
        {
            CastInterfaces<IJumpUp>().ForEach(x => x.JumpUp());
        }

        public void JumpDown(InputAction.CallbackContext context)
        {
            CastInterfaces<IJumpDown>().ForEach(x => x.JumpDown());
        }

        private List<T> CastInterfaces<T>()
        {
            if (lockerSettings.Api.IsCatInteractionLocked)
            {
                return new List<T>();
            }

            List<RaycastHit2D> hits = new();

            Vector2 direction = (Vector2)playerRigidBody.transform.forward;
            _ = playerRigidBody.Cast(direction, contactFilter, hits, float.MaxValue);

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

            playerInput.Player.UpJump.performed -= JumpUp;
            playerInput.Player.DownJump.performed -= JumpDown;
            playerInput.Disable();
        }
    }
}
