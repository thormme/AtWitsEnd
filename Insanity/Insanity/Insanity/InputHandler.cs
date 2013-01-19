﻿using System;
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
    }
}
