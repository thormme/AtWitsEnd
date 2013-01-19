using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Insanity
{
    public interface IGamestate
    {
        void Update(GameTime gameTime);
        void Draw(GameTime gameTime);
        void Initialize();
        void LoadContent();
        void UnloadContent();
    }
}
