﻿using System;
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
        // 0-fully sane 1-insane;
        public double InsanityLevel = 0;

        public Player(Vector2 position)
            : base(position, new Vector2(60, 240), new Sprite("spriteSheets/player sane spritesheet"), new InputHandler())
        {
            
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
        }
    }
}
