using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Insanity.GameStates;

namespace Insanity.Actors
{
    public class Actor
    {
        public Vector2 Position;
        public Vector2 Size;
        protected Sprite Sprite;
        protected bool facingLeft;

        public virtual Level OwnerLevel { get; set; }

        public Actor(Vector2 position, Vector2 size, Sprite sprite)
        {
            Position = position;
            Size = size;
            Sprite = sprite;
            facingLeft = false;
        }

        public virtual void Draw(Camera camera, SpriteBatch spriteBatch, GameTime gameTime)
        {
            Sprite.Draw(gameTime, spriteBatch, new Rectangle(
                (int)Position.X - (int)camera.Position.X,
                (int)Position.Y - (int)camera.Position.Y,
                (int)Size.X,
                (int)Size.Y),
                facingLeft);
        }

        public virtual void Update(GameTime gameTime, double insanityLevel)
        {
            Sprite.Update(gameTime);
        }

        public bool IsTouching(Actor actor)
        {
            bool intersect;
            Rectangle otherBounds = new Rectangle((int)actor.Position.X, (int)actor.Position.Y, (int)actor.Size.X, (int)actor.Size.Y);
            new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y).Intersects(ref otherBounds, out intersect);
            return intersect;
        }
    }
}
