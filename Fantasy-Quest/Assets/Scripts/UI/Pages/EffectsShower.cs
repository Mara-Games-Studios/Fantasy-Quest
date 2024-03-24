﻿using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace UI.Pages
{
    [Serializable]
    public class EffectsShower
    {
        [SerializeField]
        private PageInfo pageInfo;

        [SerializeField]
        private RectTransform startPoint;

        [SerializeField]
        private RectTransform middlePoint;
        
        [SerializeField]
        private RectTransform endPoint;

        [SerializeField]
        private float minAlpha = 0.15f;

        [SerializeField]
        private float maxAlpha = 1f;

        [SerializeField]
        private float moveDuration = 1f;
        
        [SerializeField]
        private float fadeDuration = 1f;

        private Tween moveTween;
        private Tween fadeTween;

        public event Action OnEffectShowed;
        
        public void Initialize()
        {
            pageInfo.CanvasGroup.alpha = minAlpha;
            pageInfo.RectTransform.position = startPoint.position;
            pageInfo.CanvasGroup.gameObject.SetActive(false);
        }

        [Button]
        public void Show()
        {
            MoveToStartPosition();
            pageInfo.CanvasGroup.gameObject.SetActive(true);
            StopTweens();
            
            fadeTween = pageInfo.CanvasGroup.DOFade(maxAlpha, fadeDuration);
            fadeTween.onComplete += () => OnEffectShowed?.Invoke();
            
            moveTween = pageInfo.RectTransform.DOMove(
                middlePoint.position,
                moveDuration
            );
        }

        [Button]
        public void Hide()
        {
            StopTweens();
            fadeTween = pageInfo.CanvasGroup.DOFade(minAlpha, fadeDuration);
            
            moveTween = pageInfo.RectTransform.DOMove(
                endPoint.position,
                moveDuration
            );

            moveTween.onComplete += MoveToStartPosition;
        }

        private void MoveToStartPosition()
        {
            pageInfo.RectTransform.DOMove(startPoint.position, 0);
            pageInfo.CanvasGroup.DOFade(minAlpha, 0);
            pageInfo.CanvasGroup.gameObject.SetActive(false);
        }
        
        private void StopTweens()
        {
            moveTween?.Kill();
            fadeTween?.Kill();
        }
    }
    
    [Serializable]
    public struct PageInfo
    {
        [Required]
        public CanvasGroup CanvasGroup;

        [Required]
        public RectTransform RectTransform;
    }
}
