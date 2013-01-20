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

        private const double mPhotoViewTime = 1;
        private double mPhotoTimer = -1;

        public bool IsAttacking { get { return Sprite.GetAnimation().Equals("Fall"); } }

        public bool IsFinished { get; protected set; }
        public bool IsDead { get { return InsanityLevel >= Player.deadlyInsane; } protected set { } }

        public Player(Vector2 position)
            : base(position, new Vector2(40, 180), new Sprite("spriteSheets/player sane spritesheet"), new InputHandler(), 300, 400)
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

            OwnerLevel.Camera.Position += (Position + Size/2f - (OwnerLevel.Camera.Position + new Vector2(OwnerLevel.ScreenWidth, OwnerLevel.ScreenHeight)/2)) * (float)gameTime.ElapsedGameTime.TotalSeconds * 4;

            if ((mController as InputHandler).Pause())
            {
                InsanityGame.GamestateManager.Push(new PauseState());
            }

            if ((mController as InputHandler).ViewPhoto() && !IsFrozen)
            {
                mPhotoTimer = mPhotoViewTime;
                IsFrozen = true;
            }

            if (mPhotoTimer > 0)
            {
                mPhotoTimer -= gameTime.ElapsedGameTime.TotalSeconds;
                if (mPhotoTimer <= 0)
                {
                    if (InsanityLevel < inanimateEnemyThreshold)
                    {
                        InsanityLevel = inanimateEnemyThreshold;
                    }
                    else if (InsanityLevel < humanEnemyThreshold)
                    {
                        InsanityLevel = humanEnemyThreshold;
                    }
                    else if (InsanityLevel < ghastlyEnemyThreshold)
                    {
                        InsanityLevel = ghastlyEnemyThreshold;
                    }
                    //IsFrozen = false;
                }
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

            if (OwnerLevel.Actors.Any((actor) => { return actor is DeathArea && IsTouching(actor); }))
            {
                InsanityLevel = Player.deadlyInsane;
            }

            var pills = OwnerLevel.Actors.Where((actor) =>
            {
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
                var Enemy = enemy as Enemy;
                if (Enemy.IsHarmful(InsanityLevel))
                {
                    if (IsAttacking)
                        OwnerLevel.RemoveActor(enemy);
                    else if (!IsFrozen)
                    {
                        InsanityLevel += .02;
                        IsFrozen = true;

                        Rectangle enemyBounds = new Rectangle((int)enemy.Position.X, (int)enemy.Position.Y, (int)enemy.Size.X, (int)enemy.Size.Y); 
                        
                        int feetHeight = 10;
                        int sideWidth = 10;
                        Rectangle leftSide = new Rectangle((int)Position.X, (int)Position.Y + feetHeight, (int)sideWidth, (int)Size.Y - feetHeight * 2);
                        Rectangle rightSide = new Rectangle((int)Position.X + (int)Size.X - sideWidth, (int)Position.Y + feetHeight, (int)sideWidth, (int)Size.Y - feetHeight * 2);

                        if (enemyBounds.Intersects(leftSide))
                        {
                            //bounce right
                            Velocity.X = 100;
                        }
                        else if (enemyBounds.Intersects(rightSide))
                        {
                            //bounce left
                            Velocity.X = -100;
                        }            
                    } 
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

            IsFinished = OwnerLevel.Actors.Any((actor) => { return actor is Goal && IsTouching(actor); });
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
