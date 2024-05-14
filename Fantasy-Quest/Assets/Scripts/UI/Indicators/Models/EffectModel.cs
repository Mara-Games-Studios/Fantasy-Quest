using System;
using Audio;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Indicators
{
    [Serializable]
    public class EffectModel
    {
        [Required]
        public HorizontalLayoutGroup Indicators;

        [Required]
        public IndicatorsButtons IndicatorsButtons;

        [Required]
        public CanvasGroup IndicatorsAlpha;

        [SerializeField]
        public SoundPlayer SlideSound;
        
        [SerializeField]
        public SoundPlayer EnterSound;
    
        [HideInInspector]
        public RectTransform RectTransform;
        public Tween VanishingTween;
        public float FadeDuration = 1f;
        public float MinAlpha = 0.1f;
        public float MaxAlpha = 1f;
        public int DefaultLeftOffset = 18;
        public int AdditiveSpacing = 30;

        public void Initialize()
        {
            RectTransform = Indicators.GetComponent<RectTransform>();
        }
    }
}
