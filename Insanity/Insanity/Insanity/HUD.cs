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
            backgroundPosition = overlayPosition;
            fillPosition = overlayPosition;
            pillPosition = new Rectangle(290, 25, 30, 60);
        }

        public virtual void Update(GameTime gameTime, Player player)
        {
            text = string.Format("x{0}", player.CurrentPills);

            double insanity = player.InsanityLevel;
            int x;
            if (insanity < Player.inanimateEnemyThreshold)
            {
                sanityFill.ChangeAnimation("Green");
                x = (int)(insanity * 60 / Player.inanimateEnemyThreshold);
                
                fillPosition = new Rectangle(30 + x, fillPosition.Y, 240 - x, fillPosition.Height);
            }
            else if (insanity < Player.humanEnemyThreshold)
            {
                sanityFill.ChangeAnimation("Yellow");
                x = (int)(insanity * 60 / (Player.humanEnemyThreshold - Player.inanimateEnemyThreshold) 
                    - Player.inanimateEnemyThreshold * 60 / (Player.humanEnemyThreshold - Player.inanimateEnemyThreshold));

                fillPosition = new Rectangle(90 + x, fillPosition.Y, 180 - x, fillPosition.Height);
            }
            else if (insanity < Player.ghastlyEnemyThreshold)
            {
                sanityFill.ChangeAnimation("Orange");

                x = (int)(insanity * 60 / (Player.ghastlyEnemyThreshold - Player.humanEnemyThreshold)
                    - Player.humanEnemyThreshold * 60 / (Player.ghastlyEnemyThreshold - Player.humanEnemyThreshold));

                fillPosition = new Rectangle(150 + x, fillPosition.Y, 120 - x, fillPosition.Height);
            }
            else
            {
                sanityFill.ChangeAnimation("Red");

                x = (int)(insanity * 60 / (Player.deadlyInsane - Player.ghastlyEnemyThreshold)
                    - Player.ghastlyEnemyThreshold * 60 / (Player.deadlyInsane - Player.ghastlyEnemyThreshold));

                fillPosition = new Rectangle(210 + x, fillPosition.Y, 60 - x, fillPosition.Height);
            }
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
