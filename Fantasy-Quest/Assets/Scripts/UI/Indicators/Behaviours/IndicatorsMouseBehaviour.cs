using UI.Indicators;

namespace UI.Pages.Behaviours
{
    public class IndicatorsMouseBehaviour: IndicatorsBehaviour
    {
        public IndicatorsMouseBehaviour(LayoutModel layoutModel, EffectModel effectModel) : base(layoutModel, effectModel) {}

        public override void Enable()
        {
            if(LayoutModel?.VerticalButtons == null)
                return;
            
            IndicatedButton.OnPointerEntered += ShowButtonByMouse;
        }

        public override void Disable()
        {
            if(LayoutModel?.VerticalButtons == null)
                return;
            
            IndicatedButton.OnPointerEntered -= ShowButtonByMouse;
        }
        
        private void ShowButtonByMouse(IndicatedButton button)
        {
            LayoutModel.CurrentButtonIndex = LayoutModel.VerticalButtons.IndexOf(button);
            ShowOn(button, EffectModel);
        }
    }
}
