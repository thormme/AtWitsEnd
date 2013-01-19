using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Insanity
{
    public interface IGamestate
    {
        void Update(GameTime gameTime);
        void Draw(GameTime gameTime);
        void Initialize(ContentManager Content, GraphicsDeviceManager graphics);
        void LoadContent();
        void UnloadContent();

        //make sure this is only true upon LoadContent completion
        //useful for probably unnecessary loading screen
        bool Loaded { get; }
    }
}
