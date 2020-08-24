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
    public enum GameState { start,inGame,GameOver};
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GameComponent1 spriteManager;
        public GameState gameState = GameState.start;
        SpriteFont stringFont;
        Texture2D background;

        public Random rnd { get; private set; }
        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = 768; 
            graphics.PreferredBackBufferWidth = 1440;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            rnd = new Random();
            spriteManager = new GameComponent1(this);
            Components.Add(spriteManager);
            spriteManager.Enabled = false;
            spriteManager.Visible = false;
            this.IsMouseVisible = true;

            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {  
            // Create a new SpriteBatch, which can be used to draw textures.
            background = Content.Load<Texture2D>(@"img//bgimg");
            spriteBatch = new SpriteBatch(GraphicsDevice);
            stringFont = Content.Load<SpriteFont>(@"Fonts//MyFont1");
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
           
            // TODO: Add your update logic here
            KeyboardState keyState = Keyboard.GetState();
            switch (gameState)
            {
                case GameState.start:
                    if (keyState.GetPressedKeys().Length > 0)
                    {
                        gameState = GameState.inGame;
                        spriteManager.Enabled = true;
                        spriteManager.Visible = true;
                    }

                  
                    break;
                case GameState.inGame:
                    
                    break;
                case GameState.GameOver:
                    spriteManager.Enabled = false;
                    spriteManager.Visible = false;
                    if (keyState.IsKeyDown(Keys.Enter))
                    {
                        Exit();
                    }
                    break;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here
            string str;
            switch (gameState)
            { 
                case GameState.start:
                      spriteBatch.Begin();
                    str = "Welcome!please press any key to continue!";
                    spriteBatch.DrawString(stringFont,str,new Vector2(Window.ClientBounds.Width/2-stringFont.MeasureString(str).X/2,Window.ClientBounds.Height/2-stringFont.MeasureString(str).Y/2),Color.Black);
                    spriteBatch.End();
                    

                    break;
                case GameState.inGame:
                    spriteBatch.Begin();
                    spriteBatch.Draw(background, Vector2.Zero, Color.White);
                    spriteBatch.End();
                    break;
                case GameState.GameOver:
                    spriteBatch.Begin();
                    spriteBatch.Draw(background,Vector2.Zero,Color.White);
                    str ="your score is "+spriteManager.score+"!  please press Enter key to exit!";
                    spriteBatch.DrawString(stringFont, str, new Vector2(Window.ClientBounds.Width / 2 - stringFont.MeasureString(str).X / 2, Window.ClientBounds.Height / 2 - stringFont.MeasureString(str).Y / 2), Color.Black);
                    spriteBatch.End();
                    break;
            } 
            base.Draw(gameTime);
        }
    }
}
