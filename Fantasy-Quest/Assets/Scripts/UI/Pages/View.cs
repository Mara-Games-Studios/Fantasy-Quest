using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Pages
{
    [AddComponentMenu("Scripts/UI/Pages/Pages.View")]
    public class View : MonoBehaviour
    {
        [SerializeField]
        private UI.Pages.Model model;

        [SerializeField]
        private List<Image> imageButtons = new();

        private Tween moveTween;
        private Tween fadeTween;

        public static event System.Action<List<Image>> OnPageShowing;
        public static event System.Action OnPageHiding;

        private void Awake()
        {
            model.pageInfo.canvasGroup.alpha = model.minAlpha;
            model.pageInfo.rectTransform.position = model.startPoint.position;
            model.pageInfo.canvasGroup.gameObject.SetActive(model.isActiveOnAwake);
        }

        [Button]
        public void Show()
        {
            model.pageInfo.canvasGroup.gameObject.SetActive(true);
            StopTweens();
            fadeTween = model.pageInfo.canvasGroup.DOFade(model.maxAlpha, model.duration);
            fadeTween.onComplete += () => OnPageShowing?.Invoke(imageButtons);
            if (model.isUsingMovement)
            {
                moveTween = model.pageInfo.rectTransform.DOMove(
                    model.endPoint.position,
                    model.duration
                );
            }
        }

        [Button]
        public void Hide()
        {
            StopTweens();
            OnPageHiding?.Invoke();
            fadeTween = model.pageInfo.canvasGroup.DOFade(model.minAlpha, model.duration);
            fadeTween.onComplete += () => model.pageInfo.canvasGroup.gameObject.SetActive(false);
            if (model.isUsingMovement)
            {
                moveTween = model.pageInfo.rectTransform.DOMove(
                    model.startPoint.position,
                    model.duration
                );
            }
        }

        private void StopTweens()
        {
            moveTween?.Kill();
            fadeTween?.Kill();
        }
    }
}
