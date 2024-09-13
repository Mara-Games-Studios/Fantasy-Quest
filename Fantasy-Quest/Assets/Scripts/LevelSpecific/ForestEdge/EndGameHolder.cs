using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace LevelSpecific.ForestEdge
{
    [AddComponentMenu("Scripts/LevelSpecific/ForestEdge/LevelSpecific.ForestEdge.EndGameHolder")]
    internal class EndGameHolder : MonoBehaviour
    {
        [SerializeField]
        private float delay;

        private GameplayInput input;

        public UnityEvent OnSkipPerformed;
        private bool enabledInput = false;

        private void Awake()
        {
            input = new GameplayInput();
            input.Player.CutsceneAnyKey.performed += CutsceneAnyKey_performed;
        }

        private void OnEnable()
        {
            input.Enable();
        }

        public void DelayEnable()
        {
            _ = DOVirtual.DelayedCall(delay, () => enabledInput = true, false);
        }

        private void CutsceneAnyKey_performed(InputAction.CallbackContext obj)
        {
            if (!enabledInput)
            {
                return;
            }

            enabledInput = false;
            OnSkipPerformed?.Invoke();
        }

        private void OnDisable()
        {
            input.Disable();
        }
    }
}
