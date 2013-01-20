using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Insanity.Actors.InputBots
{
    public class DoctorInput : IInputAgent
    {
        public void Update(GameTime gameTime)
        {
        }

        public bool MoveRight()
        {
            return false;
        }

        public bool MoveLeft()
        {
            return false;
        }

        public bool Jump()
        {
            return false;
        }
    }
}
