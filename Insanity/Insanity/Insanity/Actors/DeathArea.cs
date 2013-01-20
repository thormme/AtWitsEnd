using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Insanity.Actors
{
    public class DeathArea : Actor
    {
        int blah = Tile.Width;
        public DeathArea(Vector2 position, Vector2 size)
            : base(position, size, new Sprite("spriteSheets/goal"))
        {
            
        }

        public DeathArea(List<string> args)
            : this(new Vector2(float.Parse(args[0]), float.Parse(args[1])), new Vector2(int.Parse(args[2]) * Tile.Width, int.Parse(args[3]) * Tile.Height))
        {
        }
    }
}
