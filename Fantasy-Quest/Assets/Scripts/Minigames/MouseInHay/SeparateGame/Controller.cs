using Audio;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Minigames.MouseInHay.SeparateGame
{
    [AddComponentMenu(
        "Scripts/Minigames/MouseInHay/SeparateGame/Minigames.MouseInHay.SeparateGame.Controller"
    )]
    internal class Controller : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private SoundPlayer music;

        [Required]
        [SerializeField]
        private Hay hay;

        [Required]
        [SerializeField]
        private Paw paw;

        [Required]
        [SerializeField]
        private ScoreCounter scoreCounter;

        [Required]
        [SerializeField]
        private PlayScreen playScreen;

        [SerializeField]
        private int hitsToFail = 3;

        [SerializeField]
        private int failedHits = 0;

        [SerializeField]
        private float timeScaleUpSpeed;

        [SerializeField]
        private TextMeshPro failCounter;

        [SerializeField]
        private ResultScreen resultScreen;

        private bool upscaleTimeScale;

        private void Awake()
        {
            paw.FailMouseHit.AddListener(FailHit);
        }

        public void StartGame()
        {
            music.PlayClip();
            hay.StartShowMouses();
            paw.EnableInput();
            upscaleTimeScale = true;
            Time.timeScale = 1f;
        }

        public void StopGame()
        {
            music.StopClip();
            hay.StopShowMouses();
            paw.DisableInput();
            upscaleTimeScale = false;
            failedHits = 0;
            resultScreen.Show(scoreCounter.Score, Time.timeScale);
            Time.timeScale = 1f;
            scoreCounter.ResetScore();
            playScreen.Show();
        }

        private void FailHit()
        {
            failedHits++;
            if (failedHits >= hitsToFail)
            {
                StopGame();
            }
        }

        private void Update()
        {
            failCounter.text =
                "Промахи: "
                + (hitsToFail - failedHits).ToString()
                + $"\nСкорость: {Time.timeScale:0.00}";
            if (upscaleTimeScale)
            {
                Time.timeScale += Time.deltaTime * timeScaleUpSpeed;
            }
        }
    }
}
