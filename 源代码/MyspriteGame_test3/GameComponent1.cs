using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace MyspriteGame_test3
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class GameComponent1 : Microsoft.Xna.Framework.DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        UserControledSprite player;
        List<AutoSprite> spritelList=new List<AutoSprite>();
        List<ChasingSprite> sprite2List = new List<ChasingSprite>();
        
        SoundEffect F1Sound;
        public int score = 0;
        public int eatten = 0;
        public float test;
        SpriteFont scoreFont;

        int enemySpawnMinMilliseconds = 500; 
        int enemySpawnMaxMilliseconds = 1000;
        int enemyMinSpeed = 2;
        int enemyMaxSpeed = 3;
        int nextSpawnTime = 0;

        int enemynum_max1 = 10;


        private void ResetSpawnTime()
        { nextSpawnTime = ((Game1)Game).rnd.Next(enemySpawnMinMilliseconds, enemySpawnMaxMilliseconds); } 
  
        public GameComponent1(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            ResetSpawnTime(); 
            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            nextSpawnTime -= gameTime.ElapsedGameTime.Milliseconds; 
            if (nextSpawnTime < 0) 
            {
                if (spritelList.Count <= enemynum_max1 )
                SpawnEnemy1();
                ResetSpawnTime();
            } 

            player.Update(gameTime,Game.Window.ClientBounds);
          //  foreach (AutoSprite s in spritelList)
            for (int i = 0; i < sprite2List.Count; ++i)
            {

                sprite2List[i].Update(gameTime, Game.Window.ClientBounds);
                if (player.level <= 3 && sprite2List[i].CollisionBoudns.Intersects(player.Base_CollisionBoudns))           
                 {
                     ((Game1)Game).gameState = GameState.GameOver;
                     
                 }
                if (player.level > 3 && player.CollisionBoudns.Intersects(sprite2List[i].Base_CollisionBoudns))
                {
                    
                    score += 2;
                    sprite2List.RemoveAt(i);
                }

            }
 
            for (int i = 0; i < spritelList.Count; ++i)
            {
                
                spritelList[i].Update(gameTime, Game.Window.ClientBounds);

                if (spritelList[i].CollisionBoudns.Intersects(player.CollisionBoudns))
                {
                    spritelList[i].Sound.Play();
                    score += spritelList[i].Score;
                    spritelList.RemoveAt(i);
                    eatten++;
                   // Game.Exit();
                }
            }
            if (eatten == 10)
            {
                player.level+=2;
                eatten -= 10;
            }
            test = player.level;
            if (spritelList.Count == 0)
            {
                ((Game1)Game).gameState = GameState.GameOver;
            }

            base.Update(gameTime);
        }
        protected override void LoadContent()
        {

            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
              F1Sound = Game.Content.Load<SoundEffect>(@"Audio//DropCell");
             player = new UserControledSprite(Game.Content.Load<Texture2D>(@"img//003"), new Point(0, 6), new Point(125, 105), 
                new Point(4, 8), new Vector2(6, 6), new Vector2(Game.Window.ClientBounds.Width / 2, Game.Window.ClientBounds.Height / 2),false,1,5);
             
             /*
              *
             spritelList.Add(new AutoSprite(Game.Content.Load<Texture2D>(@"img//F1_swim"),new Point(0,4),new Point(65,48),
                 new Point(4,8),new Vector2(3,3),new Vector2(1,100) ,5,F1Sound,1));
             spritelList.Add(new AutoSprite(Game.Content.Load<Texture2D>(@"img//F1_swim"), new Point(0, 4), new Point(65, 48),
                 new Point(4, 8), new Vector2(3, 3), new Vector2(1, 300), 5, F1Sound,1));
              */
             spritelList.Add(new AutoSprite(Game.Content.Load<Texture2D>(@"img//F1_swim"), new Point(0, 4), new Point(65, 48),
                 new Point(4, 8), new Vector2(3, 3), new Vector2(1, 400), 5, F1Sound,1 ));

              
            
            scoreFont = Game.Content.Load<SpriteFont>(@"Fonts//ScoreFont1");
           
            base.LoadContent();
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin( SpriteSortMode.FrontToBack,BlendState.AlphaBlend);
            player.Draw(gameTime, spriteBatch);
            foreach (AutoSprite s in spritelList)
            {
                s.Draw(gameTime, spriteBatch);
            }
            foreach (ChasingSprite d in sprite2List)
            {
                d.Draw(gameTime,spriteBatch); 
            base.Draw(gameTime);
            }
            spriteBatch.DrawString(scoreFont, "Score:"+score.ToString(), Vector2.Zero, Color.Black);
            spriteBatch.DrawString(scoreFont, "last_direction "+player.last.ToString(), new Vector2(100, 0), Color.Black);
            spriteBatch.DrawString(scoreFont, "now_direction "+player.Now_direction.ToString(), new Vector2(350,0), Color.Black);


            spriteBatch.DrawString(scoreFont, "(L:false R:true)"+player.direction.ToString(), new Vector2(80, 80), Color.Black);
            spriteBatch.DrawString(scoreFont,"X:" +player.position.X.ToString(), new Vector2(160, 160), Color.Black);
            spriteBatch.DrawString(scoreFont, "Y"+player.position.Y.ToString(), new Vector2(240, 160), Color.Black);
            spriteBatch.DrawString(scoreFont,   "level:"+player.level.ToString(), new Vector2(700, 0), Color.Black);
            spriteBatch.DrawString(scoreFont, "Test=" + test.ToString(), new Vector2(900, 0), Color.Black);

            spriteBatch.End();
            
        }
        protected override void UnloadContent()
        {
            F1Sound.Dispose();
            base.UnloadContent();
        }

        private void SpawnEnemy1()
        {
            Vector2 speed = Vector2.Zero; 
            Vector2 position = Vector2.Zero;
            Point frameSize1 = new Point(65, 48);
            Point frameSize2 = new Point(83, 46);


            // Randomly choose which side of the screen to place enemy
            // then randomly create a position along that side of the screen 
            // and randomly choose a speed for the enemy 
            switch (((Game1)Game).rnd.Next(14))
            {
                case 0://Left to Right(Down)
                case 1:
                case 2:
                    position = new Vector2(-position.X, ((Game1)Game).rnd.Next(0,((Game1)Game).Window.ClientBounds.Height));
                    speed = new Vector2(((Game1)Game).rnd.Next(enemyMinSpeed, enemyMaxSpeed), ((Game1)Game).rnd.Next(enemyMinSpeed, enemyMaxSpeed));
                    spritelList.Add(new AutoSprite(Game.Content.Load<Texture2D>(@"img//F1_swim"), new Point(0, 4), frameSize1,
                 new Point(4, 8), speed, position, 5, F1Sound, 1));
                    break;
                case 3://Right to Left(Down)
                case 4:
                case 5:
                    position = new Vector2(((Game1)Game).Window.ClientBounds.Width, ((Game1)Game).rnd.Next(0, ((Game1)Game).Window.ClientBounds.Height));
                    speed = new Vector2(-((Game1)Game).rnd.Next(enemyMinSpeed, enemyMaxSpeed), ((Game1)Game).rnd.Next(enemyMinSpeed, enemyMaxSpeed));
                    spritelList.Add(new AutoSprite(Game.Content.Load<Texture2D>(@"img//F1_swim"), new Point(0, 0), frameSize1,
                 new Point(4, 8), speed, position, 5, F1Sound, 1));
                    break;
                case 6://Left to Right(Up)
                case 7:
                case 8:
                    position = new Vector2(-position.X, ((Game1)Game).rnd.Next(0, ((Game1)Game).Window.ClientBounds.Height));
                    speed = new Vector2(((Game1)Game).rnd.Next(enemyMinSpeed, enemyMaxSpeed), -((Game1)Game).rnd.Next(enemyMinSpeed, enemyMaxSpeed));
                    spritelList.Add(new AutoSprite(Game.Content.Load<Texture2D>(@"img//F1_swim"), new Point(0, 4), frameSize1,
                 new Point(4, 8), speed, position, 5, F1Sound, 1));
                    break;
                case 9://Right to Left(Up)
                case 10:
                case 11:
                    position = new Vector2(((Game1)Game).Window.ClientBounds.Width, ((Game1)Game).rnd.Next(0, ((Game1)Game).Window.ClientBounds.Height));
                    speed = new Vector2(-((Game1)Game).rnd.Next(enemyMinSpeed, enemyMaxSpeed), -((Game1)Game).rnd.Next(enemyMinSpeed, enemyMaxSpeed));
                    spritelList.Add(new AutoSprite(Game.Content.Load<Texture2D>(@"img//F1_swim"), new Point(0, 0), frameSize1,
                 new Point(4, 8), speed, position, 5, F1Sound, 1));
                    break;
                case 12:
                    sprite2List.Add(new ChasingSprite(Game.Content.Load<Texture2D>(@"img//000"), new Point(0, 0), new Point(83, 46),
                    new Point(4, 8), new Vector2(-2, -2), new Vector2(-(int)(frameSize2.X*2), ((Game1)Game).Window.ClientBounds.Height - 3*frameSize2.Y), 5, F1Sound, this));
                    break;
                case 13:
                    sprite2List.Add(new ChasingSprite(Game.Content.Load<Texture2D>(@"img//000"), new Point(0, 0), new Point(83, 46),
                     new Point(4, 8), new Vector2(-2, -2), new Vector2(-(int)(frameSize2.X*2), 10), 5, F1Sound, this)); 
                    break;
            }
            //Create
            
        }
        public Vector2 getplayerPosition()
        {
            return player.getPosition;
            
        }
        public Rectangle getplayerRange()
        {
            return player.Base_CollisionBoudns;
        }


    }

}
