using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Insanity
{
    public interface IGamestateManager
    {
        void Push(IGamestate state);
        IGamestate Pop();
        IGamestate Current { get; }
    }
}
