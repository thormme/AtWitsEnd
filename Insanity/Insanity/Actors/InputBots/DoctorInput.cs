using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Insanity.GameStates;

namespace Insanity.Actors.InputBots
{
    public class DoctorInput : IInputAgent
    {
        protected bool movingRight;

        protected Level LevelRef;

        public void GiveLevel(Level level)
        {
            LevelRef = level;
        }

        public DoctorInput()
        {
            movingRight = true;
        }

        public void Update(GameTime gameTime, Actor agent)
        {
            var doctor = agent as DoctorMonster;

            if ((movingRight && doctor.onRightWall) || (!movingRight && doctor.onLeftWall))
                switchDirection();
        }

        private void switchDirection()
        {
            movingRight = !movingRight;
        }

        public bool MoveRight()
        {
            return movingRight;
        }

        public bool MoveLeft()
        {
            return !MoveRight();
        }

        public bool Jump()
        {
            return false;
        }
    }
}
