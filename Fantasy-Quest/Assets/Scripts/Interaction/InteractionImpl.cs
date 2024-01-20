using System.Collections.Generic;
using Interaction.InteractableItem;
using Sirenix.OdinInspector;
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
        private bool useFilterMask = true;

        [ShowIf("useFilterMask")]
        [SerializeField]
        private LayerMask filterMask;

        [SerializeField]
        private bool useTrigger = false;

        private PlayerInput playerInput;
        private ContactFilter2D contactFilter;

        //Can be easily reworked to work with ActionEvents
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
            List<GameObject> GOList = GetInteractableGOList();
            foreach (GameObject go in GOList)
            {
                if (go.GetComponent<IInteractableItem>() != null)
                {
                    go.GetComponent<IInteractableItem>().InteractionByHuman();
                }
                else
                {
                    go.GetComponent<ICarryableItem>()?.CarryByHuman();
                }
            }
        }

        public void InteractCat(InputAction.CallbackContext context)
        {
            List<GameObject> GOList = GetInteractableGOList();
            foreach (GameObject go in GOList)
            {
                if (go.GetComponent<IInteractableItem>() != null)
                {
                    go.GetComponent<IInteractableItem>().InteractionByCat();
                }
                else
                {
                    go.GetComponent<ICarryableItem>()?.CarryByCat();
                }
            }
        }

        private List<GameObject> GetInteractableGOList()
        {
            List<RaycastHit2D> hits = new();

            Vector2 direction =
                new(playerRigidBody.transform.forward.x, playerRigidBody.transform.forward.y);
            _ = playerRigidBody.Cast(direction, contactFilter, hits, colDistance);

            List<GameObject> GOList = new();
            foreach (RaycastHit2D hit in hits)
            {
                GOList.Add(hit.transform.gameObject);
            }

            return GOList;
        }

        private void OnDisable()
        {
            playerInput.Disable();
            playerInput.Player.CallHumanInteraction.performed -= InteractHuman;
            playerInput.Player.CatInteraction.performed -= InteractCat;
        }
    }
}
