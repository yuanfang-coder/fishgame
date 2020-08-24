using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna .Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyspriteGame_test3
{  
    class UserControledSprite:spriteBase
    {
        public float level;
        int collisionOffset = 20;
         public bool direction;//0左1右
        //bool beforedirection;

        public enum re_direction { RU, RD, LU, LD };
        public re_direction Now_direction = re_direction.LU;
        public re_direction last = re_direction.LU;


        public UserControledSprite(Texture2D texture, Point currentFrame, Point frameSize, Point sheetSize, Vector2 speed, Vector2 position,int collisionOffset, bool direction,int level,int intervalTime)
            : base(texture, currentFrame, frameSize, sheetSize, speed, position, collisionOffset, intervalTime) { this.direction = direction; this.level = level; }
        public UserControledSprite(Texture2D texture, Point currentFrame, Point frameSize, Point sheetSize, Vector2 speed, Vector2 position, bool direction, int level,int collisionOffset)
            : base(texture, currentFrame, frameSize, sheetSize, speed, position, collisionOffset) { this.direction = direction; this.level = level; }

        public override void Update(GameTime gameTime, Rectangle clientBounds)
        {
            elapsedTime += gameTime.ElapsedGameTime.Milliseconds;
            if (elapsedTime >= intervalTime)
            {
                elapsedTime -= intervalTime;
                currentFrame.X++;
                if (direction == false)
                {
                    if (currentFrame.X == sheetSize.X && currentFrame.Y == 9)
                    {
                        currentFrame.X = 1;
                        currentFrame.Y = 6;
                    }
                    if (currentFrame.X > sheetSize.X)
                    {
                        currentFrame.X = 1;
                        currentFrame.Y++;
                    }
                }
                if (direction == true)
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
                           
            }

            MouseState mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                position.X = mouseState.X;
                position.Y = mouseState.Y;
            }
           /* if (position.X != mouseState.X || position.Y != mouseState.Y)
            {
                position.X = mouseState.X;
                position.Y = mouseState.Y;
            }*/
            if (position.X < 0) position.X = 0;
            if (position.Y < 0) position.Y = 0;
            if (position.X > (clientBounds.Width - (int)((1 + (level - 1) / 10) *frameSize.X))) position.X = clientBounds.Width - (int)((1 + (level - 1) / 10) *frameSize.X);
            if (position.Y > (clientBounds.Height - (int)((1 + (level - 1) / 10) *frameSize.Y))) position.Y = clientBounds.Height - (int)((1 + (level - 1) / 10) *frameSize.Y);


            //鼠标相对位置 
            if ((mouseState.X > position.X + (int)((1 + (level - 1) / 10) * (5 + (int)(frameSize.X / 2))) && mouseState.Y > position.Y + (int)((1 + (level - 1) / 10) * (3 + (int)(frameSize.X / 2)))) && (mouseState.Y > position.Y + (int)((1 + (level - 1) / 10) * (5 + (int)(frameSize.Y / 2))) && mouseState.X > position.X + (int)((1 + (level - 1) / 10) * (3 + (int)(frameSize.X / 2)))))
            {
                Now_direction = re_direction.RD;
                speed.X = speed.Y = 5;
                direction = true;
            }
            if ((mouseState.X > position.X + (int)(1 + (level - 1) / 10) * (5 + (int)(frameSize.X / 2)) && mouseState.Y < position.Y - (int)((1 + (level - 1) / 10) * (3 - (int)(frameSize.Y / 2)))) && (mouseState.Y < position.Y - (int)((1 + (level - 1) / 10) * (5 - (int)(frameSize.Y / 2)) )&& mouseState.X > position.X + (int)((1 + (level - 1) / 10) *( 3 + (int)(frameSize.X / 2)))))
            {
                Now_direction = re_direction.RU;
                speed.X = 5;
                speed.Y = -5; 
                direction = true;
            }
            if ((mouseState.X < position.X - (int)((1 + (level - 1) / 10) * (5 - (int)(frameSize.X / 2))) && mouseState.Y > position.Y + (int)((1 + (level - 1) / 10) * (3 + (int)(frameSize.Y / 2)))) && (mouseState.Y > position.Y + (int)((1 + (level - 1) / 10) * (5 + (int)(frameSize.Y / 2))) && mouseState.X < position.X - (int)((1 + (level - 1) / 10) *( 3 - (int)(frameSize.X / 2)))))
            {
                Now_direction = re_direction.LD;
                speed.X = -5;
                speed.Y = 5;
                direction = false;
            }
            if ((mouseState.X < position.X - (int)((1 + (level - 1) / 10) * (5 - (int)(frameSize.X / 2))) && mouseState.Y < position.Y - (int)((1 + (level - 1) / 10) * (3 - (int)(frameSize.Y / 2)))) && (mouseState.Y < position.Y + (int)((1 + (level - 1) / 10) * ((int)(frameSize.Y / 2) - 5) )&& mouseState.X < position.X - (int)((1 + (level - 1) / 10) * (3 - (int)(frameSize.X / 2)))))
            {
                Now_direction = re_direction.LU;
                speed.X = speed.Y = -5;
                direction = false;
            }
            if (mouseState.X >= position.X - (int)((1 + (level - 1) / 10) * (5 - (int)(frameSize.X / 2))) && mouseState.X <= position.X + (int)((1 + (level - 1) / 10) * (5 + (int)(frameSize.X / 2))) && mouseState.Y >= position.Y - (int)((1 + (level - 1) / 10) * (5 - (int)(frameSize.Y / 2))) && mouseState.Y <= position.Y + (int)((1 + (level - 1) / 10) *( 5 + (int)(frameSize.Y / 2))))
            {
                speed.X = speed.Y = 0;
                Now_direction = last;
            }
            if (mouseState.Y >= position.Y - (int)((1 + (level - 1) / 10) * (3- (int)(frameSize.Y / 2))) && mouseState.Y <= position.Y + (int)((1 + (level - 1) / 10) * (3 + (int)(frameSize.Y / 2))) && mouseState.X > position.X + (int)((1 + (level - 1) / 10) *( 5 + (int)(frameSize.X / 2))))
            {
                speed.X = 5; speed.Y = 0;
                Now_direction = last;
            }
            if (mouseState.Y >= position.Y - (int)((1 + (level - 1) / 10) * (3 - (int)(frameSize.Y / 2))) && mouseState.Y <= position.Y + (int)((1 + (level - 1) / 10) * (3 + (int)(frameSize.Y / 2))) && mouseState.X < position.X - (int)((1 + (level - 1) / 10) * (5 - (int)(frameSize.X / 2))))
            {
                speed.X = -5; speed.Y = 0;
                Now_direction = last;
            }
            if (mouseState.X >= position.X - (int)((1 + (level - 1) / 10) * (3 - (int)(frameSize.X / 2))) && mouseState.X <= position.X + (int)((1 + (level - 1) / 10) * (3 + (int)(frameSize.X / 2))) && mouseState.Y > position.Y + (int)((1 + (level - 1) / 10) * (5 + (int)(frameSize.Y / 2))))
            {
                speed.X = 0; speed.Y = 5;
                Now_direction = last;
            }
            if (mouseState.X >= position.X - (int)((1 + (level - 1) / 10) * (3 - (int)(frameSize.X / 2))) && mouseState.X <= position.X + (int)((1 + (level - 1) / 10) * (3 + (int)(frameSize.X / 2))) && mouseState.Y < position.Y - (int)((1 + (level - 1) / 10) * (5 - (int)(frameSize.Y / 2))))
            {
                speed.X = 0; speed.Y = -5;
                Now_direction = last;
            }

           

            //等待化简!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!1



            
            if (last == re_direction.RU && Now_direction == re_direction.LU)
            {
                currentFrame.X = 0; currentFrame.Y = 6;
            }

            if (last == re_direction.RU && Now_direction == re_direction.LD)
            {
                currentFrame.X = 0; currentFrame.Y = 6; 
            }
            if (last == re_direction.LU && Now_direction == re_direction.RU)
            {
                currentFrame.X = 0; currentFrame.Y = 0;
            }
            if (last == re_direction.LU && Now_direction == re_direction.RD)
            {
                currentFrame.X = 0; currentFrame.Y = 0; 
            } 

            if (last == re_direction.RD && Now_direction == re_direction.LD)
            {
                currentFrame.X = 0; currentFrame.Y = 6;
            }

            if (last == re_direction.RD && Now_direction == re_direction.LU)
            {
                currentFrame.X = 0; currentFrame.Y = 6; 
            }

            if (last == re_direction.LD && Now_direction == re_direction.RD)
            {
                currentFrame.X = 0; currentFrame.Y = 0;
            }
            if (last == re_direction.LD && Now_direction == re_direction.RU)
            {
                currentFrame.X = 0; currentFrame.Y = 0; 
            }
            last = Now_direction;



            position+=speed;

           
            }
        
        
        protected override Vector2 Speed
        {
            get
            { 
                Vector2 inputDirection = Vector2.Zero;
                KeyboardState keyState = Keyboard.GetState();
                if (keyState.IsKeyDown(Keys.Left))
                {
                    inputDirection.X--;
                                     
                }
                if (keyState.IsKeyDown(Keys.Right))
                {
                    inputDirection.X++;
                                 
                }
                if (keyState.IsKeyDown(Keys.Up))
                    inputDirection.Y--;
                if (keyState.IsKeyDown(Keys.Down))
                    inputDirection.Y++;
                return inputDirection * speed;
             }
        }
          public new Rectangle CollisionBoudns
        {
            get 
            {

                    if (direction == false)
                        return new Rectangle((int)position.X, ((int)position.Y + (int)((1 + (level - 1) / 10) * (int)(frameSize.Y / 4))), (int)((1 + (level - 1) / 10)) * (int)(frameSize.X / 2), (int)((1 + (level - 1) / 10) * (int)(frameSize.Y / 2)));
                    else
                        return new Rectangle((int)position.X + (int)((1 + (level - 1) / 10) * frameSize.X / 2), (int)position.Y + (int)((1 + (level - 1) / 10) * (int)(frameSize.Y / 4)), (int)((1 + (level - 1) / 10) * (int)(frameSize.X / 2)), (int)((1 + (level - 1) / 10) * (int)(frameSize.Y / 2)));
                }      

         }
        
          public Rectangle  Base_CollisionBoudns
          {
              get
              {
                  return new Rectangle((int)position.X + (int)((1 + (level - 1) / 10) * collisionOffset), (int)position.Y + collisionOffset, (int)((1 + (level - 1) / 10) * (frameSize.X - 2 * collisionOffset)), (int)((1 + (level - 1) / 10) * (frameSize.Y - 2 * collisionOffset)));
              }
          }
          public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
          {
              //base.Draw(gameTime, spriteBatch);

              
                  spriteBatch.Draw(texture, position, new Rectangle((int)(currentFrame.X - 1) * frameSize.X, (int)currentFrame.Y * frameSize.Y, frameSize.X, frameSize.Y),
                      Color.White, 0, Vector2.Zero, (1 + (level - 1) / 10), SpriteEffects.None, 0);
             }

              
                  
 
              }
          

        
        
    
}
