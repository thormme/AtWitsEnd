using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Insanity
{
    public interface IController
    {
        void Update(GameTime gameTime);

        bool MoveRight();

        bool MoveLeft();

        bool Jump();
    }
}
