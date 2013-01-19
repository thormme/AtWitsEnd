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
        public MainMenu() : base()
        {
            TitleText = "Main Menu";
            TitlePosition = new Vector2(300, 100);
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
