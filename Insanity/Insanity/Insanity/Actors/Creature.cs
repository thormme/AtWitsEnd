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
            int feetHeight = 10;
            int sideWidth = 30;
            List<Tile> collidingLeftTiles = OwnerLevel.GetCollidingTiles(new Rectangle((int)Position.X, (int)Position.Y, (int)sideWidth, (int)Size.Y - feetHeight), false);
            List<Tile> collidingRightTiles = OwnerLevel.GetCollidingTiles(new Rectangle((int)Position.X + (int)Size.X - sideWidth, (int)Position.Y, (int)sideWidth, (int)Size.Y - feetHeight), false);
            List<Tile> collidingFootTiles = OwnerLevel.GetCollidingTiles(new Rectangle((int)Position.X + 2, (int)Position.Y + (int)Size.Y - feetHeight, (int)Size.X - 2, feetHeight), false).Where((tile) => 
            {
                return !(collidingLeftTiles.Contains(tile) || collidingRightTiles.Contains(tile)); 
            }).ToList();

            bool onGround = collidingFootTiles.Count > 0 && Velocity.Y > 0;

            if (onGround)
            {
                Velocity.Y = 0;
                Position.Y = collidingFootTiles[0].Y - (int)Size.Y;
            }
            if (collidingLeftTiles.Count > 0)
            {
                Velocity.X = 0;
                Position.X = collidingLeftTiles[0].X + Tile.Width;
            }
            if (collidingRightTiles.Count > 0)
            {
                Velocity.X = 0;
                Position.X = collidingRightTiles[0].X - Size.X;
            }

            if (mController.MoveLeft())
            {
                Velocity.X = -mHorizontalSpeed;
                facingLeft = true;
                if (onGround)
                {
                    Sprite.ChangeAnimation("Walk");
                }
            }
            if (mController.MoveRight())
            {
                Velocity.X = mHorizontalSpeed;
                facingLeft = false;
                if (onGround)
                {
                    Sprite.ChangeAnimation("Walk");
                }
            }
            if (mController.Jump() && onGround)
            {
                Velocity.Y = -mJumpSpeed;
                Sprite.ChangeAnimation("Jump");
            }

            Velocity.Y += (float)gameTime.ElapsedGameTime.TotalSeconds * 40;
            Position += (float)gameTime.ElapsedGameTime.TotalSeconds * Velocity;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            mController.Update(gameTime);
            Move(gameTime);
        }
    }
}
