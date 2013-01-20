using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Insanity.Actors;

namespace Insanity
{
    public class HUD
    {
        protected Sprite sanityOverlay;
        protected Sprite pillMarker;
        protected Sprite sanityFill;
        protected Sprite sanityBackground;
        protected SpriteFont hudFont;

        protected Rectangle backgroundPosition;
        protected Rectangle fillPosition;
        protected Rectangle overlayPosition;
        protected Rectangle pillPosition;

        protected string text;
        protected Vector2 textPosition;

        public HUD()
        {
            sanityOverlay = new Sprite("hudElements/sanityBarOutline");
            sanityFill = new Sprite("hudElements/sanityBarFills");
            sanityBackground = new Sprite("hudElements/sanityBarBackground");
            pillMarker = new Sprite("spriteSheets/pill");

            hudFont = InsanityGame.GameFonts["fonts/hudFont"];
            text = "";

            textPosition = new Vector2(360, 70);

            overlayPosition = new Rectangle(30, 25, 240, 60);
            pillPosition = new Rectangle(290, 25, 30, 60);
        }

        public virtual void Update(GameTime gameTime, Player player)
        {
            text = string.Format("x{0}", player.CurrentPills);
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            sanityBackground.Draw(gameTime, spriteBatch, backgroundPosition, false);
            sanityFill.Draw(gameTime, spriteBatch, fillPosition, false);
            sanityOverlay.Draw(gameTime, spriteBatch, overlayPosition, false);
            pillMarker.Draw(gameTime, spriteBatch, pillPosition, false);

            //draw text component
            Vector2 fontOrigin = hudFont.MeasureString(text) / 2;
            spriteBatch.DrawString(hudFont, text, textPosition, Color.Black, 0, fontOrigin, 1, SpriteEffects.None, 0);
        }
    }
}
