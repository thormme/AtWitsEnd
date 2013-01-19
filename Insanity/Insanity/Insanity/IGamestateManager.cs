using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Insanity
{
    public interface IGamestateManager
    {
        void push(IGamestate state);
        void pop(IGamestate state);
        IGamestate Current {get; private set}
    }
}
