using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Insanity
{
    public class Tile
    {
        public bool Solid { get; protected set; }
        public Sprite Sprite { get; protected set; }
        public int X { get; protected set; }
        public int Y { get; protected set; }
        public int Row { get; protected set; }
        public int Column { get; protected set; }

        public static int Width = 60;
        public static int Height = 60;

        public Tile(bool solid, Sprite sprite, int x, int y, int row, int column)
        {
            Solid = solid;
            Sprite = sprite;
            X = x;
            Y = y;
            Row = row;
            Column = column;
        }

        public virtual void Draw(Camera camera, SpriteBatch spriteBatch, GameTime gameTime)
        {
            Sprite.Draw(gameTime, spriteBatch, new Rectangle(X - (int)camera.Position.X, Y - (int)camera.Position.Y, Width, Height), new Vector2(Row, Column));
        }
    }
}
