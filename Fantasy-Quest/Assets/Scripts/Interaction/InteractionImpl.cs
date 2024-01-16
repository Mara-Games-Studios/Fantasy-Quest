using System.Collections.Generic;
using Interaction.InteractableItem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Interaction
{
    [RequireComponent(typeof(Rigidbody2D))]
    [AddComponentMenu("Scripts/Interaction/Interaction")]
    internal class InteractionImpl : MonoBehaviour
    {
        //Can be replaced by InputEventManager, so new InputSystem wasn't used
        [Header("Input")]
        [SerializeField]
        private KeyCode catInteractionButton = KeyCode.E;

        //Can be replaced by InputEventManager, so new InputSystem wasn't used
        [SerializeField]
        private KeyCode humanInteractionButton = KeyCode.Alpha1;

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

        private Rigidbody2D playerRigidBody;

        private void Awake() { }

        //Can be easily reworked to work with ActionEvents
        private void Update()
        {
            if (Input.GetKeyDown(catInteractionButton))
            {
                List<GameObject> GOList = GetInteractableGOList();
                foreach (GameObject go in GOList)
                {
                    if (go.GetComponent<IInteractableItem>() != null)
                    {
                        go.GetComponent<IInteractableItem>().InteractionByCat();
                    }
                    else if (go.GetComponent<ICarryableItem>() != null)
                    {
                        go.GetComponent<ICarryableItem>().CarryByCat();
                    }
                }
            }

            if (Input.GetKeyDown(humanInteractionButton))
            {
                List<GameObject> GOList = GetInteractableGOList();
                foreach (GameObject go in GOList)
                {
                    if (go.GetComponent<IInteractableItem>() != null)
                    {
                        go.GetComponent<IInteractableItem>().InteractionByHuman();
                    }
                    else if (go.GetComponent<ICarryableItem>() != null)
                    {
                        go.GetComponent<ICarryableItem>().CarryByHuman();
                    }
                }
            }
        }

        private List<GameObject> GetInteractableGOList()
        {
            RaycastHit2D[] hits = new RaycastHit2D[10];
            ContactFilter2D filter = new ContactFilter2D();
            filter.layerMask = filterMask;
            filter.useLayerMask = useFilterMask;
            filter.useTriggers = useTrigger;

            Vector2 Direction = new Vector2(
                playerRigidBody.transform.forward.x,
                playerRigidBody.transform.forward.y
            );

            int num = playerRigidBody.Cast(Direction, filter, hits, colDistance);

            List<GameObject> GOList = new List<GameObject>();
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.transform != null)
                    GOList.Add(hit.transform.gameObject);
                else
                    //For future needs
                    break;
            }

            return GOList;
        }
    }
}
