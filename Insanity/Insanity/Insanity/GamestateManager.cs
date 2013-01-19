using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Insanity
{
    public class GamestateManager : IGamestateManager
    {
        Stack<IGamestate> GamestateStack;

        public GamestateManager()
        {
            GamestateStack = new Stack<IGamestate>();
        }

        public void push(IGamestate state)
        {
            throw new NotImplementedException();
        }

        public void pop(IGamestate state)
        {
            throw new NotImplementedException();
        }


        public IGamestate Current
        {
            get { throw new NotImplementedException(); }
        }
    }
}
