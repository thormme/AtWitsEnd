using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Insanity
{
    public class Player : Actor
    {
        // 0-fully sane 1-insane;
        double InsanityLevel;

        public Player(Vector2 position)
        : base(position, new Vector2(120, 180), new Sprite("tiles/ground"))
        {
        }

        public Player(List<string> args)
            : this(new Vector2(float.Parse(args[0]), float.Parse(args[1])))
        {
        }
    }
}
