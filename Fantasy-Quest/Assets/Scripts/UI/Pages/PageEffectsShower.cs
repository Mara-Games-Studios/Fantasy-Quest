using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace UI.Pages
{
    [AddComponentMenu("Scripts/UI/Pages/UI.Pages.PageEffectsShower")]
    public class PageEffectsShower: MonoBehaviour
    {
        [SerializeField]
        private PageInfo pageInfo;

        [SerializeField]
        private PivotPoints pivotPoints;

        [SerializeField]
        private float minAlpha = 0.15f;

        [SerializeField]
        private float maxAlpha = 1f;

        [SerializeField]
        private float moveDuration = 1.5f;
        
        [SerializeField]
        private float fadeDuration = 1.5f;

        private Tween moveTween;
        private Tween fadeTween;

        public event Action OnEffectShowed;
        
        public void Initialize()
        {
            pageInfo.CanvasGroup.alpha = minAlpha;
            pageInfo.RectTransform.position = pivotPoints.StartPoint.position;
            pageInfo.CanvasGroup.gameObject.SetActive(false);
        }

        [Button]
        public void ShowFromStart()
        {
            MoveToPoint(pivotPoints.StartPoint);
            Show();
        }
        
        [Button]
        public void ShowFromEnd()
        {
            MoveToPoint(pivotPoints.EndPoint);
            Show();
        }

        private void Show()
        {
            pageInfo.CanvasGroup.gameObject.SetActive(true);
            StopTweens();
            
            fadeTween = pageInfo.CanvasGroup.DOFade(maxAlpha, fadeDuration);
            fadeTween.onComplete += () => OnEffectShowed?.Invoke();
            
            moveTween = pageInfo.RectTransform.DOMove(
                pivotPoints.MiddlePoint.position,
                moveDuration
            );
        }

        [Button]
        public void HideToEnd()
        {
            StopTweens();
            fadeTween = pageInfo.CanvasGroup.DOFade(minAlpha, fadeDuration);
            
            moveTween = pageInfo.RectTransform.DOMove(
                pivotPoints.EndPoint.position,
                moveDuration
            );

            moveTween.onComplete += () => MoveToPoint(pivotPoints.StartPoint);
        }

        [Button]
        public void HideToStart()
        {
            StopTweens();
            fadeTween = pageInfo.CanvasGroup.DOFade(minAlpha, fadeDuration);
            
            moveTween = pageInfo.RectTransform.DOMove(
                pivotPoints.StartPoint.position,
                moveDuration
            );

            moveTween.onComplete += () => MoveToPoint(pivotPoints.StartPoint);
        }
        private void MoveToPoint(RectTransform point)
        {
            pageInfo.RectTransform.DOMove(point.position, 0);
            pageInfo.CanvasGroup.DOFade(minAlpha, 0);
            pageInfo.CanvasGroup.gameObject.SetActive(false);
        }
        
        private void StopTweens()
        {
            moveTween?.Kill();
            fadeTween?.Kill();
        }
    }
}
