﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace Insanity.GameStates
{
    public class MainMenu : MenuBase
    {
        public MainMenu()
            : base()
        {
            TitleText = "Main Menu";
            TitlePosition = new Vector2(400, 100);
        }

        public void PlayFunc()
        {
            InsanityGame.GamestateManager.Pop(); 
            InsanityGame.GamestateManager.Push(new Level("level0"));
        }

        public void QuitFunc()
        {
            InsanityGame.PendingQuit = true;
        }

        public override List<Button> CreateButtons()
        {
            var menuButtons = new List<Button>();

            var playButton = new Button("Button", "Play Game", new Rectangle(300, 200, 120, 60), PlayFunc);
            var quitButton = new Button("Button", "Quit", new Rectangle(300, 300, 120, 60), QuitFunc);

            menuButtons.Add(playButton);
            menuButtons.Add(quitButton);

            return menuButtons;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public override void Initialize(ContentManager Content, GraphicsDeviceManager graphics)
        {
            base.Initialize(Content, graphics);
        }

        public override void LoadContent()
        {
            base.LoadContent();
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }
    }
}
