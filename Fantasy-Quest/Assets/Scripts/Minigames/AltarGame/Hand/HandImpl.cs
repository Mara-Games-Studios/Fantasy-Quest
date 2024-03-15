using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Dialogue;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Minigames.AltarGame.Hand
{
    [AddComponentMenu("Scripts/Minigames/AltarGame/Minigames.AltarGame.Hand")]
    internal class HandImpl : MonoBehaviour
    {
        [Serializable]
        private struct PointToMove
        {
            public Transform Point;
            public float Duration;

            public Vector3 Position => Point.position;
        }

        [AssetsOnly]
        [SerializeField]
        private List<Item> itemsToCreate;

        [SerializeField]
        private Altar altar;

        [SerializeField]
        private Manager manager;

        [SerializeField]
        private Animator animator;

        [SerializeField]
        private PointToMove takeItemPoint;

        [SerializeField]
        private float moveToSlotDuration = 2.0f;

        [SerializeField]
        private float decideWaitingTime = 3.0f;

        [SerializeField]
        private PointToMove endGamePoint;

        [SerializeField]
        private ChainSpeaker firstMoveSpeech;

        [SerializeField]
        private ChainSpeaker wrongPlacingSpeech;

        [ReadOnly]
        [SerializeField]
        private bool isChoosing = false;

        [ReadOnly]
        [SerializeField]
        private Item holdingItem = null;

        [ReadOnly]
        [SerializeField]
        private Queue<Slot> query;

        [ReadOnly]
        [SerializeField]
        private Slot chosenSlot;

        [ReadOnly]
        [SerializeField]
        private bool isFirstMove = true;
        private Coroutine waitingForDecide;

        public void TakeItem()
        {
            Tween moveTween = transform.DOMove(takeItemPoint.Position, takeItemPoint.Duration);
            moveTween.onComplete += CreateItem;
        }

        private void CreateItem()
        {
            Item item = itemsToCreate[UnityEngine.Random.Range(0, itemsToCreate.Count)];
            _ = itemsToCreate.Remove(item);
            Item created = Instantiate(item, transform);
            created.transform.position += Vector3.back;
            holdingItem = created;
            StartPlaceItem();
        }

        private void StartPlaceItem()
        {
            query = new Queue<Slot>(altar.GetFreeSlots());
            if (query.Count == 0)
            {
                Debug.LogError("No slots in created query");
                return;
            }
            MoveToNextSlot();
        }

        private void MoveToNextSlot()
        {
            if (query.Any())
            {
                chosenSlot = query.Dequeue();
                Tween moveTween = transform.DOMove(
                    chosenSlot.transform.position,
                    moveToSlotDuration
                );
                if (isFirstMove)
                {
                    moveTween.onComplete += () => firstMoveSpeech.Tell(() => StartWaitForDecide());
                    isFirstMove = false;
                }
                else
                {
                    moveTween.onComplete += StartWaitForDecide;
                }
            }
            else
            {
                StartPlaceItem();
            }
        }

        private void StartWaitForDecide()
        {
            isChoosing = true;
            waitingForDecide = StartCoroutine(WaitForDecide(decideWaitingTime));
        }

        private IEnumerator WaitForDecide(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            PlaceItemInSlot();
        }

        private void PlaceItemInSlot()
        {
            isChoosing = false;
            chosenSlot.PlaceItem(holdingItem);
            animator.SetTrigger("PlaceItem");
        }

        // Called by animation callback
        private void CompletePlacingItemInSlot()
        {
            if (itemsToCreate.Any())
            {
                TakeItem();
            }
            else
            {
                Tween moveTween = transform.DOMove(endGamePoint.Position, endGamePoint.Duration);
                moveTween.onComplete += EndPointReached;
            }
        }

        private void EndPointReached()
        {
            if (altar.IsAllRightPlaced())
            {
                altar.TurnOnAltar();
            }
            else
            {
                wrongPlacingSpeech.Tell(() => manager.QuitMiniGame());
            }
        }

        public void ChooseDisagree()
        {
            if (!isChoosing)
            {
                return;
            }

            StopCoroutine(waitingForDecide);
            isChoosing = false;
            MoveToNextSlot();
        }

        public void ChooseAgree()
        {
            if (!isChoosing)
            {
                return;
            }

            StopCoroutine(waitingForDecide);
            PlaceItemInSlot();
        }
    }
}
