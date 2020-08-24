using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace MyspriteGame_test3
{
    class ChasingSprite:spriteBase
    {
        SoundEffect sound;
        bool direction = false;
        public enum re_direction {RU,RD,LU,LD};
        public re_direction Now_direction=re_direction.RD;
        public re_direction last=re_direction.RD;
        GameComponent1 spriteManager;
        int collisionOffset = 5;

        



         public ChasingSprite(Texture2D texture, Point currentFrame, Point frameSize, Point sheetSize, Vector2 speed, Vector2 position,int collisionOffset,SoundEffect sound,GameComponent1 spriteManager, int intervalTime)
            : base(texture, currentFrame, frameSize, sheetSize, speed, position, collisionOffset, intervalTime) { this.sound = sound; this.spriteManager = spriteManager; }
         public ChasingSprite(Texture2D texture, Point currentFrame, Point frameSize, Point sheetSize, Vector2 speed, Vector2 position, int collisionOffset, SoundEffect sound, GameComponent1 spriteManager)
            : base(texture, currentFrame, frameSize, sheetSize, speed, position, collisionOffset) { this.sound = sound; this.spriteManager=spriteManager; }

         public override void Update(GameTime gameTime, Rectangle clientBounds)
         {
             //base.Update(gameTime, clientBounds);
             elapsedTime += gameTime.ElapsedGameTime.Milliseconds;



             position += Speed;

             if (Speed.X < 0)
                 direction = false;
             if (Speed.X > 0)
                 direction = true;

             if (position.X > (clientBounds.Width) )
             {
                 currentFrame.X = currentFrame.Y = 0;
             }
             if (position.X < 0-frameSize.X*2.5)
             {
                 currentFrame.X = 0;
                 currentFrame.Y = 4;
             }

             if (position.X < 0 - frameSize.X * 2.5 || position.X > (clientBounds.Width) )
             {
                 speed.X *= -1;
                 if (last == re_direction.LU)
                     Now_direction = re_direction.RU;
                 if (last == re_direction.LD)
                     Now_direction = re_direction.RD;
                 if (last == re_direction.RU)
                     Now_direction = re_direction.LU;
                 if (last == re_direction.RD)
                     Now_direction = re_direction.LD;
             }
             if (position.Y < 0-frameSize.Y*2.5 || position.Y > (clientBounds.Height) )
             {
                 speed.Y *= -1;
                 if (last == re_direction.LU)
                     Now_direction = re_direction.LD;
                 if (last == re_direction.LD)
                     Now_direction = re_direction.LU;
                 if (last == re_direction.RU)
                     Now_direction = re_direction.RD;
                 if (last == re_direction.RD)
                     Now_direction = re_direction.RU;
             }

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
             get
             {
                 
                 if (ChasingRange.Intersects(spriteManager.getplayerRange()))
                 {
                     if (spriteManager.test <= 3)
                     {
                         if (position.X > spriteManager.getplayerPosition().X && position.Y > spriteManager.getplayerPosition().Y)
                         {

                             Now_direction = re_direction.RD;
                             speed.X = speed.Y = -2;
                         }
                         if (position.X > spriteManager.getplayerPosition().X && position.Y < spriteManager.getplayerPosition().Y)
                         {
                             Now_direction = re_direction.RU;
                             speed.X = -2;
                             speed.Y = 2;
                         }
                         if (position.X < spriteManager.getplayerPosition().X && position.Y > spriteManager.getplayerPosition().Y)
                         {
                             Now_direction = re_direction.LD;
                             speed.X = 2;
                             speed.Y = -2;
                         }
                         if (position.X < spriteManager.getplayerPosition().X && position.Y < spriteManager.getplayerPosition().Y)
                         {
                             Now_direction = re_direction.LU;
                             speed.X = speed.Y = 2;
                         }
                     }
                     if (spriteManager.test > 3 && (position.X > 0-frameSize.X*2.5 && position.X <= (spriteManager.Game.Window.ClientBounds.Width)&&(position.Y > -frameSize.Y*2.5) && position.Y <(spriteManager.Game.Window.ClientBounds.Height)))
                     {
                         if (position.X > spriteManager.getplayerPosition().X && position.Y > spriteManager.getplayerPosition().Y)
                         {

                             Now_direction = re_direction.LU;
                             speed.X = speed.Y = 2;
                         }
                         if (position.X > spriteManager.getplayerPosition().X && position.Y < spriteManager.getplayerPosition().Y)
                         {
                             Now_direction = re_direction.LD;
                             speed.X = 2;
                             speed.Y = -2;
                         }
                         if (position.X < spriteManager.getplayerPosition().X && position.Y > spriteManager.getplayerPosition().Y)
                         {
                             Now_direction = re_direction.RU;
                             speed.X = -2;
                             speed.Y = 2;
                         }
                         if (position.X < spriteManager.getplayerPosition().X && position.Y < spriteManager.getplayerPosition().Y)
                         {
                             Now_direction = re_direction.RD;
                             speed.X = speed.Y = -2;
                         }
                     }
                     //等待化简
                     if (last == re_direction.RU && Now_direction == re_direction.LU)
                     {
                         currentFrame.X = 0; currentFrame.Y = 4;
                     }
                     if (last == re_direction.LU && Now_direction == re_direction.RU)
                     {
                         currentFrame.X = 0; currentFrame.Y = 0;
                     }
                     if (last == re_direction.RD && Now_direction == re_direction.LD)
                     {
                         currentFrame.X = 0; currentFrame.Y = 4;
                     }
                     if (last == re_direction.LD && Now_direction == re_direction.RD)
                     {
                         currentFrame.X = 0; currentFrame.Y = 0;
                     }
                     if (last == re_direction.LD && Now_direction == re_direction.RU)
                     {
                         currentFrame.X = 0; currentFrame.Y = 0;
                     }
                     if (last == re_direction.RU && Now_direction == re_direction.LD)
                     {
                         currentFrame.X = 0; currentFrame.Y = 4;
                     }
                     if (last == re_direction.LU && Now_direction == re_direction.RD)
                     {
                         currentFrame.X = 0; currentFrame.Y = 0;
                     }
                     if (last == re_direction.RD && Now_direction == re_direction.LU)
                     {
                         currentFrame.X = 0; currentFrame.Y = 4;
                     }
                 }

                 last = Now_direction;
                 return speed; 
             }
         }

         public Rectangle ChasingRange
         {
             get
             {
                 return new Rectangle((int)(position.X - 2.5 * frameSize.X), (int)(position.Y - 2.5 * frameSize.Y), (int)(7.5 * frameSize.X), (int)(7.5 * frameSize.Y));
 
             }
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
                     return new Rectangle((int)position.X, (int)(position.Y) + (int)(frameSize.Y * 0.625), (int)(frameSize.X * 2.5 / 3), (int)(frameSize.Y * 1.25));
                 else
                     return new Rectangle((int)(position.X + frameSize.X * 2.5 * 0.67), (int)(position.Y) + (int)(frameSize.Y * 0.625), (int)(frameSize.X * 2.5 / 3), (int)(frameSize.Y * 1.25));
             }
         }
         public Rectangle Base_CollisionBoudns
         {
             get
             {
                 return new Rectangle((int)position.X +  collisionOffset, (int)(position.Y + collisionOffset), (int) (2.5*frameSize.X - 2 * collisionOffset), (int) (2.5*frameSize.Y - 2 * collisionOffset));
             }
         }

    }
}
