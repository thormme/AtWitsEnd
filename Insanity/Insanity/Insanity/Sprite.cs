using System;
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
            public string Name;
            public string NextAnimation;
            public int length;
            public double animationSpeed;
            public int StartingRow;
            public int StartingColumn;
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
            currentAnimation = spriteInfo.Animations.First().Value;
            elapsedTime = 0;
            UpdateSource();
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

                reader.ReadLine();

                for(int i = 0; i < numAnimations; i++)
                {
                    var temp = new Animation();
                    string animationName;

                    input = reader.ReadLine();
                    animationName = input.Split(' ')[1];
                    temp.Name = animationName;

                    input = reader.ReadLine();
                    temp.NextAnimation = input.Split(' ')[1];
                    
                    input = reader.ReadLine();                    
                    temp.length = Convert.ToInt32(input.Split(' ')[1]);
                    
                    input = reader.ReadLine();
                    temp.animationSpeed = Convert.ToDouble(input.Split(' ')[1]);

                    input = reader.ReadLine();
                    temp.StartingRow = Convert.ToInt32(input.Split(' ')[1]);

                    input = reader.ReadLine();
                    temp.StartingColumn = Convert.ToInt32(input.Split(' ')[1]);

                    reader.ReadLine();

                    spriteInfo.Animations.Add(animationName, temp);
                }
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Rectangle position, bool flipped)
        {
            if (flipped)
            {
                //spriteBatch.Draw(texture, new Rectangle(position.X + position.Width, position.Y, -1 * position.Width, position.Height), source, Color.White);
                spriteBatch.Draw(texture, position, source, Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
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

                if (currentFrame >= currentAnimation.length)
                {
                    ChangeAnimation(currentAnimation.NextAnimation);
                }
                else
                {
                    UpdateSource();
                }
            }
        }

        private void UpdateSource()
        {
            int row = currentAnimation.StartingRow + currentFrame / spriteInfo.maxLength;
            int column = (currentAnimation.StartingColumn + currentFrame) % spriteInfo.maxLength;
            int width = spriteInfo.frameWidth * BlockWidth;
            int height = spriteInfo.frameHeight * BlockHeight;
            source = new Rectangle(column * width, row * height, width, height);
        }

        public bool ChangeAnimation(string animationName)
        {
            if (!spriteInfo.Animations.ContainsKey(animationName))
                return false;

            if (currentAnimation.Name == animationName)
                return true;

            currentFrame = 0;
            currentAnimation = spriteInfo.Animations[animationName];

            UpdateSource();

            return true;
        }

        public string GetAnimation()
        {
            return currentAnimation.Name;
        }
    }
}
