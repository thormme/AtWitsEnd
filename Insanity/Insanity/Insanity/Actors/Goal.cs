using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Insanity.Actors
{
    public class Goal : Actor
    {
        public Goal(Vector2 position, string nextLevel)
            : base(position, new Vector2(60,120), new Sprite("spriteSheets/goal"))
        {
            NextLevel = nextLevel;
        }

        public Goal(List<string> args)
            : this(new Vector2(float.Parse(args[0]), float.Parse(args[1])), args[2])
        {
        }

        public string NextLevel { get; protected set; }
    }
}
