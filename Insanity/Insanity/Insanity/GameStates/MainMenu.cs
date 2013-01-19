using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace Insanity
{
    public class MainMenu : IGamestate
    {
        private bool loaded;
        public MainMenu()
        {
            loaded = false;
        }

        public void Update(GameTime gameTime)
        {
            
        }

        public void Draw(GameTime gameTime)
        {
            
        }

        public void Initialize(ContentManager Content, GraphicsDeviceManager graphics)
        {
            
        }

        public void LoadContent()
        {
            loaded = true;
        }

        public void UnloadContent()
        {
            
        }


        public bool Loaded
        {
            get { return loaded; }
        }
    }
}
