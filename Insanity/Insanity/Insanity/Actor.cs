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
        public Vector2 Positon;
        public Vector2 Size;
        Sprite Sprite;

        public Actor(Vector2 position, Vector2 size, Sprite sprite)
        {
            Positon = position;
            Size = size;
            Sprite = sprite;
        }

        public virtual void Draw(Camera camera, SpriteBatch spriteBatch, GameTime gameTime)
        {
            Sprite.Draw(gameTime, spriteBatch, new Rectangle(
                (int)Positon.X + (int)camera.Position.X,
                (int)Positon.Y + (int)camera.Position.Y,
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
