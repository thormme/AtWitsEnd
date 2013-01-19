using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Insanity
{
    public abstract class MenuBase : IGamestate
    {
        private bool loaded;

        protected List<Button> buttons;
        protected SpriteBatch spriteBatch;
        protected String TitleText;
        protected Vector2 TitlePosition;
        
        public MenuBase(List<Button> menuButtons)
        {
            loaded = false;
            buttons = menuButtons ?? new List<Button>();
        }

        public virtual void Update(GameTime gameTime)
        {
            foreach (var button in buttons)
            {
                button.Update(gameTime);
            }
        }

        public virtual void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            foreach (var button in buttons)
            {
                button.Draw(gameTime, spriteBatch);
            }

            spriteBatch.DrawString(InsanityGame.GameFonts["TitleFont"], TitleText, TitlePosition, Color.Crimson);

            spriteBatch.End();
        }

        public virtual void Initialize(ContentManager Content, GraphicsDeviceManager graphics)
        {
            spriteBatch = new SpriteBatch(graphics.GraphicsDevice);
        }

        public virtual void LoadContent()
        {
            loaded = true;
        }

        public virtual void UnloadContent()
        {
        }

        public bool Loaded
        {
            get { return loaded; }
        }
    }
}
