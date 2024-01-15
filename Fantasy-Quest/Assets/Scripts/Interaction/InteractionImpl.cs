using System.Collections.Generic;
using Interaction.InteractableItem;
using UnityEngine;

namespace Interaction
{
    [AddComponentMenu("Scripts/Interaction/Interaction")]
    internal class InteractionImpl : MonoBehaviour
    {
        [Header("Input")]
        [SerializeField]
        private KeyCode catInteractionButton = KeyCode.E; //Can be replaced by InputEventManager, so new InputSystem wasn't used

        [SerializeField]
        private KeyCode humanInteractionButton = KeyCode.Alpha1; //Can be replaced by InputEventManager, so new InputSystem wasn't used

        [Header("Filter Settings")]
        [SerializeField]
        private float colDistance = 0.0f;

        [SerializeField]
        private bool useFilterMask = true;

        [SerializeField]
        private LayerMask filterMask;

        [SerializeField]
        private bool useTrigger = false;

        private Rigidbody2D rbPlayer;

        private void Awake()
        {
            rbPlayer = GetComponent<Rigidbody2D>();
        }

        private void Update() //Can be easily reworked to work with ActionEvents
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
            RaycastHit2D[] Hits = new RaycastHit2D[10];
            ContactFilter2D Filter = new ContactFilter2D();
            Filter.layerMask = filterMask;
            Filter.useLayerMask = useFilterMask;
            Filter.useTriggers = useTrigger;

            Vector2 Direction = new Vector2(
                rbPlayer.transform.forward.x,
                rbPlayer.transform.forward.y
            );

            int num = rbPlayer.Cast(Direction, Filter, Hits, colDistance);

            List<GameObject> GOList = new List<GameObject>();
            foreach (RaycastHit2D hit in Hits)
            {
                if (hit.transform != null)
                    GOList.Add(hit.transform.gameObject);
                else
                    break;
            }

            return GOList;
        }
    }
}
