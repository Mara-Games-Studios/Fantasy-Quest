﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Common;
using DG.Tweening;
using Dialogue;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Minigames.AltarGame.Hand
{
    [AddComponentMenu("Scripts/Minigames/AltarGame/Minigames.AltarGame.Hand")]
    internal class HandImpl : MonoBehaviour
    {
        [Serializable]
        public class PointToMove
        {
            public Transform Point;
            public float Duration;

            public Vector3 Position => Point.position;
        }

        [AssetsOnly]
        [SerializeField]
        private List<Item> itemsToCreate = new();

        [ReadOnly]
        [SerializeField]
        private List<Item> temporaryItemsToCreate = new();

        [Required]
        [SerializeField]
        private Altar altar;

        [Required]
        [SerializeField]
        private Manager manager;

        [Required]
        [SerializeField]
        private Animator animator;

        [Required]
        [SerializeField]
        private PointToMove takeItemPoint;

        [SerializeField]
        private float moveToSlotDuration = 2.0f;

        [SerializeField]
        private float decideWaitingTime = 3.0f;

        [Required]
        [SerializeField]
        private PointToMove endGamePoint;

        [Required]
        [SerializeField]
        private ChainSpeaker firstMoveSpeech;

        [Required]
        [SerializeField]
        private ChainSpeaker wrongPlacingSpeech;

        [Required]
        [SerializeField]
        private Transform startPosition;

        [Required]
        [SerializeField]
        private Transform createItemPosition;

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
        private Coroutine tellCoroutine;
        private Tween moveTween;

        public void ResetHand()
        {
            isFirstMove = true;
            temporaryItemsToCreate.Clear();
            temporaryItemsToCreate.AddRange(itemsToCreate);
            _ = this.KillCoroutine(waitingForDecide);
            _ = this.KillCoroutine(tellCoroutine);
            moveTween?.Kill();
            isChoosing = false;
            if (holdingItem != null)
            {
                Destroy(holdingItem.gameObject);
                holdingItem = null;
            }
            transform.position = startPosition.position;
        }

        public void TakeItem()
        {
            moveTween = transform.DOMove(takeItemPoint.Position, takeItemPoint.Duration);
            moveTween.onComplete += CreateItem;
        }

        private void CreateItem()
        {
            Item item = temporaryItemsToCreate[
                UnityEngine.Random.Range(0, temporaryItemsToCreate.Count)
            ];
            _ = temporaryItemsToCreate.Remove(item);
            Item created = Instantiate(item, transform);
            created.transform.position = createItemPosition.position;
            holdingItem = created;
            StartChoosePlaceItems();
        }

        private void StartChoosePlaceItems()
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
                moveTween = transform.DOMove(chosenSlot.transform.position, moveToSlotDuration);
                if (isFirstMove)
                {
                    moveTween.onComplete += () => firstMoveSpeech.Tell(() => { });
                    isFirstMove = false;
                }
                moveTween.onComplete += StartWaitForDecide;
            }
            else if (altar.GetFreeSlots().Count() != 1)
            {
                StartChoosePlaceItems();
            }
            else
            {
                StartWaitForDecide();
            }
        }

        private void StartWaitForDecide()
        {
            isChoosing = true;
            _ = this.KillCoroutine(waitingForDecide);
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
            if (temporaryItemsToCreate.Any())
            {
                TakeItem();
            }
            else
            {
                moveTween = transform.DOMove(endGamePoint.Position, endGamePoint.Duration);
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
                tellCoroutine = StartCoroutine(
                    wrongPlacingSpeech.TellRoutine(() => manager.QuitMiniGame())
                );
            }
        }

        public UnityEvent OnChooseDisagreeHappens;

        public void ChooseDisagree()
        {
            if (!isChoosing)
            {
                return;
            }
            OnChooseDisagreeHappens?.Invoke();
            _ = this.KillCoroutine(waitingForDecide);
            isChoosing = false;
            MoveToNextSlot();
        }

        public UnityEvent OnChooseAgreeHappens;

        public void ChooseAgree()
        {
            if (!isChoosing)
            {
                return;
            }
            OnChooseAgreeHappens?.Invoke();
            _ = this.KillCoroutine(waitingForDecide);
            PlaceItemInSlot();
        }
    }
}
