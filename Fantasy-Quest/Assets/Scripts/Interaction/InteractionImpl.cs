using System.Collections.Generic;
using System.Linq;
using Configs;
using Dialogue;
using Interaction.Item;
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

        private GameplayInput playerInput;

        private void Awake()
        {
            playerInput = new GameplayInput();
        }

        private void OnEnable()
        {
            playerInput.Enable();
            playerInput.Player.CallHumanInteraction.performed += InteractHuman;
            playerInput.Player.CatInteraction.performed += InteractCat;
            playerInput.Player.UpJump.performed += TransitionUp;
            playerInput.Player.DownJump.performed += TransitionDown;
        }

        public void InteractHuman(InputAction.CallbackContext context)
        {
            CastInterfaces<ISpeakable>().ForEach(x => x.Speak());
            CastInterfaces<ICarryable>().ForEach(x => x.CarryByHuman());
            CastInterfaces<IInteractable>().ForEach(x => x.InteractByHuman());
        }

        public void InteractCat(InputAction.CallbackContext context)
        {
            CastInterfaces<ISpeakable>().ForEach(x => x.Speak());
            CastInterfaces<ICarryable>().ForEach(x => x.CarryByCat());
            CastInterfaces<IInteractable>().ForEach(x => x.InteractByCat());
        }

        public void TransitionUp(InputAction.CallbackContext context)
        {
            CastInterfaces<ISceneTransition>(true).ForEach(x => x.ToNewScene());
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
            playerInput.Player.CallHumanInteraction.performed -= InteractHuman;
            playerInput.Player.CatInteraction.performed -= InteractCat;
            playerInput.Player.UpJump.performed -= TransitionUp;
            playerInput.Player.DownJump.performed -= TransitionDown;
            playerInput.Disable();
        }
    }
}
