using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Insanity.Actors
{
    public class TextTip : Actor
    {
        private SpriteFont spriteFont;
        private String text;

        public TextTip(Vector2 position, IEnumerable<string> ToolText)
            : base(position, new Vector2(0,0), new Sprite("spriteSheets/goal"))
        {
            spriteFont = InsanityGame.GameFonts["fonts/TipFont"];
            text = "";
            foreach (var s in ToolText)
            {
                text += s + " ";
            }
        }

        public TextTip(List<string> args)
            : this(new Vector2(float.Parse(args[0]), float.Parse(args[1])), args.Skip(2))
        {
        }

        public override void Draw(Camera camera, SpriteBatch spriteBatch, GameTime gameTime)
        {
            base.Draw(camera, spriteBatch, gameTime);

            Vector2 position = new Vector2((int)Position.X - (int)camera.Position.X,
                (int)Position.Y - (int)camera.Position.Y);

            spriteBatch.DrawString(spriteFont, text, position, Color.Black);
        }
    }
}
