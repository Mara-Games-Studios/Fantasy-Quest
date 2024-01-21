using System.Collections.Generic;
using System.Linq;
using Interaction.Item;
using Sirenix.OdinInspector;
using Speakable;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Interaction
{
    internal struct InteractableObjects
    {
        public List<ISpeakable> Speakables;
        public List<ICarryable> Carryables;
        public List<IInteractable> Interactables;

        public InteractableObjects(int a)
        {
            Speakables = new();
            Carryables = new();
            Interactables = new();
        }
    }

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
        private bool useFilterMask = true;

        [ShowIf("useFilterMask")]
        [SerializeField]
        private LayerMask filterMask;

        [SerializeField]
        private bool useTrigger = false;

        private PlayerInput playerInput;
        private ContactFilter2D contactFilter;
        private InteractableObjects interactableObjects = new(0);

        private void Awake()
        {
            playerInput = new PlayerInput();
            contactFilter = new()
            {
                layerMask = filterMask,
                useLayerMask = useFilterMask,
                useTriggers = useTrigger
            };
        }

        private void OnEnable()
        {
            playerInput.Enable();
            playerInput.Player.CallHumanInteraction.performed += InteractHuman;
            playerInput.Player.CatInteraction.performed += InteractCat;
        }

        public void InteractHuman(InputAction.CallbackContext context)
        {
            interactableObjects = FindInteractableObjects();

            interactableObjects.Speakables.ForEach(speakable => speakable.Speak());
            interactableObjects.Carryables.ForEach(carryable => carryable.CarryByHuman());
            interactableObjects.Interactables.ForEach(interactable =>
                interactable.InteractionByHuman()
            );
        }

        public void InteractCat(InputAction.CallbackContext context)
        {
            interactableObjects = FindInteractableObjects();

            interactableObjects.Speakables.ForEach(speakable => speakable.Speak());
            interactableObjects.Carryables.ForEach(carryable => carryable.CarryByCat());
            interactableObjects.Interactables.ForEach(interactable =>
                interactable.InteractionByCat()
            );
        }

        private InteractableObjects FindInteractableObjects()
        {
            List<RaycastHit2D> hits = new();

            Vector2 direction =
                new(playerRigidBody.transform.forward.x, playerRigidBody.transform.forward.y);
            _ = playerRigidBody.Cast(direction, contactFilter, hits, colDistance);

            List<ISpeakable> speakables = new();
            List<ICarryable> carryables = new();
            List<IInteractable> interactables = new();

            foreach (Transform hit in hits.Select(x => x.transform))
            {
                if (hit.TryGetComponent(out ISpeakable speakable))
                {
                    speakables.Add(speakable);
                }
                else if (hit.TryGetComponent(out ICarryable carryable))
                {
                    carryables.Add(carryable);
                }
                else if (hit.TryGetComponent(out IInteractable interactable))
                {
                    interactables.Add(interactable);
                }
            }

            return new InteractableObjects
            {
                Speakables = speakables,
                Carryables = carryables,
                Interactables = interactables
            };
        }

        private void OnDisable()
        {
            playerInput.Disable();
            playerInput.Player.CallHumanInteraction.performed -= InteractHuman;
            playerInput.Player.CatInteraction.performed -= InteractCat;
        }
    }
}
