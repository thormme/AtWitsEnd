using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Insanity
{
    public class Actor
    {
        public Vector2 Position;
        public Vector2 Size;
        Sprite Sprite;

        public Level OwnerLevel;

        public Actor(Vector2 position, Vector2 size, Sprite sprite)
        {
            Position = position;
            Size = size;
            Sprite = sprite;
        }

        public virtual void Draw(Camera camera, SpriteBatch spriteBatch, GameTime gameTime)
        {
            Sprite.Draw(gameTime, spriteBatch, new Rectangle(
                (int)Position.X + (int)camera.Position.X,
                (int)Position.Y + (int)camera.Position.Y,
                (int)Size.X,
                (int)Size.Y),
                false);
        }

        public virtual void Update(GameTime gameTime)
        {
            Sprite.Update(gameTime);
        }
    }
}
