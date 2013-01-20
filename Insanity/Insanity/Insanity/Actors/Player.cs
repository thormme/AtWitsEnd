using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Insanity.GameStates;
using Microsoft.Xna.Framework.Graphics;
using Insanity.Actors;

namespace Insanity.Actors
{
    public class Player : Creature
    {
        public static double perfectlySane = 0;
        public static double inanimateEnemyThreshold = 0.14;
        public static double humanEnemyThreshold = 0.35;
        public static double ghastlyEnemyThreshold = 0.65;
        public static double deadlyInsane = 1.0;

        protected HUD hud;

        protected Sprite saneSprite;
        protected Sprite midsaneSprite;
        protected Sprite insaneSprite;
        
        protected enum SanityState
        {
            Sane, Midsane, Insane
        }

        protected SanityState currentSanity;

        // 0-fully sane 1-insane;
        public double InsanityLevel { get; protected set; }
        public int CurrentPills { get; protected set; }

        public bool IsAttacking { get { return Sprite.GetAnimation().Equals("Fall"); } }

        public Player(Vector2 position)
            : base(position, new Vector2(60, 240), new Sprite("spriteSheets/player sane spritesheet"), new InputHandler(), 300, 300)
        {
            currentSanity = SanityState.Sane;
            saneSprite = Sprite;
            midsaneSprite = new Sprite("spriteSheets/player midsane spritesheet");
            insaneSprite = new Sprite("spriteSheets/player insane spritesheet");
            Sprite.ChangeAnimation("Stand");

            saneSprite.ChangeAnimation("Stand");
            midsaneSprite.ChangeAnimation("Stand");
            insaneSprite.ChangeAnimation("Stand");

            InsanityLevel = 0;
            CurrentPills = 0;
            hud = new HUD();
        }

        public Player(List<string> args)
            : this(new Vector2(float.Parse(args[0]), float.Parse(args[1])))
        {
        }

        public override void Update(GameTime gameTime, double insanityLevel)
        {
            base.Update(gameTime, insanityLevel);

            OwnerLevel.Camera.Position += (Position + Size/2f - (OwnerLevel.Camera.Position + new Vector2(OwnerLevel.ScreenWidth, OwnerLevel.ScreenHeight)/2)) * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if ((mController as InputHandler).Pause())
            {
                InsanityGame.GamestateManager.Push(new PauseState());
            }
            if ((mController as InputHandler).TakePill() && CurrentPills > 0)
            {
                CurrentPills--;
                if (InsanityLevel < humanEnemyThreshold)
                {
                    InsanityLevel = 0;
                }
                else if (InsanityLevel < ghastlyEnemyThreshold)
                {
                    InsanityLevel = inanimateEnemyThreshold;
                }
                else
                {
                    InsanityLevel = humanEnemyThreshold;
                }
            }

            var pills = OwnerLevel.Actors.Where((actor) => { 
                return actor is Pill && IsTouching(actor); 
            });

            foreach (Pill pill in pills)
            {
                CurrentPills++;
                OwnerLevel.RemoveActor(pill);
            }

            var enemies = OwnerLevel.Actors.Where((actor) =>
            {
                return actor is Enemy && IsTouching(actor);
            });

            foreach (var enemy in enemies)
            {
                if(IsAttacking)
                    OwnerLevel.RemoveActor(enemy);
                else if (!IsFrozen)
                {
                    InsanityLevel += .02;
                    IsFrozen = true;
                }
            }

            InsanityLevel += gameTime.ElapsedGameTime.TotalSeconds / 214;

            SanityState newSanity;

            if (InsanityLevel < inanimateEnemyThreshold)
            {
                OwnerLevel.InsanityLevel = 0;
                newSanity = SanityState.Sane;
            } 
            else if (InsanityLevel < humanEnemyThreshold)
            {
                OwnerLevel.InsanityLevel = 1;
                newSanity = SanityState.Midsane;
            }
            else if (InsanityLevel < ghastlyEnemyThreshold)
            {
                OwnerLevel.InsanityLevel = 1;
                newSanity = SanityState.Midsane;
            }
            else
            {
                OwnerLevel.InsanityLevel = 2;
                newSanity = SanityState.Insane;
            }

            if (newSanity != currentSanity)
            {
                ChangeSprite(newSanity);
            }
            currentSanity = newSanity;

            hud.Update(gameTime, this);
        }

        private void ChangeSprite(SanityState newSanity)
        {
            switch (newSanity)
            {
                case SanityState.Sane:
                    ChangeSprite(saneSprite);
                    break;
                case SanityState.Midsane:
                    ChangeSprite(midsaneSprite);
                    break;
                case SanityState.Insane:
                    ChangeSprite(insaneSprite);
                    break;
                default:
                    break;
            }
        }

        public virtual void DrawHud(GameTime gameTime, SpriteBatch spriteBatch)
        {
            hud.Draw(gameTime, spriteBatch);
        }
    }
}
