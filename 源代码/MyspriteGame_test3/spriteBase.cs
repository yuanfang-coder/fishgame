using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;




namespace MyspriteGame_test3
{
     abstract class spriteBase
    {
        
        protected Texture2D texture;
        protected Point currentFrame;
        protected Point frameSize;
        protected Point sheetSize;

        protected Vector2 speed;
        public Vector2 position;
         
        protected int intervalTime;
        protected int elapsedTime=95;
        protected int direction_elapsedTime = 5;
        protected int direction_defaultIntervalTime = 2500;
        const int defaultIntervalTime = 60;
        int collisionOffset;


        public spriteBase(Texture2D texture, Point currentFrame, Point frameSize, Point sheetSize, Vector2 speed, Vector2 position,int collisionOffset, int intervalTime)
        {
            this.texture = texture;
            this.currentFrame = currentFrame;
            this.frameSize = frameSize;
            this.sheetSize = sheetSize;
            this.speed = speed;
            this.position = position;
            this.collisionOffset = collisionOffset;
            this.intervalTime = intervalTime;
        }
        public spriteBase(Texture2D texture, Point currentFrame, Point frameSize, Point sheetSize, Vector2 speed, Vector2 position, int collisionOffset) :
            this(texture, currentFrame, frameSize, sheetSize, speed, position, collisionOffset ,defaultIntervalTime) { }
        public virtual void Update(GameTime gameTime, Rectangle clientBounds)
        {
         
         }
        
        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, new Rectangle((int)(currentFrame.X - 1) * frameSize.X, (int)currentFrame.Y * frameSize.Y, frameSize.X, frameSize.Y), Color.White, 0, Vector2.Zero,1f, SpriteEffects.None, 0);
        }
        protected abstract Vector2 Speed { get; }
         
        public Rectangle CollisionBoudns
        {
            get
            {
                return new Rectangle((int)position.X+collisionOffset,(int) position.Y+collisionOffset, frameSize.X-2*collisionOffset, frameSize.Y-2*collisionOffset);
                
            }
        }
        public Vector2 getPosition
        {
            get { return position; }
        }
    }
}
