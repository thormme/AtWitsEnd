using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Insanity.GameStates;
using Microsoft.Xna.Framework.Graphics;
using Insanity.Actors;

namespace Insanity
{
    public class Player : Creature
    {
        // 0-fully sane 1-insane;
        public double InsanityLevel = 0;

        public Player(Vector2 position)
            : base(position, new Vector2(120, 180), new Sprite("spriteSheets/player sane spritesheet"))
        {
        }

        public Player(List<string> args)
            : this(new Vector2(float.Parse(args[0]), float.Parse(args[1])))
        {
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (InsanityGame.Input.MoveLeft())
            {
                Velocity.X = -mHorizontalSpeed;
                facingLeft = true;
            }
            if (InsanityGame.Input.MoveRight())
            {
                Velocity.X = mHorizontalSpeed;
                facingLeft = false;
            }
            if (InsanityGame.Input.Jump())
            {
                Velocity.Y = -mJumpSpeed;
            }
            if (InsanityGame.Input.Pause())
            {
                InsanityGame.GamestateManager.Push(new PauseState());
            }

            Velocity.Y += (float)gameTime.ElapsedGameTime.TotalSeconds * 40;
            Position += (float)gameTime.ElapsedGameTime.TotalSeconds * Velocity;

            //List<Tile> collidingTiles = OwnerLevel.GetCollidingTiles(new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y), true);
            List<Tile> collidingFootTiles = OwnerLevel.GetCollidingTiles(new Rectangle((int)Position.X, (int)Position.Y + (int)Size.Y - 30, (int)Size.X, 30), false);
            if (collidingFootTiles.Count > 0)
            {
                Velocity.Y = 0;
                Position.Y = collidingFootTiles[0].Y - (int)Size.Y;
            }
        }
    }
}
