using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Insanity.GameStates
{
    public class DeadState : MenuBase
    {
        public DeadState()
            : base()
        {
            TitleText = "You have become completely insane!\n\n                         Retry?";
            TitlePosition = new Vector2(InsanityGame.ScreenWidth / 2, InsanityGame.ScreenHeight / 4);
        }

        public void ResumeFunc()
        {
            InsanityGame.GamestateManager.Pop();
        }

        public void QuitFunc()
        {
            InsanityGame.GamestateManager.Pop();
            InsanityGame.GamestateManager.Pop();
            InsanityGame.GamestateManager.Push(new MainMenu());
        }

        public override List<Button> CreateButtons()
        {
            var menuButtons = new List<Button>();

            var resumeButton = new Button("Button", "Yes", new Rectangle(InsanityGame.ScreenWidth / 2 - 60, InsanityGame.ScreenHeight * 3/ 8, 120, 60), ResumeFunc);
            var quitButton = new Button("Button", "No", new Rectangle(InsanityGame.ScreenWidth / 2 - 60, InsanityGame.ScreenHeight * 5 / 8, 120, 60), QuitFunc);

            menuButtons.Add(resumeButton);
            menuButtons.Add(quitButton);

            return menuButtons;
        }
    }
}
