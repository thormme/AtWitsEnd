﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.IO;

namespace Insanity
{
    public class Sprite
    {
        static int BlockHeight = 80;
        static int BlockWidth = 80;

        private struct Animation
        {
            public int length;
            public double animationSpeed;
            public string NextAnimation;
            public int StartingRow;
        }

        private struct SpriteSheetInfo
        {
            public int frameWidth;
            public int frameHeight;
            public int maxLength;

            public Dictionary<string, Animation> Animations;
        }
        
        private Texture2D texture;
        private Rectangle? source;
        private SpriteSheetInfo spriteInfo;

        private int currentFrame;
        private Animation currentAnimation;

        //in ms
        private double elapsedTime;

        public Sprite(string assetName)
        {
            texture = InsanityGame.GameTextures[assetName];
            ReadInfo(assetName);
            elapsedTime = 0;
        }

        private void ReadInfo(string assetName)
        {
            spriteInfo = new SpriteSheetInfo();
            spriteInfo.Animations = new Dictionary<string,Animation>();
            string input;

            using (var reader = new StreamReader("Content/" + assetName + ".txt"))
            {
                input = reader.ReadLine();

                spriteInfo.frameWidth = Convert.ToInt32(input.Split(' ')[1]);

                input = reader.ReadLine();

                spriteInfo.frameHeight = Convert.ToInt32(input.Split(' ')[1]);

                input = reader.ReadLine();

                spriteInfo.maxLength = Convert.ToInt32(input.Split(' ')[1]);

                input = reader.ReadLine();

                var numAnimations = Convert.ToInt32(input.Split(' ')[1]);

                for(int i = 0; i < numAnimations; i++)
                {
                    var temp = new Animation();
                    string animationName;
                    
                    input = reader.ReadLine();                    
                    temp.length = Convert.ToInt32(input.Split(' ')[1]);
                    
                    input = reader.ReadLine();
                    temp.animationSpeed = Convert.ToDouble(input.Split(' ')[1]);

                    input = reader.ReadLine();
                    animationName = input.Split(' ')[1];

                    input = reader.ReadLine();
                    temp.NextAnimation = input.Split(' ')[1];

                    input = reader.ReadLine();
                    temp.StartingRow = Convert.ToInt32(input.Split(' ')[1]);

                    spriteInfo.Animations.Add(animationName, temp);
                }
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Rectangle position, bool flipped)
        {
            if (flipped)
            {
                spriteBatch.Draw(texture, new Rectangle(position.X + position.Width, position.Y, -1 * position.Width, position.Height), source, Color.White);
            }
            else
            {
                spriteBatch.Draw(texture, position, source, Color.White);
            }
        }

        public void Update(GameTime gameTime)
        {
            elapsedTime += gameTime.ElapsedGameTime.TotalMilliseconds;
            if(elapsedTime > currentAnimation.animationSpeed)
            {
                elapsedTime -= currentAnimation.animationSpeed;
                currentFrame++;

                if (currentFrame > currentAnimation.length)
                {
                    currentFrame = 0;
                    currentAnimation = spriteInfo.Animations[currentAnimation.NextAnimation];
                }
                UpdateSource();
            }
        }

        private void UpdateSource()
        {
            int row = currentAnimation.StartingRow + currentFrame / spriteInfo.maxLength;
            int column = currentFrame % spriteInfo.maxLength;
            source = new Rectangle(row, column, spriteInfo.frameWidth * BlockWidth, spriteInfo.frameHeight * BlockHeight);
        }

        public bool ChangeAnimation(string animationName)
        {
            if (!spriteInfo.Animations.ContainsKey(animationName))
                return false;
            currentFrame = 0;
            currentAnimation = spriteInfo.Animations[animationName];

            UpdateSource();

            return true;
        }
    }
}
