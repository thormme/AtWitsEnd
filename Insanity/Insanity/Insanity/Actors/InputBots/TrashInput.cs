using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Insanity.GameStates;

namespace Insanity.Actors.InputBots
{
    public class TrashInput : IInputAgent
    {
        protected Level LevelRef;

        protected bool movingRight;

        const int switchTime = 1000; //ms

        private double timer;

        public TrashInput()
        {
            movingRight = false;
            timer = 0;
        }

        public void Update(GameTime gameTime, Actor agent)
        {
            //do stuff
            var can = agent as TrashCan;

            if (!can.IsHarmful(LevelRef.mPlayer.InsanityLevel))
            {
                timer += gameTime.ElapsedGameTime.TotalMilliseconds;
                if (timer > switchTime)
                {
                    timer -= switchTime;
                    switchDirection();
                }
            }
            else if(!LevelRef.mPlayer.IsFrozen)
            {
                movingRight = (can.Position.X < LevelRef.mPlayer.Position.X);
            }

        }

        private void switchDirection()
        {
            movingRight = !movingRight;
        }

        public void GiveLevel(Level level)
        {
            LevelRef = level;
        }

        public bool MoveRight()
        {
            return movingRight;
        }

        public bool MoveLeft()
        {
            return !movingRight;
        }

        public bool Jump()
        {
            return false;
        }
    }
}
