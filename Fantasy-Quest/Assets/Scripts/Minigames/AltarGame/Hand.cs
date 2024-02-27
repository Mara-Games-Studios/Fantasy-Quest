using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Common;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Minigames.AltarGame
{
    [AddComponentMenu("Scripts/Minigames/AltarGame/Minigames.AltarGame.Hand")]
    internal class Hand : MonoBehaviour
    {
        [AssetsOnly]
        [SerializeField]
        private List<Item> itemsToCreate;

        [SerializeField]
        private Altar altar;

        [SerializeField]
        private Transition.End.Controller endController;

        [Scene]
        [SerializeField]
        private string nextScene;

        [SerializeField]
        private Transform takeItemPoint;

        [SerializeField]
        private float takeItemDuration = 2.0f;

        [SerializeField]
        private float moveToSlotDuration = 2.0f;

        [SerializeField]
        private float decideWaitingTime = 3.0f;

        [ReadOnly]
        [SerializeField]
        private bool isChoosing;

        [ReadOnly]
        [SerializeField]
        private Item holdingItem = null;

        [ReadOnly]
        [SerializeField]
        private Queue<Slot> query;

        [ReadOnly]
        [SerializeField]
        private Slot chosenSlot;

        private Coroutine waitingForDecide;
        private AltarGameInput input;

        private void Awake()
        {
            input = new();
            input.Player.Agree.performed += AgreePerformed;
            input.Player.Disagree.performed += DisagreePerformed;
        }

        private void Start()
        {
            TakeItem();
        }

        private void TakeItem()
        {
            TweenerCore<Vector3, Vector3, VectorOptions> moveTween = transform.DOMove(
                takeItemPoint.position,
                takeItemDuration
            );
            moveTween.onComplete += () =>
            {
                Item item = itemsToCreate[Random.Range(0, itemsToCreate.Count)];
                _ = itemsToCreate.Remove(item);
                Item created = Instantiate(item, transform);
                created.transform.position += Vector3.back;
                holdingItem = created;
                StartPlaceItem();
            };
        }

        private void StartPlaceItem()
        {
            query = new Queue<Slot>(altar.GetFreeSlots());
            if (query.Count == 0)
            {
                Debug.LogError("No slots in created query");
                return;
            }
            MoveToNextPlace();
        }

        private void MoveToNextPlace()
        {
            if (query.Any())
            {
                chosenSlot = query.Dequeue();
                TweenerCore<Vector3, Vector3, VectorOptions> moveTween = transform.DOMove(
                    chosenSlot.transform.position,
                    moveToSlotDuration
                );
                moveTween.onComplete += () =>
                {
                    isChoosing = true;
                    waitingForDecide = StartCoroutine(WaitForDecide(decideWaitingTime));
                };
            }
            else
            {
                StartPlaceItem();
            }
        }

        private IEnumerator WaitForDecide(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            PlaceItemInSlot();
        }

        private void MoveToNextSlot()
        {
            isChoosing = false;
            MoveToNextPlace();
        }

        private void PlaceItemInSlot()
        {
            isChoosing = false;
            chosenSlot.PlaceItem(holdingItem);
            if (itemsToCreate.Any())
            {
                TakeItem();
            }
            else if (altar.IsAllRightPlaced())
            {
                Debug.Log("You win");
                endController.LoadScene(
                    nextScene,
                    Configs.TransitionSettings.Instance.MinLoadingDuration
                );
            }
            else
            {
                Debug.Log("You lose");
                endController.LoadScene(
                    nextScene,
                    Configs.TransitionSettings.Instance.MinLoadingDuration
                );
            }
        }

        private void DisagreePerformed(InputAction.CallbackContext context)
        {
            if (!isChoosing)
            {
                return;
            }

            StopCoroutine(waitingForDecide);
            MoveToNextSlot();
        }

        private void AgreePerformed(InputAction.CallbackContext context)
        {
            if (!isChoosing)
            {
                return;
            }

            StopCoroutine(waitingForDecide);
            PlaceItemInSlot();
        }

        private void OnEnable()
        {
            input.Enable();
        }

        private void OnDisable()
        {
            input.Disable();
        }
    }
}
