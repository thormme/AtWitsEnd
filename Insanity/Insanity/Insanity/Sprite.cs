using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Insanity
{
    public class Sprite
    {
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

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Draw(texture, position, source, Color.White);
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
            source = new Rectangle();
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
