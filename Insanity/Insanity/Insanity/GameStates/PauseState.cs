using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Insanity.GameStates
{
    public class PauseState : MenuBase
    {
        public PauseState()
            : base(new List<Button> { 
                new Button("Button", "Resume", new Rectangle(300,200, 120, 60), () => 
                {
                    InsanityGame.GamestateManager.pop();
                }),
                new Button("Button", "Quit", new Rectangle(300, 300, 120, 60), () => 
                {
                    InsanityGame.GamestateManager.pop();
                    InsanityGame.GamestateManager.pop();
                    InsanityGame.GamestateManager.push(new MainMenu());
                })})
        {
            TitleText = "pause";
            TitlePosition = new Vector2(400, 100);
        }
    }
}
