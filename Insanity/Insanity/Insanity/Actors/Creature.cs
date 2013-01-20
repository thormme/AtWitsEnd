using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Insanity.GameStates;

namespace Insanity.Actors
{
    public class Creature : Actor
    {
        static int GravityRate = 300;

        public Vector2 Velocity = new Vector2();
        protected IInputAgent mController;

        protected float mHorizontalSpeed;
        protected float mJumpSpeed;

        Vector2[] lastValidPosition = new Vector2[Level.NumInsanityLevels];

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

            for (int i = 0; i < Level.NumInsanityLevels; i++)
            {
                // If there are no colliding tiles
                if (OwnerLevel.GetCollidingTiles(new Rectangle((int)Position.X + 2, (int)Position.Y + 2, (int)Size.X - 4, (int)Size.Y - 4), false, i).Count == 0)
                {
                    lastValidPosition[i] = Position;
                }
            }
            if ((lastValidPosition[OwnerLevel.InsanityLevel] - Position).Length() > 10f)
            {
                FixPosition();
            }

            Velocity.Y += (float)gameTime.ElapsedGameTime.TotalSeconds * GravityRate;

            Position += (float)gameTime.ElapsedGameTime.TotalSeconds * Velocity;

        }

        private bool CheckIfRoom(int posX, int posY, int horizontalTiles, int verticalTiles)
        {
            for (int x = 0; x < horizontalTiles; x++)
            {
                for (int y = 0; y < verticalTiles; y++)
                {
                    if (!(OwnerLevel.IsWithinLevel(posX + x, posY + y) && !OwnerLevel.GetTile(posX + x, posY + y).Solid))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        protected void FixPosition()
        {
            int numVerticalTiles = (int)Math.Ceiling((Size.Y) / (float)Tile.Height);
            int numHorizontalTiles = (int)Math.Ceiling((Size.X) / (float)Tile.Width);

            Queue<Point> positionQueue = new Queue<Point>();
            List<Point> checkedPositions = new List<Point>();

            Point positionPoint = new Point((int)(Position.X / Tile.Width), (int)(Position.Y / Tile.Height));
            positionQueue.Enqueue(positionPoint);
            checkedPositions.Add(positionPoint);

            while (positionQueue.Count > 0)
            {
                Point curPosition = positionQueue.Dequeue();
                if (CheckIfRoom(curPosition.X, curPosition.Y, numHorizontalTiles, numVerticalTiles))
                {
                    Position = new Vector2(curPosition.X * Tile.Width, curPosition.Y * Tile.Height);
                    return;
                }
                Point up = new Point(curPosition.X, curPosition.Y - 1);
                Point down = new Point(curPosition.X, curPosition.Y + 1);
                Point left = new Point(curPosition.X - 1, curPosition.Y);
                Point right = new Point(curPosition.X + 1, curPosition.Y);

                if (!checkedPositions.Contains(up))
                {
                    positionQueue.Enqueue(up);
                    checkedPositions.Add(up);
                }
                if (!checkedPositions.Contains(down))
                {
                    positionQueue.Enqueue(down);
                    checkedPositions.Add(down);
                }
                if (!checkedPositions.Contains(left))
                {
                    positionQueue.Enqueue(left);
                    checkedPositions.Add(left);
                }
                if (!checkedPositions.Contains(right))
                {
                    positionQueue.Enqueue(right);
                    checkedPositions.Add(right);
                }
            }

            Position = lastValidPosition[OwnerLevel.InsanityLevel];
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
