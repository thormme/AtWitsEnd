using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Insanity.GameStates
{
    public class LoadingScreen : IGamestate
    {
        private SpriteBatch spriteBatch;
        private String Text;
        private Vector2 TextPosition;

        private const char dot = '.';
        private double animationTime;
        private int dotCount;


        public LoadingScreen()
        {
            Text = "Loading";

            Vector2 textSize = InsanityGame.GameFonts["fonts/TitleFont"].MeasureString(Text);
            TextPosition = new Vector2(InsanityGame.ScreenWidth / 2 - textSize.X / 2, InsanityGame.ScreenHeight / 2 - textSize.Y / 2);
        }

        public void Update(GameTime gameTime)
        {
            animationTime += gameTime.ElapsedGameTime.TotalMilliseconds;

            if(animationTime > 1000)
            {
                dotCount = (dotCount + 1) % 5;
                animationTime -= 1000;
            }
        }

        public void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            string text = Text + new String(dot, dotCount);

            spriteBatch.DrawString(InsanityGame.GameFonts["fonts/TitleFont"], text, TextPosition, Color.White);

            spriteBatch.End();
        }

        public void Initialize(ContentManager Content, GraphicsDeviceManager graphics)
        {
            spriteBatch = new SpriteBatch(graphics.GraphicsDevice);
        }

        public void LoadContent()
        {

        }

        public void UnloadContent()
        {

        }

        public bool Loaded
        {
            get { return true; }
        }
    }
}
