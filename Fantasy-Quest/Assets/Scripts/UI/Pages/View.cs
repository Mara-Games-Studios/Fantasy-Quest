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
            model.PageInfo.CanvasGroup.alpha = model.MinAlpha;
            model.PageInfo.RectTransform.position = model.StartPoint.position;
            model.PageInfo.CanvasGroup.gameObject.SetActive(model.IsActiveOnAwake);
        }

        [Button]
        public void Show()
        {
            model.PageInfo.CanvasGroup.gameObject.SetActive(true);
            StopTweens();
            fadeTween = model.PageInfo.CanvasGroup.DOFade(model.MaxAlpha, model.Duration);
            fadeTween.onComplete += () => OnPageShowing?.Invoke(imageButtons);
            if (model.IsUsingMovement)
            {
                moveTween = model.PageInfo.RectTransform.DOMove(
                    model.EndPoint.position,
                    model.Duration
                );
            }
        }

        [Button]
        public void Hide()
        {
            StopTweens();
            OnPageHiding?.Invoke();
            fadeTween = model.PageInfo.CanvasGroup.DOFade(model.MinAlpha, model.Duration);
            fadeTween.onComplete += () => model.PageInfo.CanvasGroup.gameObject.SetActive(false);
            if (model.IsUsingMovement)
            {
                moveTween = model.PageInfo.RectTransform.DOMove(
                    model.StartPoint.position,
                    model.Duration
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
