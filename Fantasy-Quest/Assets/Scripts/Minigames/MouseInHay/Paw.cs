using System;
using System.Collections.Generic;
using System.Linq;
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
        private Dictionary<Hole, InputAction> HolesWithInput =>
            holesWithInput.ToDictionary(x => x.Hole, x => x.InputAction);

        public UnityEvent SuccessMouseHit;
        public UnityEvent FailMouseHit;
        public UnityEvent MouseCatches;

        private void Awake()
        {
            foreach (KeyValuePair<Hole, InputAction> pair in HolesWithInput)
            {
                pair.Value.performed += (context) => MoveTo(pair.Key);
            }
        }

        private void MoveTo(Hole hole)
        {
            if (isInAction)
            {
                return;
            }

            isInAction = true;
            Tween moveToHole = transform.DOMove(hole.transform.position, moveToHoleTime);
            moveToHole.onComplete += () => SlapMouse(hole);
        }

        private void SlapMouse(Hole hole)
        {
            Result result = hole.TryGrabMouse();
            if (result.Success)
            {
                scoreCounter.AddPoint();
                if (scoreCounter.IsWinGame)
                {
                    MouseCatches?.Invoke();
                    Debug.Log("You win game");
                    manager.ExitGame();
                    return;
                }
                else
                {
                    SuccessMouseHit?.Invoke();
                }
            }
            else
            {
                FailMouseHit?.Invoke();
            }
            Tween moveFromHole = transform.DOMove(startPosition.position, moveFromHoleTime);
            moveFromHole.onComplete += () => isInAction = false;
        }

        private void OnEnable()
        {
            foreach (InputAction inputAction in HolesWithInput.Values)
            {
                inputAction.Enable();
            }
        }

        private void OnDisable()
        {
            foreach (InputAction inputAction in HolesWithInput.Values)
            {
                inputAction.Disable();
            }
        }
    }
}
