using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Insanity.Actors
{
    public class Creature : Actor
    {
        public Vector2 Velocity = new Vector2();
        protected IController mController;

        protected float mHorizontalSpeed;
        protected float mJumpSpeed;

        public Creature(Vector2 position, Vector2 size, Sprite sprite, IController controller, float horizontalSpeed = 60, float jumpSpeed = 90)
            : base(position, size, sprite)
        {
            mController = controller;
            mHorizontalSpeed = horizontalSpeed;
            mJumpSpeed = jumpSpeed;
        }

        public virtual void Move(GameTime gameTime)
        {
            if (mController.MoveLeft())
            {
                Velocity.X = -mHorizontalSpeed;
                facingLeft = true;
            }
            if (mController.MoveRight())
            {
                Velocity.X = mHorizontalSpeed;
                facingLeft = false;
            }
            if (mController.Jump())
            {
                Velocity.Y = -mJumpSpeed;
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

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            mController.Update(gameTime);
            Move(gameTime);
        }
    }
}
