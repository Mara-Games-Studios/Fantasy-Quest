using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Minigames.MouseInHay.SeparateGame
{
    [AddComponentMenu(
        "Scripts/Minigames/MouseInHay/SeparateGame/Minigames.MouseInHay.SeparateGame.ResultScreen"
    )]
    internal class ResultScreen : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup screenGroup;

        [SerializeField]
        private TextMeshProUGUI score;

        [SerializeField]
        private Button confirm;

        private void Awake()
        {
            confirm.onClick.AddListener(Confirm);
        }

        public void Show(int score)
        {
            screenGroup.blocksRaycasts = true;
            this.score.text = $"Результат:\n\n{score} попаданий!\n\nКрасавчик!";
            _ = screenGroup.DOFade(1, 1).OnComplete(() => screenGroup.interactable = true);
        }

        private void Confirm()
        {
            _ = screenGroup
                .DOFade(0, 1)
                .OnComplete(() =>
                {
                    screenGroup.interactable = false;
                    screenGroup.blocksRaycasts = false;
                });
        }
    }
}
