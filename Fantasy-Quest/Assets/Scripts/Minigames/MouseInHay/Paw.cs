using System;
using System.Collections;
using System.Collections.Generic;
using Common;
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
        private Manager manager;

        [SerializeField]
        private List<HoleByInputAction> holesWithInput;

        private Coroutine slapCoroutine;

        public UnityEvent SuccessMouseHit;
        public UnityEvent FailMouseHit;

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
                .DOMove(hole.transform.position, moveToHoleTime)
                .WaitForCompletion();
            Result result = hole.TryGrabMouse();
            if (result.Success)
            {
                scoreCounter.AddPoint();
                SuccessMouseHit?.Invoke();
                if (scoreCounter.IsWinGame)
                {
                    manager.ExitGame(ExitGameState.Win);
                }
            }
            else
            {
                FailMouseHit?.Invoke();
            }
            yield return transform
                .DOMove(startPosition.position, moveFromHoleTime)
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
