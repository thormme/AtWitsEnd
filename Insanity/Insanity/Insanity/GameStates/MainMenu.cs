using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace Insanity
{
    public class MainMenu : MenuBase
    {
        public MainMenu()
            : base(new List<Button> { new Button("Button", "Pick Me!", new Rectangle(100, 200, 120, 60), () => { InsanityGame.GamestateManager.Pop(); InsanityGame.GamestateManager.Push(new Level("level0")); }),
                                        new Button("Button", "No, Me!!", new Rectangle(100, 300, 120, 60), () => {InsanityGame.GamestateManager.Pop(); InsanityGame.GamestateManager.Push(new MainMenu());}) })
        {
            TitleText = "Main Menu";
            TitlePosition = new Vector2(400, 100);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public override void Initialize(ContentManager Content, GraphicsDeviceManager graphics)
        {
            base.Initialize(Content, graphics);
        }

        public override void LoadContent()
        {
            base.LoadContent();
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }
    }
}
