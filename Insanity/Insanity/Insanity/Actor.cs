using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Insanity
{
    public class Actor
    {
        Vector2 Positon;
        Vector2 Size;
        Sprite Sprite;

        public void Draw()
        {
            Sprite.Draw(new Rectangle(
                (int)Positon.X,
                (int)Positon.Y,
                (int)Size.X,
                (int)Size.Y));
        }

        public void Act()
        {

        }
    }
}
