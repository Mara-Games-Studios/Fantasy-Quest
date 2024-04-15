using System.Collections.Generic;
using UI.Pages;

namespace UI.Indicators
{
    public class LayoutModel
    {
        public List<IndicatedButton> VerticalButtons;
        public GameplayInput MainMenuInput;
        public int CurrentButtonIndex;

        public LayoutModel()
        {
            MainMenuInput = new GameplayInput();
            MainMenuInput.Enable();
            VerticalButtons = new List<IndicatedButton>();
        }
    }
}
