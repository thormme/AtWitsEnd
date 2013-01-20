using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Insanity.Actors;
using Insanity.GameStates;

namespace Insanity
{
    public interface IInputAgent
    {
        void Update(GameTime gameTime, Actor agent = null);

        void GiveLevel(Level level);

        bool MoveRight();

        bool MoveLeft();

        bool Jump();
    }
}
