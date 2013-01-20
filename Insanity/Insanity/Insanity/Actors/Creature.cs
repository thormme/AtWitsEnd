using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Insanity.Actors
{
    public class Creature : Actor
    {
        static int GravityRate = 300;

        public Vector2 Velocity = new Vector2();
        protected IInputAgent mController;

        protected float mHorizontalSpeed;
        protected float mJumpSpeed;

        public Creature(Vector2 position, Vector2 size, Sprite sprite, IInputAgent controller, float horizontalSpeed = 60, float jumpSpeed = 90)
            : base(position, size, sprite)
        {
            mController = controller;
            mHorizontalSpeed = horizontalSpeed;
            mJumpSpeed = jumpSpeed;
        }

        public virtual void Move(GameTime gameTime)
        {
            int feetHeight = 10;
            int sideWidth = 10;
            List<Tile> collidingLeftTiles = OwnerLevel.GetCollidingTiles(new Rectangle((int)Position.X, (int)Position.Y + feetHeight, (int)sideWidth, (int)Size.Y - feetHeight * 2), false);
            List<Tile> collidingRightTiles = OwnerLevel.GetCollidingTiles(new Rectangle((int)Position.X + (int)Size.X - sideWidth, (int)Position.Y + feetHeight, (int)sideWidth, (int)Size.Y - feetHeight * 2), false);
            List<Tile> collidingFootTiles = OwnerLevel.GetCollidingTiles(new Rectangle((int)Position.X + 2, (int)Position.Y + (int)Size.Y - feetHeight, (int)Size.X - 4, feetHeight), false).Where((tile) => 
            {
                return !(collidingLeftTiles.Contains(tile) || collidingRightTiles.Contains(tile));
            }).ToList();
            List<Tile> collidingHeadTiles = OwnerLevel.GetCollidingTiles(new Rectangle((int)Position.X + 2, (int)Position.Y, (int)Size.X - 4, feetHeight), false).Where((tile) =>
            {
                return !(collidingLeftTiles.Contains(tile) || collidingRightTiles.Contains(tile));
            }).ToList();

            bool onGround = collidingFootTiles.Count > 0 && Velocity.Y > 0;

            if (onGround)
            {
                Velocity.Y = 0;
                Position.Y = collidingFootTiles[0].Y - (int)Size.Y;
            }
            if (collidingHeadTiles.Count > 0 && Velocity.Y < 0)
            {
                Velocity.Y = 0;
                Position.Y = collidingHeadTiles[0].Y + (int)Tile.Height;
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
            if (!(mController.MoveRight() || mController.MoveLeft()) && onGround)
            {
                Sprite.ChangeAnimation("Stand");
            }
            if (mController.Jump() && onGround)
            {
                Velocity.Y = -mJumpSpeed;
                Sprite.ChangeAnimation("Jump");
            }

            if (onGround)
            {
                Velocity.X *= .95f;
            }
            Velocity.Y += (float)gameTime.ElapsedGameTime.TotalSeconds * GravityRate;
            Position += (float)gameTime.ElapsedGameTime.TotalSeconds * Velocity;
        }

        public override void Update(GameTime gameTime, double insanityLevel)
        {
            base.Update(gameTime, insanityLevel);
            mController.Update(gameTime);
            Move(gameTime);
        }

        public override void Draw(Camera camera, SpriteBatch spriteBatch, GameTime gameTime)
        {
            Sprite.Draw(gameTime, spriteBatch, new Rectangle(
                (int)Position.X - (int)camera.Position.X - (int)Size.X / 2,
                (int)Position.Y - (int)camera.Position.Y,
                (int)Size.X * 2,
                (int)Size.Y),
                facingLeft);
        }

        protected void ChangeSprite(Sprite sprite)
        {
            string currentAnimation = Sprite.GetAnimation();
            int currentFrame = Sprite.GetCurrentFrame();

            Sprite = sprite;
            sprite.ChangeAnimation(currentAnimation, true);
            sprite.SetCurrentFrame(currentFrame);
        }
    }
}
