using System.Collections.Generic;
using System.Linq;
using Dialogue;
using Interaction.Item;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Interaction
{
    internal struct InteractableObjects
    {
        public List<ISpeakable> Speakables;
        public List<ICarryable> Carryables;
        public List<IInteractable> Interactables;
    }

    internal struct TransitionObjects
    {
        public List<ISceneTransition> SceneTransitors;
        public List<IJumpTranstition> JumpTranstitors;
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

        private GameplayInput playerInput;
        private ContactFilter2D contactFilter;

        private void Awake()
        {
            playerInput = new GameplayInput();
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
            playerInput.Player.UpJump.performed += TransitionUp;
            playerInput.Player.DownJump.performed += TransitionDown;
        }

        public void InteractHuman(InputAction.CallbackContext context)
        {
            InteractableObjects interactableObjects = FindInteractableObjects();

            interactableObjects.Speakables.ForEach(speakable => speakable.Speak());
            interactableObjects.Carryables.ForEach(carryable => carryable.CarryByHuman());
            interactableObjects.Interactables.ForEach(interactable =>
                interactable.InteractByHuman()
            );
        }

        public void InteractCat(InputAction.CallbackContext context)
        {
            InteractableObjects interactableObjects = FindInteractableObjects();

            interactableObjects.Speakables.ForEach(speakable => speakable.Speak());
            interactableObjects.Carryables.ForEach(carryable => carryable.CarryByCat());
            interactableObjects.Interactables.ForEach(interactable => interactable.InteractByCat());
        }

        public void TransitionUp(InputAction.CallbackContext context)
        {
            TransitionObjects transitionObjects = FindTransitionObjects();

            transitionObjects.SceneTransitors.ForEach(sceneTrans => sceneTrans.ToNewScene());
            transitionObjects.JumpTranstitors.ForEach(jumpTrans => jumpTrans.JumpUp());
        }

        public void TransitionDown(InputAction.CallbackContext context)
        {
            TransitionObjects transitionObjects = FindTransitionObjects();

            transitionObjects.JumpTranstitors.ForEach(jumpTrans => jumpTrans.JumpDown());
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
                if (hit.TryGetComponent(out ICarryable carryable))
                {
                    carryables.Add(carryable);
                }
                if (hit.TryGetComponent(out IInteractable interactable))
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

        private TransitionObjects FindTransitionObjects()
        {
            List<RaycastHit2D> hits = new();

            Vector2 direction =
                new(playerRigidBody.transform.forward.x, playerRigidBody.transform.forward.y);

            _ = playerRigidBody.Cast(direction, contactFilter, hits, colDistance);

            List<IJumpTranstition> jumpTransitors = new();
            List<ISceneTransition> sceneTransitors = new();

            foreach (Transform hit in hits.Select(x => x.transform))
            {
                if (hit.TryGetComponent(out IJumpTranstition jumpTrans))
                {
                    jumpTransitors.Add(jumpTrans);
                }
                if (hit.TryGetComponent(out ISceneTransition sceneTrans))
                {
                    sceneTransitors.Add(sceneTrans);
                }
            }

            return new TransitionObjects
            {
                JumpTranstitors = jumpTransitors,
                SceneTransitors = sceneTransitors,
            };
        }

        private void OnDisable()
        {
            playerInput.Disable();
            playerInput.Player.CallHumanInteraction.performed -= InteractHuman;
            playerInput.Player.CatInteraction.performed -= InteractCat;
            playerInput.Player.UpJump.performed -= TransitionUp;
            playerInput.Player.DownJump.performed -= TransitionDown;
        }
    }
}
