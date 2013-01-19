using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace Insanity
{
    public class InputHandler
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

        public void Update()
        {
            OldKeyboardState = CurrentKeyboardState;
            CurrentKeyboardState = Keyboard.GetState();
        }

        public bool MoveRight()
        {
            return CurrentKeyboardState.IsKeyDown(Keys.D);
        }

        public bool MoveLeft()
        {
            return CurrentKeyboardState.IsKeyDown(Keys.A);
        }

        public bool Jump()
        {
            return CurrentKeyboardState.IsKeyDown(Keys.W) && !OldKeyboardState.IsKeyDown(Keys.W);
        }

        public bool NewUpPress()
        {
            return CurrentKeyboardState.IsKeyDown(Keys.W) && !OldKeyboardState.IsKeyDown(Keys.W);
        }

        public bool NewDownPress()
        {
            return CurrentKeyboardState.IsKeyDown(Keys.S) && !OldKeyboardState.IsKeyDown(Keys.S);
        }

        internal bool NewEnterPress()
        {
            return CurrentKeyboardState.IsKeyDown(Keys.Enter) && !OldKeyboardState.IsKeyDown(Keys.Enter);
        }
    }
}
