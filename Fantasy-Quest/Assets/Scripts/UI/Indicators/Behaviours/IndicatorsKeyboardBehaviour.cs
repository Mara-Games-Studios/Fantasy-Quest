using UI.Indicators;
using UnityEngine.InputSystem;

namespace UI.Pages.Behaviours
{
    public class IndicatorsKeyboardBehaviour : IndicatorsBehaviour
    {
        public IndicatorsKeyboardBehaviour(LayoutModel layoutModel, EffectModel effectModel)
            : base(layoutModel, effectModel) { }

        public override void Enable()
        {
            LayoutModel.MainMenuInput.UI.Down.performed += Down_performed;
            LayoutModel.MainMenuInput.UI.Up.performed += Up_performed;
            LayoutModel.MainMenuInput.UI.Left.performed += Left_performed;
            LayoutModel.MainMenuInput.UI.Right.performed += Right_performed;
            LayoutModel.MainMenuInput.UI.EnterClick.performed += EnterClick_performed;
        }

        private void EnterClick_performed(InputAction.CallbackContext obj)
        {
            Click();
        }

        private void Right_performed(InputAction.CallbackContext obj)
        {
            GoRight();
        }

        private void Left_performed(InputAction.CallbackContext obj)
        {
            GoLeft();
        }

        private void Up_performed(InputAction.CallbackContext obj)
        {
            GoUp();
        }

        private void Down_performed(InputAction.CallbackContext obj)
        {
            GoDown();
        }

        public override void Disable()
        {
            LayoutModel.MainMenuInput.UI.Down.performed -= Down_performed;
            LayoutModel.MainMenuInput.UI.Up.performed -= Up_performed;
            LayoutModel.MainMenuInput.UI.Left.performed -= Left_performed;
            LayoutModel.MainMenuInput.UI.Right.performed -= Right_performed;
            LayoutModel.MainMenuInput.UI.EnterClick.performed -= EnterClick_performed;
        }

        private void GoDown()
        {
            if (
                LayoutModel.VerticalButtons == null
                || LayoutModel.VerticalButtons.Count == 0
                || !LayoutModel.Page.EffectedObject.activeSelf
            )
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
            if (
                LayoutModel.VerticalButtons == null
                || LayoutModel.VerticalButtons.Count == 0
                || !LayoutModel.Page.EffectedObject.activeSelf
            )
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
            if (
                LayoutModel.VerticalButtons == null
                || LayoutModel.VerticalButtons.Count == 0
                || !LayoutModel.Page.EffectedObject.activeSelf
            )
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
                EffectModel.SlideSound.PlayClip();
            }
        }

        private void GoRight()
        {
            if (
                LayoutModel.VerticalButtons == null
                || LayoutModel.VerticalButtons.Count == 0
                || !LayoutModel.Page.EffectedObject.activeSelf
            )
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
                EffectModel.SlideSound.PlayClip();
            }
        }

        private void Click()
        {
            if (
                LayoutModel.VerticalButtons == null
                || LayoutModel.VerticalButtons.Count == 0
                || !LayoutModel.Page.EffectedObject.activeSelf
            )
            {
                return;
            }

            LayoutModel.VerticalButtons[LayoutModel.CurrentButtonIndex].onClick.Invoke();
            EffectModel.EnterSound.PlayClip();
        }
    }
}
