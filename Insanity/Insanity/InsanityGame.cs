using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.IO;
using Insanity.GameStates;

namespace Insanity
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class InsanityGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static bool PendingQuit; //terrible, but it works

        public static IGamestateManager GamestateManager;
        public static InputHandler Input;
        public static Dictionary<string, Texture2D> GameTextures;

        public static Dictionary<string, SpriteFont> GameFonts;

        public static int ScreenHeight;
        public static int ScreenWidth;
        private LoadingScreen LoadingScreen;

        public SoundEffect soundEffect;
        private SoundEffectInstance Instance;

        public InsanityGame()
        {
            PendingQuit = false;
            graphics = new GraphicsDeviceManager(this);

#if(!DEBUG)
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            graphics.IsFullScreen = true;
#endif

            ScreenHeight = graphics.PreferredBackBufferHeight;
            ScreenWidth = graphics.PreferredBackBufferWidth;

            Content.RootDirectory = "Content";
            GamestateManager = new GamestateManager(Content, graphics);
            Input = new InputHandler();
            GameTextures = new Dictionary<string, Texture2D>();
            GameFonts = new Dictionary<string, SpriteFont>();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            GameTextures["Button"] = Content.Load<Texture2D>("Button");

            soundEffect = Content.Load<SoundEffect>("background");

            System.IO.DirectoryInfo fontsDirectory = new System.IO.DirectoryInfo("Content/fonts");
            IEnumerable<FileInfo> fileList = fontsDirectory.GetFiles("*.xnb*", System.IO.SearchOption.AllDirectories);

            foreach (FileInfo fileInfo in fileList)
            {
                string key = fontsDirectory.Name + "/" + Path.GetFileNameWithoutExtension(fileInfo.Name);
                GameFonts[key] = Content.Load<SpriteFont>(key);
            }

            System.IO.DirectoryInfo levelDirectory = new System.IO.DirectoryInfo("Content/levels");
            fileList = levelDirectory.GetFiles("*.xnb*", System.IO.SearchOption.AllDirectories);

            foreach (FileInfo fileInfo in fileList)
            {
                string key = levelDirectory.Name + "/" + Path.GetFileNameWithoutExtension(fileInfo.Name);
                GameTextures[key] = Content.Load<Texture2D>(key);
            }

            System.IO.DirectoryInfo tileDirectory = new System.IO.DirectoryInfo("Content/tiles");
            fileList = tileDirectory.GetFiles("*.xnb*", System.IO.SearchOption.AllDirectories);

            foreach (FileInfo fileInfo in fileList)
            {
                string key = tileDirectory.Name + "/" + Path.GetFileNameWithoutExtension(fileInfo.Name);
                GameTextures[key] = Content.Load<Texture2D>(key);
            }

            System.IO.DirectoryInfo spriteDirectory = new System.IO.DirectoryInfo("Content/spriteSheets");
            fileList = spriteDirectory.GetFiles("*.xnb*", System.IO.SearchOption.AllDirectories);

            foreach (FileInfo fileInfo in fileList)
            {
                string key = spriteDirectory.Name + "/" + Path.GetFileNameWithoutExtension(fileInfo.Name);
                GameTextures[key] = Content.Load<Texture2D>(key);
            }

            System.IO.DirectoryInfo hudDirectory = new System.IO.DirectoryInfo("Content/hudElements");
            fileList = hudDirectory.GetFiles("*.xnb*", System.IO.SearchOption.AllDirectories);

            foreach (FileInfo fileInfo in fileList)
            {
                string key = hudDirectory.Name + "/" + Path.GetFileNameWithoutExtension(fileInfo.Name);
                GameTextures[key] = Content.Load<Texture2D>(key);
            }

            LoadingScreen = new LoadingScreen();
            LoadingScreen.Initialize(Content, graphics);
            LoadingScreen.LoadContent();
            
            GamestateManager.Push(new MainMenu());

            Instance = soundEffect.CreateInstance();
            Instance.IsLooped = true;
            Instance.Play();

            //GamestateManager.Push(new Level("level3"));
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            Input.Update(gameTime);
            if (/*Input.Quit() || */PendingQuit == true)
                this.Exit();

            // TODO: Add your update logic here
            if (GamestateManager.Current.Loaded)
                GamestateManager.Current.Update(gameTime);
            else
                LoadingScreen.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            if (GamestateManager.Current.Loaded)
                GamestateManager.Current.Draw(gameTime);
            else
                LoadingScreen.Draw(gameTime);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
