using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Insanity.Actors
{
    public class Creature : Actor
    {
        public Vector2 Velocity = new Vector2();

        protected float mHorizontalSpeed = 30;
        protected float mJumpSpeed = 90;

        public Creature(Vector2 position, Vector2 size, Sprite sprite)
            : base(position, size, sprite)
        {
        }

        public virtual void move()
        {
        }
    }
}
