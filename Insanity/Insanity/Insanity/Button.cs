using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Insanity
{
    public delegate void ButtonFunc();

    public class Button
    {
        protected ButtonFunc onSelect;
        protected Sprite sprite;
        protected String text;

        protected SpriteFont spriteFont;

        protected Vector2 fontPosition;
        protected Rectangle position;

        public Button(String assetName, String buttonText, Rectangle pos, ButtonFunc selectFunc)
        {
            text = buttonText;
            sprite = new Sprite(assetName);
            onSelect = selectFunc;
            position = pos;
            fontPosition = new Vector2(pos.X + pos.Width / 2, pos.Y + pos.Height / 2);
            spriteFont = InsanityGame.GameFonts["ButtonFont"];
        }

        public void Hit()
        {
            onSelect();
        }

        public void Select()
        {
            sprite.ChangeAnimation("Selected");
        }

        public void Unselect()
        {
            sprite.ChangeAnimation("Unselected");
        }

        public void Update(GameTime gameTime)
        {
            sprite.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            sprite.Draw(gameTime, spriteBatch, position, false);

            Vector2 fontOrigin = spriteFont.MeasureString(text) / 2;
            spriteBatch.DrawString(spriteFont, text, fontPosition, Color.Crimson, 0, fontOrigin, 1, SpriteEffects.None, 0);
        }
    }
}
