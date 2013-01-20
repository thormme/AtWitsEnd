using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Insanity.GameStates;
using Microsoft.Xna.Framework.Graphics;
using Insanity.Actors;

namespace Insanity.Actors
{
    public class Player : Creature
    {
        public static double inanimateEnemyThreshold = 0.14;
        public static double humanEnemyThreshold = 0.35;
        public static double ghastlyEnemyThreshold = 0.65;

        protected HUD hud;

        // 0-fully sane 1-insane;
        public double InsanityLevel { get; protected set; }
        public int CurrentPills { get; protected set; }

        public Player(Vector2 position)
            : base(position, new Vector2(60, 240), new Sprite("spriteSheets/player sane spritesheet"), new InputHandler())
        {
            Sprite.ChangeAnimation("Walk");
            InsanityLevel = 0;
            CurrentPills = 0;
            hud = new HUD();
        }

        public Player(List<string> args)
            : this(new Vector2(float.Parse(args[0]), float.Parse(args[1])))
        {
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            OwnerLevel.Camera.Position += (Position + Size/2f - (OwnerLevel.Camera.Position + new Vector2(OwnerLevel.ScreenWidth, OwnerLevel.ScreenHeight)/2)) * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if ((mController as InputHandler).Pause())
            {
                InsanityGame.GamestateManager.Push(new PauseState());
            }
        }

        public virtual void DrawHud(GameTime gameTime, SpriteBatch spriteBatch)
        {
            hud.Draw(gameTime, spriteBatch);
        }
    }
}
