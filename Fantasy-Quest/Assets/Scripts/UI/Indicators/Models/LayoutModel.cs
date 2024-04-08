using System.Collections.Generic;
using UI.Pages;

namespace UI.Indicators
{
    public class LayoutModel
    {
        public List<IndicatedButton> VerticalButtons;
        public MainMenuInput MainMenuInput;
        public int CurrentButtonIndex;

        public LayoutModel()
        {
            MainMenuInput = new MainMenuInput();
            MainMenuInput.Enable();
            VerticalButtons = new List<IndicatedButton>();
        }
    }
}
