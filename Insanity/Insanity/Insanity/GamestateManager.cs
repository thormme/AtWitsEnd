using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System.Threading;

namespace Insanity
{
    public class GamestateManager : IGamestateManager
    {
        ContentManager Content;
        GraphicsDeviceManager Graphics;

        Stack<IGamestate> GamestateStack;

        public GamestateManager(ContentManager content, GraphicsDeviceManager graphics)
        {
            Content = content;
            Graphics = graphics;
            GamestateStack = new Stack<IGamestate>();
        }

        public void Push(IGamestate state)
        {
            GamestateStack.Push(state);
            //TODO: add threading for dynamic load?

            Thread oThread = new Thread(new ThreadStart(() =>
            {
                state.Initialize(Content, Graphics);
                state.LoadContent();
            }));

            oThread.Start();
        }

        public IGamestate Pop()
        {
            return GamestateStack.Pop();
        }


        public IGamestate Current
        {
            get { return GamestateStack.Peek(); }
        }
    }
}
