using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Minigames.MouseInHay.SeparateGame
{
    [AddComponentMenu(
        "Scripts/Minigames/MouseInHay/SeparateGame/Minigames.MouseInHay.SeparateGame.PlayScreen"
    )]
    internal class PlayScreen : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup canvasGroup;

        [SerializeField]
        private TextMeshProUGUI counter;

        [SerializeField]
        private CanvasGroup textCanvasGroup;

        [SerializeField]
        private Button button;

        [SerializeField]
        private CanvasGroup background;

        public UnityEvent OnCountEnded;

        private void Awake()
        {
            button.onClick.AddListener(Play);
        }

        [SerializeField]
        private float textDelay = 0.5f;

        [SerializeField]
        private float lastTextDelay = 0.5f;

        [Button]
        public void Play()
        {
            Sequence sequence = DOTween.Sequence();
            canvasGroup.interactable = false;
            counter.text = "На Старт!";
            _ = sequence.Append(canvasGroup.DOFade(0, 0.5f));
            _ = sequence.Join(textCanvasGroup.DOFade(1, 0.5f));

            _ = sequence.Append(textCanvasGroup.DOFade(0, textDelay));
            _ = sequence.AppendCallback(() => counter.text = "Внимание!");
            _ = sequence.Append(textCanvasGroup.DOFade(1, textDelay));

            _ = sequence.Append(textCanvasGroup.DOFade(0, textDelay));
            _ = sequence.AppendCallback(() => counter.text = "Вперед!");
            _ = sequence.Append(textCanvasGroup.DOFade(1, textDelay));

            _ = sequence.Append(textCanvasGroup.DOFade(0, lastTextDelay));
            _ = sequence.Append(background.DOFade(0, lastTextDelay));
            _ = sequence.AppendCallback(() => OnCountEnded?.Invoke());

            _ = sequence.Play();
        }

        [Button]
        public void Show()
        {
            _ = canvasGroup.DOFade(1, 1);
            _ = textCanvasGroup.DOFade(1, 1);
            _ = background.DOFade(1, 1).OnComplete(() => canvasGroup.interactable = true);
        }
    }
}
