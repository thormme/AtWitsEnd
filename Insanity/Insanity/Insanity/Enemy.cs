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
            //act similarly to player
        }
    }
}
