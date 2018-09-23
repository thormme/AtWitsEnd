using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Insanity.GameStates
{
    public class VictoryState : MenuBase
    {
        public VictoryState()
            : base()
        {
            TitleText = "You have escaped your insanity?\n\n            Please Play Again!";
            TitlePosition = new Vector2(InsanityGame.ScreenWidth / 2, InsanityGame.ScreenHeight / 4);
        }

        public void MainMenuFunc()
        {
            InsanityGame.GamestateManager.Pop();
            InsanityGame.GamestateManager.Push(new MainMenu());
        }

        public override List<Button> CreateButtons()
        {
            var menuButtons = new List<Button>();

            var mainMenuButton = new Button("Button", "Main Menu", new Rectangle(InsanityGame.ScreenWidth / 2 - 60, InsanityGame.ScreenHeight * 5/ 8, 120, 60), MainMenuFunc);

            menuButtons.Add(mainMenuButton);

            return menuButtons;
        }
    }
}
