using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;



namespace MyspriteGame_test3
{
    class AutoSprite:spriteBase
    {
        SoundEffect sound;
        int score;
        int extraRoom = 0;
        public AutoSprite(Texture2D texture, Point currentFrame, Point frameSize, Point sheetSize, Vector2 speed, Vector2 position,int collisionOffset,SoundEffect sound, int score,int intervalTime)
            : base(texture, currentFrame, frameSize, sheetSize, speed, position, collisionOffset, intervalTime) { this.sound = sound; this.score = score; }
        public AutoSprite(Texture2D texture, Point currentFrame, Point frameSize, Point sheetSize, Vector2 speed, Vector2 position, int collisionOffset, SoundEffect sound, int score)
            : base(texture, currentFrame, frameSize, sheetSize, speed, position, collisionOffset) { this.sound = sound; this.score = score; }
        public override void Update(GameTime gameTime, Rectangle clientBounds)
        {
            //base.Update(gameTime, clientBounds);
            elapsedTime += gameTime.ElapsedGameTime.Milliseconds;
            position += Speed ;
            if (position.X >= clientBounds.Width + extraRoom)
            {
                currentFrame.X = currentFrame.Y = 0;
            }
            if (position.X <= (-frameSize.X - extraRoom))
            {
                currentFrame.X = 0;
                currentFrame.Y = 4;
            }
            if (position.X < (-frameSize.X - extraRoom) || position.X > (clientBounds.Width) + extraRoom)
                speed.X *= -1;
            if (position.Y < (-frameSize.Y - extraRoom) || position.Y > (clientBounds.Height) + extraRoom)
                speed.Y *= -1;
            if (elapsedTime >= intervalTime)
            {
                elapsedTime -= intervalTime;
                currentFrame.X++;
                if (speed.X < 0)
                {

                    if (currentFrame.X == sheetSize.X && currentFrame.Y == 3)
                    {
                        currentFrame.X = 1;
                        currentFrame.Y = 0;
                    }
                    if (currentFrame.X > sheetSize.X)
                    {
                        currentFrame.X = 1;
                        currentFrame.Y++;
                    }
                }
                if (speed.X > 0)
                {
                    if (currentFrame.X == sheetSize.X && currentFrame.Y == 7)
                    {
                        currentFrame.X = 1;
                        currentFrame.Y = 4;
                    }
                    if (currentFrame.X > sheetSize.X)
                    {
                        currentFrame.X = 1;
                        currentFrame.Y++;
                    }
                    
                }
            }

        }
        protected override Vector2 Speed
        {
            get { return speed; }
        }
        public  SoundEffect Sound
        {
            get { return sound;  }
        }
        public int Score
        {
            get { return score; }
        }
    }
}
