using System;
using UI.Indicators;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Pages.Behaviours
{
    public abstract class IndicatorsBehaviour
    {
        protected LayoutModel LayoutModel;
        protected EffectModel EffectModel;
        public static event Action<Button> OnButtonShowed;

        public IndicatorsBehaviour(LayoutModel layoutModel, EffectModel effectModel)
        {
            LayoutModel = layoutModel;
            EffectModel = effectModel;
        }

        public abstract void Enable();

        public abstract void Disable();

        public static void ShowOn(Button button, EffectModel effectModel)
        {
            RectTransform rectTransform = button.GetComponent<RectTransform>();
            int spacing =
                Convert.ToInt32(Mathf.Round(rectTransform.rect.width))
                + effectModel.AdditiveSpacing;
            effectModel.Indicators.spacing = spacing;
            effectModel.Indicators.padding.left = (-spacing / 2) - effectModel.DefaultLeftOffset;
            effectModel.RectTransform.position = rectTransform.position;
            OnButtonShowed?.Invoke(button);
        }
    }
}
