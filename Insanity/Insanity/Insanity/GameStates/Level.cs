using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace Insanity.GameStates
{
    public class Level : IGamestate
    {
        public int InsanityLevel;
        public Camera Camera;

        int mNumTilesHorizontal;
        int mNumTilesVertical;

        // Access Tiles[insanity][tileIndex]
        public List<List<Tile>> Tiles;
        public List<Actor> Actors;

        GraphicsDeviceManager mGraphics;
        SpriteBatch mSpriteBatch;

        private Tile CreateTileFromColor(Color color, int x, int y, int index)
        {
            bool solid = true;
            Sprite sprite;

            if (color.PackedValue == Color.White.PackedValue)
            {
                sprite = new Sprite("tiles/ground");
            }
            else //if (color.PackedValue == Color.Black.PackedValue)
            {
                solid = false;
                sprite = new Sprite("tiles/empty");
            }

            return new Tile(solid, sprite, x, y, index);
        }

        public Level(string levelName)
        {
            Camera = new Camera(new Vector2());
            Loaded = false;
            Actors = new List<Actor>();

            using (var reader = new StreamReader("Content/levels/" + levelName + ".actors"))
            {
                while (!reader.EndOfStream)
                {
                    string input = reader.ReadLine();
                    string[] args = input.Split(' ');
                    Type actorType = Type.GetType(args[0]);

                    Actor actor = (Actor) Activator.CreateInstance(actorType, args.Skip(1).ToList());
                    actor.OwnerLevel = this;
                    Actors.Add(actor);
                }
            }

            Tiles = new List<List<Tile>>();

            for (int i = 0; i < 3; i++)
            {
                Texture2D levelData = InsanityGame.GameTextures["levels/" + levelName + "_" + i];

                if (i == 0)
                {
                    mNumTilesHorizontal = levelData.Width;
                    mNumTilesVertical = levelData.Height;
                }

                Color[] retrievedColors = new Color[levelData.Width * levelData.Height];

                levelData.GetData<Color>(
                    0,
                    null,
                    retrievedColors,
                    0,
                    retrievedColors.Length);

                List<Tile> tiles = new List<Tile>();
                int index = 0;
                foreach (Color color in retrievedColors)
                {
                    tiles.Add(CreateTileFromColor(color, Tile.Width * (index % mNumTilesHorizontal), Tile.Height * (int)(index / mNumTilesHorizontal), index));
                    index++;
                }
                Tiles.Add(tiles);
            }
        }

        public List<Tile> GetCollidingTiles(Rectangle bounds, bool inlcudeNonSolid)
        {
            List<Tile> tiles = new List<Tile>();

            int startTileIndex = (int)(bounds.X / Tile.Width) + (int)(mNumTilesHorizontal * (bounds.Y / Tile.Height));
            int numVerticalTiles = (int)((bounds.Y + bounds.Height) / Tile.Height) - (int)(bounds.Y / Tile.Height) + 1;
            int numHorizontalTiles = (int)((bounds.X + bounds.Width) / Tile.Width) - (int)(bounds.X / Tile.Width) + 1;
            for (int y = 0; y < numVerticalTiles; y++)
            {
                int curTileIndex = startTileIndex + mNumTilesHorizontal * y;
                for (int x = 0; x < numHorizontalTiles; x++)
                {
                    if (curTileIndex >= 0 && curTileIndex < Tiles[InsanityLevel].Count)
                    {
                        Tile curTile = Tiles[InsanityLevel][curTileIndex];
                        if (inlcudeNonSolid || curTile.Solid)
                        {
                            tiles.Add(curTile);
                        }
                    }
                    curTileIndex++;
                }
            }
            return tiles;
        }

        public void Update(GameTime gameTime)
        {
            foreach (Actor actor in Actors)
            {
                actor.Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime)
        {
            mSpriteBatch.Begin();
            
            int startTileIndex = (int)(Camera.Position.X / Tile.Width) + (int)(mNumTilesHorizontal * (Camera.Position.Y / Tile.Height));
            for (int y = 0; y < mGraphics.PreferredBackBufferHeight / Tile.Height + 1; y++)
            {
                int curTileIndex = startTileIndex + mNumTilesHorizontal * y;
                for (int x = 0; x < mGraphics.PreferredBackBufferWidth / Tile.Width + 1; x++)
                {
                    if (curTileIndex >= 0)
                    {
                        Tiles[InsanityLevel][curTileIndex].Draw(Camera, mSpriteBatch, gameTime);
                    }
                    curTileIndex++;
                }
            }
            foreach (Actor actor in Actors)
            {
                actor.Draw(Camera, mSpriteBatch, gameTime);
            }

            mSpriteBatch.End();
        }

        public void Initialize(ContentManager Content, GraphicsDeviceManager graphics)
        {
            mGraphics = graphics;
            mSpriteBatch = new SpriteBatch(mGraphics.GraphicsDevice);
        }

        public void LoadContent()
        {
            Loaded = true;
        }

        public void UnloadContent()
        {
            
        }

        public bool Loaded
        {
            get;
            protected set;
        }
    }
}
