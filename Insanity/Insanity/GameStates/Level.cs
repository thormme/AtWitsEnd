﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Insanity.Actors;
using System.Threading;

namespace Insanity.GameStates
{
    public class Level : IGamestate
    {
        public const int NumInsanityLevels = 3;
        public int InsanityLevel;
        public Camera Camera;

        int mNumTilesHorizontal;
        int mNumTilesVertical;

        public int ScreenWidth;
        public int ScreenHeight;

        // Access Tiles[insanity][tileIndex]
        public List<List<Tile>> Tiles;
        public List<Actor> Actors;

        public Player mPlayer { get; protected set; }

        public Goal mGoal { get; protected set; }

        GraphicsDeviceManager mGraphics;
        SpriteBatch mSpriteBatch;

        List<Actor> mActorsToRemove = new List<Actor>();
        List<Actor> mActorsToAdd = new List<Actor>();

        private Tile CreateTileFromColor(Color color, int x, int y, string assetName)
        {
            Sprite sprite = new Sprite(assetName);

            int blockRow    = (int)color.R;
            int blockColumn = (int)color.G;
            bool solid      = (int)color.B < 255;

            return new Tile(solid, sprite, x, y, blockRow, blockColumn);
        }

        string LevelName;

        public Level(string levelName)
        {
            LevelName = levelName;
            Loaded = false;
        }

        private void YouWin()
        {
            InsanityGame.GamestateManager.Pop();
            InsanityGame.GamestateManager.Push(new VictoryState());
        }

        private void Initialize(string levelName)
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

