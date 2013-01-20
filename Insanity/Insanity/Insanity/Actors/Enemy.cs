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

        protected Sprite VisibleSprite;
        protected Sprite HarmfulSprite;

        protected enum SanityState
        {
            Visible, Harmful, Neither
        }

        protected SanityState currentState;

        public Enemy(Vector2 position, Vector2 size, string visibleSprite, string harmfulSprite, 
            IInputAgent inputBot, double visSanLevel = 0, double harmSanLevel = 0)
            : base(position, size, new Sprite(visibleSprite), inputBot)
        {
            visibleSanityLevel = visSanLevel;
            harmfulSanityLevel = harmSanLevel;

            VisibleSprite = Sprite;
            HarmfulSprite = new Sprite(harmfulSprite);

            currentState = SanityState.Neither;
        }

        public virtual bool IsVisible(double sanityLevel)
        {
            return sanityLevel > visibleSanityLevel;
        }

        public virtual bool IsHarmful(double sanityLevel)
        {
            return sanityLevel > harmfulSanityLevel;
        }

        public override void Update(GameTime gameTime, double insanityLevel)
        {
            base.Update(gameTime, insanityLevel);
            SanityState newState;

            if (insanityLevel < visibleSanityLevel)
            {
                newState = SanityState.Neither;
            }
            else if (insanityLevel < harmfulSanityLevel)
            {
                newState = SanityState.Visible;
            }
            else
            {
                newState = SanityState.Harmful;
            }

            if (newState != currentState)
            {
                ChangeSprite(newState);
            }

            currentState = newState;
        }

        private void ChangeSprite(SanityState newState)
        {
            switch (newState)
            {
                case SanityState.Neither:
                    break;
                case SanityState.Visible:
                    ChangeSprite(VisibleSprite);
                    break;
                case SanityState.Harmful:
                    ChangeSprite(HarmfulSprite);
                    break;
                default:
                    break;
            }
        }
    }
}
