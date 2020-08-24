using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace MyspriteGame_test3
{
    class BigAutoSprite:spriteBase
    {
        SoundEffect sound;
        bool direction = false;


         public BigAutoSprite(Texture2D texture, Point currentFrame, Point frameSize, Point sheetSize, Vector2 speed, Vector2 position,int collisionOffset,SoundEffect sound, int intervalTime)
            : base(texture, currentFrame, frameSize, sheetSize, speed, position, collisionOffset, intervalTime) { this.sound = sound;  }
         public BigAutoSprite(Texture2D texture, Point currentFrame, Point frameSize, Point sheetSize, Vector2 speed, Vector2 position, int collisionOffset, SoundEffect sound)
            : base(texture, currentFrame, frameSize, sheetSize, speed, position, collisionOffset) { this.sound = sound;  }

         public override void Update(GameTime gameTime, Rectangle clientBounds)
         {
             //base.Update(gameTime, clientBounds);
             elapsedTime += gameTime.ElapsedGameTime.Milliseconds;
             position += Speed;

             if (Speed.X < 0)
                 direction = false;
             if (Speed.X > 0)
                 direction = true;

             if (position.X > (clientBounds.Width) - frameSize.X * 2.5)
             {
                 currentFrame.X = currentFrame.Y = 0;
             }
             if (position.X < 0)
             {
                 currentFrame.X = 0;
                 currentFrame.Y = 4;
             }
             
                 if (position.X < 0 || position.X > (clientBounds.Width) -frameSize.X*2.5)
                     speed.X *= -1;
                 if (position.Y < 0 || position.Y > (clientBounds.Height)- frameSize.Y*2.5)
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
         public SoundEffect Sound
         {
             get { return sound; }
         }
         public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
         {
             spriteBatch.Draw(texture, position, new Rectangle((int)(currentFrame.X - 1) * frameSize.X, (int)currentFrame.Y * frameSize.Y,
                 frameSize.X, frameSize.Y), Color.White, 0, Vector2.Zero, 2.5f, SpriteEffects.None, 0);
         }
         public new Rectangle CollisionBoudns
         {
             get
             {
                 if (direction == false)
                     return new Rectangle((int)position.X, (int)(position.Y) + (int)(frameSize.Y*0.625), (int)(frameSize.X *2.5/ 3), (int)(frameSize.Y *1.25));
                  else
                     return new Rectangle((int)(position.X + frameSize.X * 2.5 * 0.67), (int)(position.Y) + (int)(frameSize.Y * 0.625), (int)(frameSize.X * 2.5 / 3), (int)(frameSize.Y * 1.25));
             }
         }

    }

}