                    Actor actor = (Actor)Activator.CreateInstance(actorType, args.Skip(1).ToList());
                    actor.OwnerLevel = this;
                    Actors.Add(actor);
                }
            }

            mPlayer = Actors.First((actor) => { return (actor as Player) != null; }) as Player;

            mGoal = Actors.First((actor) => { return actor is Goal; }) as Goal;

            Tiles = new List<List<Tile>>();

            string[] tileSpriteNames = new string[]
            {
                "tiles/hospital sane spritesheet",
                "tiles/hospital midsane spritesheet",
                "tiles/hospital insane spritesheet"
            };

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
                    tiles.Add(CreateTileFromColor(color, Tile.Width * (index % mNumTilesHorizontal), Tile.Height * (int)(index / mNumTilesHorizontal), tileSpriteNames[i]));
                    index++;
                }
                Tiles.Add(tiles);
            }
            Loaded = true;
        }

        // position in screen coords
        public int GetTileIndex(Vector2 position)
        {
            return (int)(position.X / Tile.Width) + (int)(mNumTilesHorizontal * (position.Y / Tile.Height));
        }

        // position in screen coords
        public Tile GetTile(Vector2 position, int insanityLevel = -1)
        {
            if (insanityLevel < 0)
            {
                insanityLevel = InsanityLevel;
            }

            return Tiles[insanityLevel][(int)(position.X / Tile.Width) + (int)(mNumTilesHorizontal * (position.Y / Tile.Height))];
        }

        // position in screen coords
        public Tile GetTile(int x, int y, int insanityLevel = -1)
        {
            if (insanityLevel < 0)
            {
                insanityLevel = InsanityLevel;
            }

            return Tiles[insanityLevel][x + (int)(mNumTilesHorizontal * y)];
        }

        public bool IsWithinLevel(Vector2 position)
        {
            return new Rectangle(0, 0, mNumTilesHorizontal * Tile.Width, mNumTilesVertical * Tile.Height).Contains((int)position.X, (int)position.Y);
        }

        // tile coordinates
        public bool IsWithinLevel(int x, int y)
        {
            return !(x < 0 || x >= mNumTilesHorizontal || y < 0 || y >= mNumTilesVertical);
        }

        public List<Tile> GetCollidingTiles(Rectangle bounds, bool inlcudeNonSolid = false, int insanityLevel = -1)
        {
            if (insanityLevel < 0)
            {
                insanityLevel = InsanityLevel;
            }

            List<Tile> tiles = new List<Tile>();

            int startTileIndex = (int)(bounds.X / Tile.Width) + (int)(mNumTilesHorizontal * (bounds.Y / Tile.Height));
            int numVerticalTiles = (int)((bounds.Y + bounds.Height) / Tile.Height) - (int)(bounds.Y / Tile.Height) + 1;
            int numHorizontalTiles = (int)((bounds.X + bounds.Width) / Tile.Width) - (int)(bounds.X / Tile.Width) + 1;
            for (int y = 0; y < numVerticalTiles; y++)
            {
                int curTileIndex = startTileIndex + mNumTilesHorizontal * y;
                for (int x = 0; x < numHorizontalTiles; x++)
                {
                    if (curTileIndex >= 0 && curTileIndex < Tiles[insanityLevel].Count)
                    {
                        Tile curTile = Tiles[insanityLevel][curTileIndex];
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

        public void AddActor(Actor actor)
        {
            mActorsToAdd.Add(actor);
        }

        public void RemoveActor(Actor actor)
        {
            mActorsToRemove.Add(actor);
        }

        private void CommitActorChanges()
        {
            foreach (Actor actor in mActorsToAdd)
            {
                Actors.Add(actor);
                actor.OwnerLevel = this;
            }
            foreach (Actor actor in mActorsToRemove)
            {
                Actors.Remove(actor);
                actor.OwnerLevel = null;
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (Actor actor in Actors)
            {
                actor.Update(gameTime, mPlayer.InsanityLevel);
            }
            CommitActorChanges();

            if (Camera.Position.X < 0)
            {
                Camera.Position.X = 0;
            }
            if (Camera.Position.X > mNumTilesHorizontal * Tile.Width - ScreenWidth)
            {
                Camera.Position.X = mNumTilesHorizontal * Tile.Width - ScreenWidth;
            }
            if (Camera.Position.Y < 0)
            {
                Camera.Position.Y = 0;
            }
            if (Camera.Position.Y > mNumTilesVertical * Tile.Height - ScreenHeight)
            {
                Camera.Position.Y = mNumTilesVertical * Tile.Height - ScreenHeight;
            }

            if (mPlayer.IsFinished)
            {
                var levelName = mGoal.NextLevel;
                if (levelName.Equals("Victory"))
                {
                    YouWin();
                    return;
                }
                InsanityGame.GamestateManager.Pop();
                InsanityGame.GamestateManager.Push(new Level(levelName));
            }
            else if (mPlayer.IsDead)
            {
                InsanityGame.GamestateManager.Push(new DeadState());
                Loaded = false;

                Thread oThread = new Thread(new ThreadStart(() =>
                {
                    Initialize(LevelName);
                }));

                oThread.Start();
            }
        }

        public void Draw(GameTime gameTime)
        {
            mSpriteBatch.Begin();

            int startTileIndex = (int)(Camera.Position.X / Tile.Width) + (int)(mNumTilesHorizontal * (int)(Camera.Position.Y / Tile.Height));
            int numVerticalTiles = (int)((Camera.Position.Y + ScreenHeight) / Tile.Height) - (int)(Camera.Position.Y / Tile.Height) + 1;
            int numHorizontalTiles = (int)((Camera.Position.X + ScreenWidth) / Tile.Width) - (int)(Camera.Position.X / Tile.Width) + 1;
            for (int y = 0; y < numVerticalTiles; y++)
            {
                int curTileIndex = startTileIndex + mNumTilesHorizontal * y;
                for (int x = 0; x < numHorizontalTiles; x++)
                {
                    if (curTileIndex >= 0 && curTileIndex < Tiles[InsanityLevel].Count)
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

            mPlayer.DrawHud(gameTime, mSpriteBatch);

            mSpriteBatch.End();
        }

        public void Initialize(ContentManager Content, GraphicsDeviceManager graphics)
        {
            mGraphics = graphics;
            mSpriteBatch = new SpriteBatch(mGraphics.GraphicsDevice);

            ScreenWidth = mGraphics.PreferredBackBufferWidth;
            ScreenHeight = mGraphics.PreferredBackBufferHeight;

            Initialize(LevelName);
        }

        public void LoadContent()
        {
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
