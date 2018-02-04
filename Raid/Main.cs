using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Raid
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Main : Game
    {
        public enum GameState
        {
            MainMenu,
            InGame,
            GameOver,
            GameWin,
            Scoreboard
        }
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;        
        public static int winWidth = 800, winHeight = 480;

        public static float deltaTime;
        public static int gameScore;
        public static GameState gameState;        
        public static Texture2D srcBullet;
        public static StorageFile data;
        public static SpriteFont ftAmmo, ftScore, ftPName, ftLabel, ftHighScore;
        public Level lvl0;

        Texture2D srcPlayer;                
        Rectangle cursor;                
        
        Obstacle ladder;
        Player player;                
        Button ply_btn;       
        
        string strlist;
        int oldData;
        float timer;

        public Main()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = winWidth,
                PreferredBackBufferHeight = winHeight,
                IsFullScreen = false,                
            };

            graphics.ApplyChanges();
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            gameState = GameState.MainMenu;
            IsMouseVisible = true;                                 
            cursor = new Rectangle(0, 0, 2, 2);
            timer = 100f;
            data = new StorageFile();
            data.LoadFile();
            oldData = data.GetDataNum();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            ftAmmo = Content.Load<SpriteFont>("Ammo");
            ftScore = Content.Load<SpriteFont>("Score");
            ftPName = Content.Load<SpriteFont>("PName");
            ftLabel = Content.Load<SpriteFont>("label");
            ftHighScore = Content.Load<SpriteFont>("scorelist");

            srcPlayer = Content.Load<Texture2D>("img/player_r");
            srcBullet = Content.Load<Texture2D>("img/bullet");
            Texture2D srcWeapon = Content.Load<Texture2D>("img/box_weapon");
            Texture2D srcCoin = Content.Load<Texture2D>("img/Coin");
            Texture2D srcEnemy = Content.Load<Texture2D>("img/enemy");
            Texture2D srcLife = Content.Load<Texture2D>("img/hati");
            Texture2D srcLevel = Content.Load<Texture2D>("img/bg");
            Texture2D srcWall = Content.Load<Texture2D>("img/celling");
            Texture2D srcLadder = Content.Load<Texture2D>("img/tangga");

            ladder = new Obstacle(srcLadder, new Rectangle(32 * 24, 32 * 3,srcLadder.Width,srcLadder.Height));
            player = new Player(srcPlayer, new Rectangle(32 * 0, 32 * 2, srcPlayer.Width, srcPlayer.Height));

            lvl0 = new Level(srcLevel, new Rectangle(0, 0, srcLevel.Width, srcLevel.Height), player)
            {
                ladder = ladder
            };
            lvl0.AddEnemy(srcEnemy, new Rectangle(32 * 19, 32 * 7, srcEnemy.Width, srcEnemy.Height), 0.18f,20);
            lvl0.AddEnemy(srcEnemy, new Rectangle(32 * 8, 32 * 6, srcEnemy.Width, srcEnemy.Height), 0.1f,10);
            lvl0.AddEnemy(srcEnemy, new Rectangle(32 * 15, 32 * 12, srcEnemy.Width, srcEnemy.Height), 0.13f,8);            
            lvl0.AddEnemy(srcEnemy, new Rectangle(32 * 6, 32 * 12, srcEnemy.Width, srcEnemy.Height), 0.09f,5);

            lvl0.AddWeapon(srcWeapon, new Rectangle(32 * 0, 32 * 12, srcWeapon.Width, srcWeapon.Height),
                "Shotgun", 2);
            lvl0.AddWeapon(srcWeapon, new Rectangle(32 * 24, 32 * 14, srcWeapon.Width, srcWeapon.Height),
                "Pistol", 3);            

            lvl0.AddCoin(srcCoin, new Rectangle(32 * 23, 32 * 11, srcCoin.Width, srcCoin.Height), 5);
            lvl0.AddCoin(srcCoin, new Rectangle(32 * 24, 32 * 11, srcCoin.Width, srcCoin.Height), 5);
            lvl0.AddCoin(srcCoin, new Rectangle(32 * 24, 32 * 10, srcCoin.Width, srcCoin.Height), 5);
            lvl0.AddCoin(srcCoin, new Rectangle(32 * 23, 32 * 10, srcCoin.Width, srcCoin.Height), 5);                        
            for (int i = 12; i > 9; i--)
            {
                lvl0.AddCoin(srcCoin, new Rectangle(32 * 10, 32 * i, srcCoin.Width, srcCoin.Height), 5);
            }
            for (int i = 13; i > 10; i--)
            {
                lvl0.AddCoin(srcCoin, new Rectangle(32 * 11, 32 * i, srcCoin.Width, srcCoin.Height), 5);
            }
            for (int i = 7; i <= 9; i++)
            {
                lvl0.AddCoin(srcCoin, new Rectangle(32 * 0, 32 * i, srcCoin.Width, srcCoin.Height), 5);
            }
            for (int i = 7; i <= 9; i++)
            {
                lvl0.AddCoin(srcCoin, new Rectangle(32 * 1, 32 * i, srcCoin.Width, srcCoin.Height), 5);
            }            
            for (int i = 3; i < 9; i++)
            {
                lvl0.AddCoin(srcCoin, new Rectangle(32 * i, 32 * 3, srcCoin.Width, srcCoin.Height), 5);
            }            

            #region Wall Design
            int x = 0;
            for (int i = x; i < 25; i++)
            {
                if (!(x == 9 || x == 10 || x == 11 || x == 15))
                {                    
                    lvl0.AddWall(srcWall, new Rectangle(32 * x, 32 * 5, srcWall.Width, srcWall.Height));                    
                }                
                x++;
            }

            x = 14;
            for (int i = x;i < 19; i++)
            {                
                lvl0.AddWall(srcWall, new Rectangle(32 * x, 32 * 9, srcWall.Width, srcWall.Height));
                x++;
            }
            
            lvl0.AddWall(srcWall, new Rectangle(32 * 4, 32 * 9, srcWall.Width, srcWall.Height));
            lvl0.AddWall(srcWall, new Rectangle(32 * 7, 32 * 9, srcWall.Width, srcWall.Height));                        

            int y = 15;
            for (int i = y; i > 8; i--)
            {                
                lvl0.AddWall(srcWall, new Rectangle(32 * 8, 32 * y, srcWall.Width, srcWall.Height));
                y--;
            }

            y = 2;
            for (int i = y; i < 15; i++)
            {                
                if (!(y == 5 || y == 12 || y == 11 || y == 13))
                {                    
                    lvl0.AddWall(srcWall, new Rectangle(32 * 13, 32 * y, srcWall.Width, srcWall.Height));
                }                
                y++;
            }
            
            x = 25;
            for (int i = x; i > 19; i--)
            {                
                lvl0.AddWall(srcWall, new Rectangle(32 * x, 32 * 12, srcWall.Width, srcWall.Height));                
                x--;
            }

            x = 0;
            for (int i = 0; i < 5; i++)
            {                
                lvl0.AddWall(srcWall, new Rectangle(32 * x, 32 * 10, srcWall.Width, srcWall.Height));                
                x++;
            }
            #endregion            
                                    
            ply_btn = new Button(Content.Load<Texture2D>("img/play_button"), new Rectangle(368, 224, 64, 32));

            var px = 688;
            for (int i = 0; i<player.HP; i++)
            {                
                player.life[i] = new Life(srcLife, new Rectangle(px, 16, srcLife.Width, srcLife.Height));
                px += 32;
            }            
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            deltaTime = gameTime.ElapsedGameTime.Milliseconds;            
            cursor.X = Mouse.GetState().X;
            cursor.Y = Mouse.GetState().Y;

            // TODO: Add your update logic here

            switch (gameState)
            {
                case GameState.MainMenu:                                                           
                    break;
                case GameState.InGame:
                    lvl0.Update(gameTime);
                    #region Enemies property
                    for (int i = 0; i<Level.enemies.Capacity; i++)
                    {
                        string str = "";
                        int min = 0, max = 0;
                        switch (i)
                        {
                            case 0:
                                str = "Vertical";
                                min = 32 * 6 + 16;
                                max = 32 * 10;
                                break;
                            case 1:
                                str = "H";
                                min = 32 * 7;
                                max = 32 * 12;
                                break;
                            case 2:
                                str = "Horizontal";
                                min = 32 * 13;
                                max = 32 * 16 + 16;
                                break;
                            case 3:
                                str = "V";
                                min = 32 * 8 + 16;
                                max = 32 * 12 + 16;
                                break;
                        }
                        lvl0.GetEnemy(i).Moving(Main.deltaTime, min, max, str);
                    }
                    #endregion
                    break;
                case GameState.GameOver:
                    break;
                case GameState.GameWin:
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
            GraphicsDevice.Clear(Color.DimGray);

            // TODO: Add your drawing code here            
            spriteBatch.Begin();            
            switch (gameState)
            {
                case GameState.MainMenu:
                    #region MANIMENU                    
                    spriteBatch.DrawString(ftLabel, "RAID THE GAME!", new Vector2(294, 126), Color.White);
                    if (cursor.Intersects(ply_btn.physic))
                    {
                        ply_btn.Draw(spriteBatch, Content.Load<Texture2D>("img/play_button"), Color.DimGray);
                        if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                        {
                            gameState = GameState.InGame;
                        }                        
                    }
                    else
                    {
                        gameState = GameState.MainMenu;
                        ply_btn.Draw(spriteBatch, Content.Load<Texture2D>("img/play_button"), Color.White);
                    }
                    #endregion
                    break;
                case GameState.InGame:
                    lvl0.Draw(spriteBatch); 

                    //DO NOT DELETE THIS!
                    switch (player.dir)
                    {
                        case Player.Direction.right:
                            srcPlayer = Content.Load<Texture2D>("img/player_r");
                            break;
                        case Player.Direction.left:
                            srcPlayer = Content.Load<Texture2D>("img/player_l");
                            break;
                        case Player.Direction.bottom:
                            srcPlayer = Content.Load<Texture2D>("img/player_d");
                            break;
                        case Player.Direction.top:
                            srcPlayer = Content.Load<Texture2D>("img/player_up");
                            break;
                    }
                    player.Image = srcPlayer;
                    
                    spriteBatch.DrawString(ftScore, "Score : " + gameScore, new Vector2(32, 28), Color.White);                    
                    spriteBatch.DrawString(ftPName, "Name : " + player.Name, new Vector2(32, 8), Color.White);                    
                    break;
                case GameState.GameOver:
                    spriteBatch.DrawString(ftLabel, "GAME OVER...", new Vector2(302, 189), Color.Red);
                    timer -= deltaTime * 0.1f;
                    if (timer <= 0)
                        gameState = GameState.Scoreboard;
                    break;
                case GameState.GameWin:
                    spriteBatch.DrawString(ftLabel, "GAME FINISHED", new Vector2(302, 189), Color.Blue);
                    timer -= deltaTime * 0.1f;
                    if (timer <= 0)
                        gameState = GameState.Scoreboard;                    
                    break;
                case GameState.Scoreboard:
                    if (!(data.GetDataNum() > oldData))
                    {
                        strlist = "Last try " + data.GetDataNum() + " : " + gameScore + " pts\n";
                        data.StoreData(strlist, gameScore);
                    }
                    data.SaveFile();

                    spriteBatch.DrawString(ftLabel, "HIGHSCORE : " + data.GetHighscore() + " pts", new Vector2(302, 189), Color.White);
                    data.LoadFile();               
                    spriteBatch.DrawString(ftHighScore, data.GetData(), new Vector2(336, 240), Color.White);
                    break;
            }
            
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
