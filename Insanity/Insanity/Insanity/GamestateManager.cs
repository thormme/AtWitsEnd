using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

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
            //TODO: add threading for dynamic load?
            state.Initialize(Content, Graphics);
            state.LoadContent();
            GamestateStack.Push(state);

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
