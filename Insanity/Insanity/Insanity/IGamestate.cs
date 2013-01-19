using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Insanity
{
    interface IGamestate
    {
        virtual void Update(GameTime gameTime);
        virtual void Draw(GameTime gameTime);
        virtual void Initialize();
        virtual void LoadContent();
        virtual void UnloadContent();
    }
}
