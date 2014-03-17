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

namespace BatGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Grid grid;

        Texture2D playerImage;
        Texture2D enemyImage;
        SpriteFont comicSans14;

        Player player;
        Enemy enemy;

        EnemyManager enemyManager;
        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
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
            grid = new Grid(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            player = new Player(playerImage, new Rectangle(0, 0, grid.Width, grid.Height), 
                new Point(0,0), grid, true, 0, .6, true, 0, Direction.Right);
            enemy = new Enemy(enemyImage, new Rectangle(1, 1, grid.Width, grid.Height),
                new Point(1, 1), grid, true, 0, .6, true, 0, false, Direction.Right);
            enemyManager = new EnemyManager();
            enemyManager.AddEnemy(enemy);
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
            comicSans14 = Content.Load<SpriteFont>("ComicSans");
            playerImage = Content.Load<Texture2D>("player");
            enemyImage = Content.Load<Texture2D>("enemy");
            player.ObjTexture = playerImage;
            enemy.ObjTexture = enemyImage;
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
            player.PlayerUpdate();
            player.Speed += gameTime.ElapsedGameTime.TotalSeconds;
            enemyManager.Update();
            Console.WriteLine(player.Speed);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.DrawString(comicSans14, "Grid Size: " + grid.Width + ", " + grid.Height, 
                new Vector2(10, 10), Color.Orange);
            spriteBatch.DrawString(comicSans14, "Direction: " + player.Facing,
                new Vector2(10, 30), Color.Orange);
            spriteBatch.DrawString(comicSans14, "Enemies: " + enemyManager.Count,
                new Vector2(10, 50), Color.Orange);
            spriteBatch.Draw(player.ObjTexture, player.ObjRectangle, Color.White);
            spriteBatch.Draw(enemy.ObjTexture, enemy.ObjRectangle, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
