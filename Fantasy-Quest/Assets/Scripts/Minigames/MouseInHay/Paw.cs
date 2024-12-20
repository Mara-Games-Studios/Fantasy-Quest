﻿using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Minigames.MouseInHay
{
    [AddComponentMenu("Scripts/Minigames/MouseInHay/Minigames.MouseInHay.Paw")]
    internal class Paw : MonoBehaviour
    {
        [Serializable]
        private struct HoleByInputAction
        {
            public Hole Hole;
            public InputAction InputAction;
        }

        [ReadOnly]
        [SerializeField]
        private bool isInAction = false;

        [SerializeField]
        private float moveToHoleTime;

        [SerializeField]
        private float moveFromHoleTime;

        [SerializeField]
        private Transform startPosition;

        [SerializeField]
        private ScoreCounter scoreCounter;

        [SerializeField]
        private List<HoleByInputAction> holesWithInput;

        public UnityEvent SuccessMouseHit;
        public UnityEvent FailMouseHit;

        public float MoveToHoleTime
        {
            get => moveToHoleTime;
            set => moveToHoleTime = value;
        }
        public float MoveFromHoleTime
        {
            get => moveFromHoleTime;
            set => moveFromHoleTime = value;
        }

        private void Awake()
        {
            foreach (HoleByInputAction hole in holesWithInput)
            {
                hole.InputAction.performed += (c) =>
                {
                    if (!isInAction)
                    {
                        _ = StartCoroutine(SlapMouse(hole.Hole));
                    }
                };
            }
        }

        private IEnumerator SlapMouse(Hole hole)
        {
            isInAction = true;
            yield return transform
                .DOMove(hole.transform.position, MoveToHoleTime)
                .WaitForCompletion();
            bool isGrabbed = hole.TryGrabMouse();
            if (isGrabbed)
            {
                SuccessMouseHit?.Invoke();
                scoreCounter.AddPoint();
            }
            else
            {
                FailMouseHit?.Invoke();
            }
            yield return transform
                .DOMove(startPosition.position, MoveFromHoleTime)
                .WaitForCompletion();
            isInAction = false;
        }

        public void EnableInput()
        {
            holesWithInput.ForEach(x => x.InputAction.Enable());
        }

        public void DisableInput()
        {
            holesWithInput.ForEach(x => x.InputAction.Disable());
        }
    }
}
