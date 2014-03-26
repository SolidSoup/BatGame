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
using System.IO;

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
        Texture2D verticalWallImage;
        Texture2D horizontalWallImage;
        Texture2D cornerWallImage;
        Texture2D floorTileImage;

        SpriteFont comicSans14;

        Player player;
        Enemy enemy;

        EnemyManager enemyManager;

        String[,] check;
        GameObject[,] checkMap;

        Dictionary<string, Texture2D> spriteDictionary;
        

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
            player = new Player(playerImage, new Point(0,0), grid, Direction.Right, true, 0, .4, true, 0);
            enemy = new Enemy(enemyImage, new Point(1, 1), grid, Direction.Right, true, 0, .6, true, 0, false);
            enemyManager = new EnemyManager();
            enemyManager.AddEnemy(enemy);
            spriteDictionary = new Dictionary<string, Texture2D>();

            
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
            spriteDictionary.Add("playerImage", playerImage);

            enemyImage = Content.Load<Texture2D>("enemy");
            spriteDictionary.Add("enemyImage", enemyImage);

            verticalWallImage = Content.Load<Texture2D>("VerticalWall");
            spriteDictionary.Add("verticalWall", verticalWallImage);

            horizontalWallImage = Content.Load<Texture2D>("HorizontalWall");
            spriteDictionary.Add("horizontalWall", horizontalWallImage);

            cornerWallImage = Content.Load<Texture2D>("Corner");
            spriteDictionary.Add("cornerWall", cornerWallImage);

            floorTileImage = Content.Load<Texture2D>("FloorTile");
            spriteDictionary.Add("floorTile", floorTileImage);


            player.ObjTexture = playerImage;
            //Later, there will be a LoadEnemyTexture method here instead
            enemy.ObjTexture = enemyImage;
            /*Level level1 = new Level("Content/level2.txt");
            check = level1.loadLevel();
            //LoadMap("Content/level1.txt");

            //doesnt currently work, move mouse over method to read why
            checkMap = level1.setupLevel(spriteDictionary, grid, enemyManager);*/
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
            enemyManager.EManagerUpdate();
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

            /*
            // tests drawing the 2D array of GameObjects's from the setup level method in the level class, 
            // commented out because of the reasons mentioned at the setupLevel method description in the level class
            
            for (int i = 0; i < checkMap.GetLength(0); i++)
            {
                for (int j = 0; j < checkMap.GetLength(1); j++)
                {
                    spriteBatch.Draw(checkMap[i, j].ObjTexture, new Rectangle((grid.Width / 16) * i, (grid.Height / 16) * j, verticalWallImage.Width, verticalWallImage.Height) , Color.White);
                }
            }
             * */

            spriteBatch.DrawString(comicSans14, "Grid Size: " + grid.TileWidth + ", " + grid.TileHeight, 
                new Vector2(10, 10), Color.Orange);
            spriteBatch.DrawString(comicSans14, "Direction: " + player.Facing,
                new Vector2(10, 30), Color.Orange);
            spriteBatch.DrawString(comicSans14, "Enemies: " + enemyManager.Count,
                new Vector2(10, 50), Color.Orange);
            spriteBatch.DrawString(comicSans14, "Position: " + player.PosX + ", " + player.PosY,
                new Vector2(170, 10), Color.Orange);

            /*int x = 15;
            int y = 15;

            for (int i = 0; i < check.GetLength(0); i++)
            {
                for (int j = 0; j < check.GetLength(1); j++)
                {
                    spriteBatch.DrawString(comicSans14, check[i, j],
                        new Vector2(x * j, 70 + y * i), Color.Orange);
                }
            }*/

            spriteBatch.Draw(player.ObjTexture, player.ObjRectangle, Color.White);
            enemyManager.EManagerDraw(spriteBatch);
            //spriteBatch.Draw(enemy.ObjTexture, enemy.ObjRectangle, Color.White);

            

            spriteBatch.End();

            base.Draw(gameTime);
        }

        #region LoadMap
        protected void LoadMap(String file)
        {

            String[,] data = new string[100, 100];
            String[,] map;
            int longestRow = 0;

            StreamReader input = null;

            try
            {
                input = new StreamReader(file);

                String line = "";
                int counter = 0;

                while ((line = input.ReadLine()) != null)
                {
                    if (line.Length > longestRow)
                    {
                        longestRow = line.Length;
                    }

                    for (int i = 0; i < line.Length; i++)
                    {
                        data[counter, i] = line.Substring(i, 1);
                    }
                    counter++;
                }


                map = new string[counter, longestRow];
                for (int i = 0; i < counter; i++)
                {
                    for (int j = 0; j < longestRow; j++)
                    {
                        map[i, j] = data[i, j];
                    }
                }

                check = map;

                for (int i = 0; i < map.GetLength(0); i++)
                {
                    for (int j = 0; j < map.GetLength(1); j++)
                    {
                        switch (map[i, j])
                        {
                            case "|":
                                //Add a vertical wall
                                break;
                            case "+":
                                //Add a corner
                                break;
                            case "-":
                                // Add a horizontal wall
                                break;
                            case "1":
                                //Add a floor tile
                                break;
                            case "e":
                                Enemy jim = new Enemy(enemyImage, new Point(j, i), grid, Direction.Down, true, 0, 0, true, 3, false);
                                enemyManager.AddEnemy(jim);
                                break;
                            case "p":
                                //Add a floor tile
                                //Add a player
                                break;
                        }
                    }
                }

            }
            catch (Exception)
            {
            }
            finally
            {
                if (input != null)
                {
                    input.Close();
                }
            }
        }
        #endregion
    }
}
