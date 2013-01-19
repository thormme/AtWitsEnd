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

        public Player(Vector2 position, Vector2 size, Sprite sprite)
        : base(position, size, sprite)
        { 
        }
    }
}
