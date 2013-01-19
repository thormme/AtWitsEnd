using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Insanity.Actors
{
    public abstract class Enemy : Creature
    {
        protected double visibleSanityLevel;
        protected double harmfulSanityLevel;

        public Enemy(Vector2 position, Vector2 size, Sprite sprite, double visSanLevel = 0, double harmSanLevel = 0)
            : base(position, size, sprite, new InputHandler())
        {
            visibleSanityLevel = visSanLevel;
            harmfulSanityLevel = harmSanLevel;
        }

        public virtual bool IsVisible(double sanityLevel)
        {
            return sanityLevel > visibleSanityLevel;
        }

        public virtual bool IsHarmful(double sanityLevel)
        {
            return sanityLevel > harmfulSanityLevel;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
