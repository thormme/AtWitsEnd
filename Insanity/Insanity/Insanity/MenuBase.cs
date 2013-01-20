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

        protected int currentButton;
        
        public MenuBase()
        {
            loaded = false;
            buttons = CreateButtons();
            currentButton = 0;

            buttons[currentButton].Select();
        }

        public abstract List<Button> CreateButtons();

        public virtual void Update(GameTime gameTime)
        {
            if (InsanityGame.Input.NewDownPress())
            {
                buttons[currentButton].Unselect();
                currentButton++;
                if (currentButton >= buttons.Count)
                {
                    currentButton = 0;
                }
                buttons[currentButton].Select();
            }
            if (InsanityGame.Input.NewUpPress())
            {
                buttons[currentButton].Unselect();
                currentButton--;
                if (currentButton < 0)
                {
                    currentButton = buttons.Count - 1;
                }
                buttons[currentButton].Select();
            }

            if (InsanityGame.Input.NewEnterPress())
            {
                buttons[currentButton].Hit();
            }

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


            Vector2 fontOrigin = InsanityGame.GameFonts["fonts/TitleFont"].MeasureString(TitleText) / 2;
            spriteBatch.DrawString(InsanityGame.GameFonts["fonts/TitleFont"], TitleText, TitlePosition, Color.White, 0, fontOrigin, 1, SpriteEffects.None, 0);

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
