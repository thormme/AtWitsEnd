using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Insanity
{
    public class Level : IGamestate
    {
        public int InsanityLevel;

        // Access Tiles[insanity][tileIndex]
        public List<List<Tile>> Tiles;
        public List<Actor> Actors;

        GraphicsDeviceManager mGraphics;
        SpriteBatch mSpriteBatch;

        public void Update(GameTime gameTime)
        {
            foreach (Actor actor in Actors)
            {
                actor.Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime)
        {
            foreach (Tile tile in Tiles[InsanityLevel])
            {
                tile.Draw(mSpriteBatch, gameTime);
            }
        }

        public void Initialize(ContentManager Content, GraphicsDeviceManager graphics)
        {
            mGraphics = graphics;
            mSpriteBatch = new SpriteBatch(mGraphics.GraphicsDevice);
        }

        public void LoadContent()
        {
            throw new NotImplementedException();
        }

        public void UnloadContent()
        {
            throw new NotImplementedException();
        }
    }
}
