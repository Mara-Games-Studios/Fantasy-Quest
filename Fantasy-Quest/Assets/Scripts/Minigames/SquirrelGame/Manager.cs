using System;
using System.Collections;
using Common;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Minigames.SquirrelGame
{
    [AddComponentMenu("Scripts/Minigames/SquirrelGame/Minigames.SquirrelGame.Manager")]
    internal class Manager : MonoBehaviour
    {
        [Scene]
        [SerializeField]
        private string exitScene;

        [SerializeField]
        private Transition.End.Controller endController;

        [SerializeField]
        private float statusPanelShowDuration = 1f;

        [SerializeField]
        private StatusPanel statusPanel;

        private SqirrelGame input;

        private void Awake()
        {
            input = new SqirrelGame();
        }

        private void ExitPerformed(InputAction.CallbackContext context)
        {
            input.Disable();
            statusPanel.ShowPanel(StatusPanel.State.NotFinished);
        }

        private void OnEnable()
        {
            input.Enable();
            input.Player.Exit.performed += ExitPerformed;
        }

        private void OnDisable()
        {
            input.Player.Exit.performed -= ExitPerformed;
            input.Disable();
        }

        public void ExitMinigame()
        {
            _ = StartCoroutine(
                WaitRoutine(statusPanelShowDuration, () => endController.LoadScene(exitScene))
            );
        }

        private IEnumerator WaitRoutine(float time, Action action)
        {
            yield return new WaitForSeconds(time);
            action();
        }
    }
}
