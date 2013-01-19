using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Insanity
{
    interface IGamestate
    {
        virtual void Update();
        virtual void Draw();
        virtual void Initialize();
        virtual void LoadContent();
        virtual void UnloadContent();
    }
}
