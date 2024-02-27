using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
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
        private Dictionary<Hole, InputAction> HolesWithInput =>
            holesWithInput.ToDictionary(x => x.Hole, x => x.InputAction);

        public UnityEvent SuccessMouseHit;
        public UnityEvent FailMouseHit;

        private void Awake()
        {
            foreach (KeyValuePair<Hole, InputAction> pair in HolesWithInput)
            {
                pair.Value.performed += (context) => InputPerformed(pair.Key);
            }
        }

        private void InputPerformed(Hole hole)
        {
            if (isInAction)
            {
                return;
            }

            isInAction = true;
            TweenerCore<Vector3, Vector3, VectorOptions> moveToHole = transform.DOMove(
                hole.transform.position,
                moveToHoleTime
            );
            moveToHole.onComplete += () => GrabMouse(hole);
        }

        private void GrabMouse(Hole hole)
        {
            Result result = hole.TryGrabMouse();
            if (result.Success)
            {
                scoreCounter.AddPoint();
                SuccessMouseHit?.Invoke();
            }
            else
            {
                FailMouseHit?.Invoke();
            }
            TweenerCore<Vector3, Vector3, VectorOptions> moveFromHole = transform.DOMove(
                startPosition.position,
                moveFromHoleTime
            );
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
