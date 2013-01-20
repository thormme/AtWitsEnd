using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Insanity.Actors;
using Insanity.GameStates;

namespace Insanity
{
    public class InputHandler : IInputAgent
    {
        KeyboardState CurrentKeyboardState;
        KeyboardState OldKeyboardState;

        public InputHandler()
        {
        }

        public bool Quit()
        {
            return CurrentKeyboardState.IsKeyDown(Keys.Escape);
        }

        public void Update(GameTime gameTime, Actor agent = null)
        {
            OldKeyboardState = CurrentKeyboardState;
            CurrentKeyboardState = Keyboard.GetState();
        }

        public bool MoveRight()
        {
            return CurrentKeyboardState.IsKeyDown(Keys.D) || CurrentKeyboardState.IsKeyDown(Keys.Right);
        }

        public bool MoveLeft()
        {
            return CurrentKeyboardState.IsKeyDown(Keys.A) || CurrentKeyboardState.IsKeyDown(Keys.Left);
        }

        public bool Jump()
        {
            return (CurrentKeyboardState.IsKeyDown(Keys.W) && !OldKeyboardState.IsKeyDown(Keys.W)) ||
                   (CurrentKeyboardState.IsKeyDown(Keys.Up) && !OldKeyboardState.IsKeyDown(Keys.Up));
        }

        public bool NewUpPress()
        {
            return (CurrentKeyboardState.IsKeyDown(Keys.W) && !OldKeyboardState.IsKeyDown(Keys.W)) ||
                   (CurrentKeyboardState.IsKeyDown(Keys.Up) && !OldKeyboardState.IsKeyDown(Keys.Up));
        }

        public bool NewDownPress()
        {
            return (CurrentKeyboardState.IsKeyDown(Keys.S) && !OldKeyboardState.IsKeyDown(Keys.S)) ||
                   (CurrentKeyboardState.IsKeyDown(Keys.Down) && !OldKeyboardState.IsKeyDown(Keys.Down));
        }

        public bool NewEnterPress()
        {
            return CurrentKeyboardState.IsKeyDown(Keys.Enter) && !OldKeyboardState.IsKeyDown(Keys.Enter);
        }

        public bool Pause()
        {
            return CurrentKeyboardState.IsKeyDown(Keys.P) && !OldKeyboardState.IsKeyDown(Keys.P);
        }

        public bool TakePill()
        {
            return CurrentKeyboardState.IsKeyDown(Keys.Space) && !OldKeyboardState.IsKeyDown(Keys.Space);
        }

        public bool ViewPhoto()
        {
            return CurrentKeyboardState.IsKeyDown(Keys.LeftControl) && !OldKeyboardState.IsKeyDown(Keys.LeftControl);
        }

        protected Level LevelRef;
        public void GiveLevel(Level level)
        {
            LevelRef = level;
        }
    }
}
