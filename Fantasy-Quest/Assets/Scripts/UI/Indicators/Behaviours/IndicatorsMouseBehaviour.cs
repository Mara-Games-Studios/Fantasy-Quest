using UI.Indicators;
using UnityEngine.UI;

namespace UI.Pages.Behaviours
{
    public class IndicatorsMouseBehaviour : IndicatorsBehaviour
    {
        private Button previousButton;
        private IHorizontalSlider previousSlider;

        public IndicatorsMouseBehaviour(LayoutModel layoutModel, EffectModel effectModel)
            : base(layoutModel, effectModel) { }

        public override void Enable()
        {
            if (LayoutModel?.VerticalButtons == null)
            {
                return;
            }

            IndicatedButton.OnPointerEntered += ShowButtonByMouse;
            OnButtonShowed += SubscribeToSliderSwap;
        }

        public override void Disable()
        {
            if (LayoutModel?.VerticalButtons == null)
            {
                return;
            }

            IndicatedButton.OnPointerEntered -= ShowButtonByMouse;
            OnButtonShowed -= SubscribeToSliderSwap;
        }

        private void SubscribeToSliderSwap(Button button)
        {
            if (previousButton != null)
            {
                EffectModel.IndicatorsButtons.Left.onClick.RemoveListener(previousSlider.MoveLeft);
                EffectModel.IndicatorsButtons.Left.onClick.RemoveListener(
                    EffectModel.SlideSound.PlayClip
                );
                EffectModel.IndicatorsButtons.Right.onClick.RemoveListener(
                    previousSlider.MoveRight
                );
                EffectModel.IndicatorsButtons.Right.onClick.RemoveListener(
                    EffectModel.SlideSound.PlayClip
                );
            }

            if (button.TryGetComponent(out IHorizontalSlider slider))
            {
                EffectModel.IndicatorsButtons.Left.onClick.AddListener(slider.MoveLeft);
                EffectModel.IndicatorsButtons.Left.onClick.AddListener(
                    EffectModel.SlideSound.PlayClip
                );
                EffectModel.IndicatorsButtons.Right.onClick.AddListener(slider.MoveRight);
                EffectModel.IndicatorsButtons.Right.onClick.AddListener(
                    EffectModel.SlideSound.PlayClip
                );

                previousSlider = slider;
                previousButton = button;
            }
        }

        private void ShowButtonByMouse(IndicatedButton button)
        {
            LayoutModel.CurrentButtonIndex = LayoutModel.VerticalButtons.IndexOf(button);
            ShowOn(button, EffectModel);
        }
    }
}
