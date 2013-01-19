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
        ButtonFunc onSelect;
        Sprite sprite;
        String text;

        SpriteFont spriteFont;

        Vector2 fontPosition;
        Rectangle position;

        public Button(String assetName, String buttonText, Rectangle pos, ButtonFunc selectFunc)
        {
            text = buttonText;
            sprite = new Sprite(assetName);
            onSelect = selectFunc;
            position = pos;
            fontPosition = new Vector2(pos.X + pos.Width / 4, pos.Y + pos.Height / 4);
            spriteFont = InsanityGame.GameFonts["ButtonFont"];

            sprite.ChangeAnimation("Selected");
        }

        public void Update(GameTime gameTime)
        {
            sprite.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            sprite.Draw(gameTime, spriteBatch, position, false);
            spriteBatch.DrawString(spriteFont, text, fontPosition, Color.Crimson);
        }
    }
}
