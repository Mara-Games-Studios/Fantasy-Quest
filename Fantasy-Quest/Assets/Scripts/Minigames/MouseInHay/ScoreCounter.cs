using Common;
using Configs;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Minigames.MouseInHay
{
    [AddComponentMenu("Scripts/Minigames/MouseInHay/Minigames.MouseInHay.ScoreCounter")]
    internal class ScoreCounter : MonoBehaviour
    {
        [ReadOnly]
        [SerializeField]
        private int score = 0;

        [SerializeField]
        private TMP_Text scoreLabel;

        [SerializeField]
        private int neededScore = 3;

        [SerializeField]
        private Transition.End.Controller endController;

        [SerializeField]
        private InputAction quitGameInputAction;

        [Scene]
        [SerializeField]
        private string nextScene;

        private void Awake()
        {
            quitGameInputAction.performed += QuitGameInputActionPerformed;
        }

        private void QuitGameInputActionPerformed(InputAction.CallbackContext context)
        {
            Debug.Log("Exit by key");
            ExitGame();
        }

        public void AddPoint()
        {
            score++;
        }

        public bool IsWinGame => score == neededScore;

        private void Update()
        {
            scoreLabel.text = score.ToString();
        }

        public void ExitGame()
        {
            endController.LoadScene(nextScene, TransitionSettings.Instance.MinLoadingDuration);
        }

        private void OnEnable()
        {
            quitGameInputAction.Enable();
        }

        private void OnDisable()
        {
            quitGameInputAction.Disable();
        }
    }
}
