using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Insanity
{
    public abstract class Enemy : Actor
    {
        protected double visibleSanityLevel;
        protected double harmfulSanityLevel;

        public Enemy(Vector2 position, Vector2 size, Sprite sprite, double visSanLevel = 0, double harmSanLevel = 0)
            : base(position, size, sprite)
        {
            visibleSanityLevel = 0;
            harmfulSanityLevel = 0;
        }
    }
}
