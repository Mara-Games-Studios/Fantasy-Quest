using UI.Indicators;

namespace UI.Pages.Behaviours
{
    public class IndicatorsKeyboardBehaviour : IndicatorsBehaviour
    {
        public IndicatorsKeyboardBehaviour(LayoutModel layoutModel, EffectModel effectModel)
            : base(layoutModel, effectModel) { }

        public override void Enable()
        {
            LayoutModel.MainMenuInput.UI.Down.performed += ctx => GoDown();
            LayoutModel.MainMenuInput.UI.Up.performed += ctx => GoUp();
            LayoutModel.MainMenuInput.UI.Left.performed += ctx => GoLeft();
            LayoutModel.MainMenuInput.UI.Right.performed += ctx => GoRight();
            LayoutModel.MainMenuInput.UI.EnterClick.performed += ctx => Click();
        }

        public override void Disable()
        {
            LayoutModel.MainMenuInput.UI.Down.performed -= ctx => GoDown();
            LayoutModel.MainMenuInput.UI.Up.performed -= ctx => GoUp();
            LayoutModel.MainMenuInput.UI.Left.performed -= ctx => GoLeft();
            LayoutModel.MainMenuInput.UI.Right.performed -= ctx => GoRight();
            LayoutModel.MainMenuInput.UI.EnterClick.performed -= ctx => Click();
        }

        private void GoDown()
        {
            if (LayoutModel.VerticalButtons == null || LayoutModel.VerticalButtons.Count == 0)
            {
                return;
            }

            LayoutModel.CurrentButtonIndex--;
            if (LayoutModel.CurrentButtonIndex < 0)
            {
                LayoutModel.CurrentButtonIndex = LayoutModel.VerticalButtons.Count - 1;
            }

            if (!LayoutModel.VerticalButtons[LayoutModel.CurrentButtonIndex].gameObject.activeSelf)
            {
                GoDown();
                return;
            }

            ShowOn(LayoutModel.VerticalButtons[LayoutModel.CurrentButtonIndex], EffectModel);
        }

        private void GoUp()
        {
            if (LayoutModel.VerticalButtons == null || LayoutModel.VerticalButtons.Count == 0)
            {
                return;
            }
            LayoutModel.CurrentButtonIndex++;
            if (LayoutModel.CurrentButtonIndex >= LayoutModel.VerticalButtons.Count)
            {
                LayoutModel.CurrentButtonIndex = 0;
            }

            if (!LayoutModel.VerticalButtons[LayoutModel.CurrentButtonIndex].gameObject.activeSelf)
            {
                GoUp();
                return;
            }

            ShowOn(LayoutModel.VerticalButtons[LayoutModel.CurrentButtonIndex], EffectModel);
        }

        private void GoLeft()
        {
            if (LayoutModel.VerticalButtons == null || LayoutModel.VerticalButtons.Count == 0)
            {
                return;
            }

            if (
                LayoutModel
                    .VerticalButtons[LayoutModel.CurrentButtonIndex]
                    .TryGetComponent(out IHorizontalSlider horizontalSlider)
            )
            {
                horizontalSlider.MoveLeft();
            }
        }

        private void GoRight()
        {
            if (LayoutModel.VerticalButtons == null || LayoutModel.VerticalButtons.Count == 0)
            {
                return;
            }

            if (
                LayoutModel
                    .VerticalButtons[LayoutModel.CurrentButtonIndex]
                    .TryGetComponent(out IHorizontalSlider horizontalSlider)
            )
            {
                horizontalSlider.MoveRight();
            }
        }

        private void Click()
        {
            if (LayoutModel.VerticalButtons == null || LayoutModel.VerticalButtons.Count == 0)
            {
                return;
            }

            LayoutModel.VerticalButtons[LayoutModel.CurrentButtonIndex].onClick.Invoke();
        }
    }
}
