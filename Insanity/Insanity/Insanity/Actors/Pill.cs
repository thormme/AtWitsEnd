using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Insanity.Actors
{
    public class Pill : Actor
    {
        public Pill(Vector2 position)
            : base(position, new Vector2(60,120), new Sprite("spriteSheets/pill"))
        {
        }

        public Pill(List<string> args)
            : this(new Vector2(float.Parse(args[0]), float.Parse(args[1])))
        {
        }
    }
}
