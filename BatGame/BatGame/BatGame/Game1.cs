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
        //bat texture
        Texture2D batUpDownImage;
        Texture2D batSideImage;

        Texture2D enemyImage;
        Texture2D verticalWallImage;
        Texture2D verticalLeftWallImage;
        Texture2D verticalRightWallImage;
        Texture2D horizontalWallImage;
        Texture2D horizontalDownWallImage;
        Texture2D horizontalUpWallImage;
        Texture2D cornerWallImage;
        Texture2D upRightCornerWallImage;
        Texture2D upLeftCornerWallImage;
        Texture2D downRightCornerWallImage;
        Texture2D downLeftCornerWallImage;
        Texture2D floorTileImage;
        
        Texture2D lightMask;
        Texture2D rightCone;
        Texture2D leftCone;
        Texture2D upCone;
        Texture2D downCone;
        Texture2D upRightCone;
        Texture2D upLeftCone;
        Texture2D downRightCone;
        Texture2D downLeftCone;

        Rectangle cone;

        RenderTarget2D lightsTarget;
        RenderTarget2D mainTarget;

        Effect lightingEffect;

        SpriteFont comicSans14;

        Player player;
        Enemy enemy;

        EnemyManager enemyManager;
        ImmobilesManager immobilesManager;
        GameObjectManager gameObjectManager;

        String[,] check;
        GameObject[,] checkMap;

        Dictionary<string, Texture2D> spriteDictionary;
        AnimationFarm playerAnimation;

        //Things that will eventually be moved to state manager
        GameState gameState;
        Texture2D menuImage;
        Texture2D startButtonImage;
        Texture2D optionsButtonImage;
        Texture2D loadButtonImage;
        Texture2D nameLogoImage;
        Button startButton;
        Button optionButton;
        Button loadButton;
        MouseState lastMouseState;
        MouseState currentMouseState;

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
            enemyManager = new EnemyManager();
            immobilesManager = new ImmobilesManager();
            gameObjectManager = new GameObjectManager();

            spriteDictionary = new Dictionary<string, Texture2D>();
            //aniFarm = new AnimationFarm(spriteBatch);

            player = new Player(playerImage, gameObjectManager, new Point(2, 2), grid, Direction.Right, 
                SubSquares.TopLeft, true, 0, .4, true, 0);

            gameState = GameState.menu;

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

            lightMask = Content.Load<Texture2D>("Sprites/Shader_Sprites/lightmask");
            spriteDictionary.Add("lightmask", lightMask);

            rightCone = Content.Load<Texture2D>("Sprites/Shader_Sprites/rightCone");
            spriteDictionary.Add("rightCone", rightCone);

            leftCone = Content.Load<Texture2D>("Sprites/Shader_Sprites/leftCone");
            spriteDictionary.Add("leftCone", leftCone);

            upCone = Content.Load<Texture2D>("Sprites/Shader_Sprites/upCone");
            spriteDictionary.Add("upCone", upCone);

            downCone = Content.Load<Texture2D>("Sprites/Shader_Sprites/downCone");
            spriteDictionary.Add("downCone", downCone);

            upLeftCone = Content.Load<Texture2D>("Sprites/Shader_Sprites/upLeftCone");
            spriteDictionary.Add("upLeftCone", upLeftCone);

            upRightCone = Content.Load<Texture2D>("Sprites/Shader_Sprites/upRightCone");
            spriteDictionary.Add("upRightCone", upRightCone);

            downLeftCone = Content.Load<Texture2D>("Sprites/Shader_Sprites/downLeftCone");
            spriteDictionary.Add("downLeftCone", downLeftCone);

            downRightCone = Content.Load<Texture2D>("Sprites/Shader_Sprites/downRightCone");
            spriteDictionary.Add("downRightCone", downRightCone);

            lightingEffect = Content.Load<Effect>("Shader_Tools/lightingeffect");

            var pp = GraphicsDevice.PresentationParameters;
            lightsTarget = new RenderTarget2D(
                GraphicsDevice, pp.BackBufferWidth, pp.BackBufferHeight);
            mainTarget = new RenderTarget2D(
                GraphicsDevice, pp.BackBufferWidth, pp.BackBufferHeight);
            // TODO: use this.Content to load your game content here
            comicSans14 = Content.Load<SpriteFont>("Font/ComicSans");

            playerImage = Content.Load<Texture2D>("Sprites/Bat_Sprites/player");
            spriteDictionary.Add("playerImage", playerImage);


            batUpDownImage = Content.Load<Texture2D>("Sprites/Bat_Sprites/Bat_Fly_Up");
            spriteDictionary.Add("BatUpDownImage", batUpDownImage);

            batSideImage = Content.Load<Texture2D>("Sprites/Bat_Sprites/Bat_Fly_Left");
            spriteDictionary.Add("batSideImage", batSideImage);

            enemyImage = Content.Load<Texture2D>("Sprites/Enemy_Sprites/enemy");
            spriteDictionary.Add("enemyImage", enemyImage);

            verticalWallImage = Content.Load<Texture2D>("Sprites/Walls/VerticalWall");
            spriteDictionary.Add("verticalWall", verticalWallImage);

            verticalLeftWallImage = Content.Load<Texture2D>("Sprites/Walls/VerticalLeftWall");
            spriteDictionary.Add("verticalLeftWall", verticalLeftWallImage);

            verticalRightWallImage = Content.Load<Texture2D>("Sprites/Walls/VerticalRightWall");
            spriteDictionary.Add("verticalRightWall", verticalRightWallImage);

            horizontalWallImage = Content.Load<Texture2D>("Sprites/Walls/HorizontalWall");
            spriteDictionary.Add("horizontalWall", horizontalWallImage);

            horizontalUpWallImage = Content.Load<Texture2D>("Sprites/Walls/HorizontalUpWall");
            spriteDictionary.Add("horizontalUpWall", horizontalUpWallImage);

            horizontalDownWallImage = Content.Load<Texture2D>("Sprites/Walls/HorizontalDownWall");
            spriteDictionary.Add("horizontalDownWall", horizontalDownWallImage);

            cornerWallImage = Content.Load<Texture2D>("Sprites/Corners/Corner");
            spriteDictionary.Add("cornerWall", cornerWallImage);

            downLeftCornerWallImage = Content.Load<Texture2D>("Sprites/Corners/DownLeftCorner");
            spriteDictionary.Add("downLeftCornerWall", downLeftCornerWallImage);

            upLeftCornerWallImage = Content.Load<Texture2D>("Sprites/Corners/UpLeftCorner");
            spriteDictionary.Add("upLeftCornerWall", upLeftCornerWallImage);

            downRightCornerWallImage = Content.Load<Texture2D>("Sprites/Corners/DownRightCorner");
            spriteDictionary.Add("downRightCornerWall", downRightCornerWallImage);

            upRightCornerWallImage = Content.Load<Texture2D>("Sprites/Corners/UpRightCorner");
            spriteDictionary.Add("upRightCornerWall", upRightCornerWallImage);

            floorTileImage = Content.Load<Texture2D>("Sprites/Floors/FloorTile");
            spriteDictionary.Add("floorTile", floorTileImage);


            player.ObjTexture = playerImage;

            // LoadMap("Content/level1.txt");
            Level level1 = new Level("Content/Levels/level1.txt");
            check = level1.loadLevel();
            checkMap = level1.setupLevel(spriteDictionary, grid, enemyManager, immobilesManager, gameObjectManager);

            menuImage = Content.Load<Texture2D>("Sprites/Menu_Sprites/menu");
            nameLogoImage = Content.Load<Texture2D>("Sprites/Menu_Sprites/nameLogo");
            startButtonImage = Content.Load<Texture2D>("Sprites/Menu_Sprites/startButton");
            loadButtonImage = Content.Load<Texture2D>("Sprites/Menu_Sprites/loadButton");
            optionsButtonImage = Content.Load<Texture2D>("Sprites/Menu_Sprites/optionsButton");

            int height = 2 * GraphicsDevice.Viewport.Height / 3;
            startButton = new Button((GraphicsDevice.Viewport.Width / 3) - 90, height, 120, 90, startButtonImage);
            loadButton = new Button((GraphicsDevice.Viewport.Width / 2) - 40, height, 120, 90, loadButtonImage);
            optionButton = new Button(2 * GraphicsDevice.Viewport.Width / 3, height, 120, 90, optionsButtonImage);
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
            switch (gameState)
            {
                case GameState.game:
                    player.PlayerUpdate();

                    player.Speed += gameTime.ElapsedGameTime.TotalSeconds;
                    enemyManager.EManagerUpdate(gameTime, player);
                    base.Update(gameTime);
                    break;
                case GameState.menu:
                    startButton.Selected = false;
                    optionButton.Selected = false;
                    loadButton.Selected = false;

                    lastMouseState = currentMouseState;
                    currentMouseState = Mouse.GetState();
                    Point pos = new Point(currentMouseState.X, currentMouseState.Y);

                    if (startButton.Rect.Contains(pos))
                    {
                        startButton.Selected = true;

                        if (lastMouseState.LeftButton == ButtonState.Released && currentMouseState.LeftButton == ButtonState.Pressed)
                        {
                            gameState = GameState.game;
                        }
                    }
                    if (optionButton.Rect.Contains(pos))
                    {
                        optionButton.Selected = true;

                        if (lastMouseState.LeftButton == ButtonState.Released && currentMouseState.LeftButton == ButtonState.Pressed)
                        {
                            //Pop up options
                        }
                    }
                    if (loadButton.Rect.Contains(pos))
                    {
                        loadButton.Selected = true;

                        if (lastMouseState.LeftButton == ButtonState.Released && currentMouseState.LeftButton == ButtonState.Pressed)
                        {
                            //load saved game
                        }
                    }

                    base.Update(gameTime);
                    break;
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            switch (gameState)
            {
                case GameState.game:
                    this.IsMouseVisible = false;

                    GraphicsDevice.SetRenderTarget(lightsTarget);
                    GraphicsDevice.Clear(Color.DarkGray); // Change this to alter the "base" lighting
                    spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive);
                    //lightMask bound to player's position as a little sphere of vision
                    Vector2 light = new Vector2(player.RectX - 45, player.RectY - 50);
                    spriteBatch.Draw(lightMask, light, Color.White);

                    // To add more lights: draw them here, with whatever color you want!
                    if (player.Screech == true) 
                    {
                        player.Screeching = true;
                        switch (player.Facing)  //switch statement draws echolocation wave wherever the player is facing
                        {
                            case Direction.Right:
                                cone = new Rectangle(player.RectX, player.RectY - 13, 56, 56);  //makes the starting position of the wave
                                spriteBatch.Draw(rightCone, cone, Color.Green);
                                player.Screechdirection = Direction.Right;
                                break;
                            case Direction.Left:
                                cone = new Rectangle(player.RectX - 30, player.RectY - 13, 56, 56);  //makes the starting position of the wave
                                spriteBatch.Draw(leftCone, cone, Color.Green);
                                player.Screechdirection = Direction.Left;
                                break;
                            case Direction.Up:
                                cone = new Rectangle(player.RectX - 10, player.RectY - 35, 56, 56);  //makes the starting position of the wave
                                spriteBatch.Draw(upCone, cone, Color.Green);
                                player.Screechdirection = Direction.Up;
                                break;
                            case Direction.Down:
                                cone = new Rectangle(player.RectX - 10, player.RectY +8, 56, 56);  //makes the starting position of the wave
                                spriteBatch.Draw(downCone, cone, Color.Green);
                                player.Screechdirection = Direction.Down;
                                break;
                            case Direction.UpLeft:
                                cone = new Rectangle(player.RectX - 20, player.RectY - 13, 56, 56);  //makes the starting position of the wave
                                spriteBatch.Draw(upLeftCone, cone, Color.Green);
                                player.Screechdirection = Direction.UpLeft;
                                break;
                            case Direction.UpRight:
                                cone = new Rectangle(player.RectX - 5, player.RectY - 13, 56, 56);  //makes the starting position of the wave
                                spriteBatch.Draw(upRightCone, cone, Color.Green);
                                player.Screechdirection = Direction.UpRight;
                                break;
                            case Direction.DownLeft:
                                cone = new Rectangle(player.RectX - 20, player.RectY +2, 56, 56);  //makes the starting position of the wave
                                spriteBatch.Draw(downLeftCone, cone, Color.Green);
                                player.Screechdirection = Direction.DownLeft;
                                break;
                            case Direction.DownRight:
                                cone = new Rectangle(player.RectX - 5, player.RectY +2, 56, 56);  //makes the starting position of the wave
                                spriteBatch.Draw(downRightCone, cone, Color.Green);
                                player.Screechdirection = Direction.DownRight;
                                break;
                        }
                        player.Screech = false;
                    }
                    else if (player.Screeching == true)
                    {
                        switch (player.Screechdirection)  //switch statement draws echolocation wave wherever the player was facing when they screeched
                        {                       //wave will travel to end of the screen starting from edge of player's personal lightMask
                            //currently does not stop ever
                            case Direction.Right:
                                Rectangle temp = new Rectangle(cone.X + 1, cone.Y - 6, cone.Width + 13, cone.Height + 12);
                                cone = temp;
                                spriteBatch.Draw(rightCone, cone, Color.Green);
                                break;
                            case Direction.Left:
                                temp = new Rectangle(cone.X - 8, cone.Y - 4, cone.Width + 10, cone.Height + 8);
                                cone = temp;
                                spriteBatch.Draw(leftCone, cone, Color.Green);
                                break;
                            case Direction.Up:
                                temp = new Rectangle(cone.X - 5, cone.Y - 10, cone.Width + 10, cone.Height + 10);
                                cone = temp;
                                spriteBatch.Draw(upCone, cone, Color.Green);
                                break;
                            case Direction.Down:
                                temp = new Rectangle(cone.X - 6, cone.Y + 3, cone.Width + 12, cone.Height + 10);
                                cone = temp;
                                spriteBatch.Draw(downCone, cone, Color.Green);
                                break;
                            case Direction.UpLeft:
                                temp = new Rectangle(cone.X - 8, cone.Y - 8, cone.Width + 12, cone.Height + 12);
                                cone = temp;
                                spriteBatch.Draw(upLeftCone, cone, Color.Green);
                                break;
                            case Direction.UpRight:
                                temp = new Rectangle(cone.X + 2, cone.Y - 11, cone.Width + 14, cone.Height + 10);
                                cone = temp;
                                spriteBatch.Draw(upRightCone, cone, Color.Green);
                                break;
                            case Direction.DownLeft:
                                temp = new Rectangle(cone.X - 10, cone.Y + 1, cone.Width + 11, cone.Height + 11);
                                cone = temp;
                                spriteBatch.Draw(downLeftCone, cone, Color.Green);
                                break;
                            case Direction.DownRight:
                                temp = new Rectangle(cone.X + 2, cone.Y + 1, cone.Width + 11, cone.Height + 11);
                                cone = temp;
                                spriteBatch.Draw(downRightCone, cone, Color.Green);
                                break;
                        }
                    }

                    spriteBatch.End();


                    GraphicsDevice.SetRenderTarget(mainTarget);
                    GraphicsDevice.Clear(Color.SaddleBrown);
                    spriteBatch.Begin();

                    // TODO: Add your drawing code here


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
                    immobilesManager.IManagerDraw(spriteBatch);
                    gameObjectManager.GManagerDraw(spriteBatch);
                    spriteBatch.Draw(player.ObjTexture, player.ObjRectangle, Color.White);
                    enemyManager.EManagerDraw(spriteBatch);

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

                    spriteBatch.End();


                    GraphicsDevice.SetRenderTarget(null);
                    GraphicsDevice.Clear(Color.Black);

                    // If using a shader, ALWAYS use the Begin() overload that takes the Effect!
                    spriteBatch.Begin(0, null, null, null, null, lightingEffect);
                    lightingEffect.Parameters["lightMask"].SetValue(lightsTarget);
                    lightingEffect.CurrentTechnique.Passes[0].Apply();
                    spriteBatch.Draw(mainTarget, Vector2.Zero, Color.White);
                    spriteBatch.End();

                    //GraphicsDevice.SetRenderTarget(null);           //code does the lightmask's effects on sprites through multiplication
                    ////GraphicsDevice.Clear(Color.SaddleBrown);        //so anything completely black will remain black
                    //GraphicsDevice.Clear(Color.Blue);
                    //spriteBatch.Begin();//SpriteSortMode.Immediate, BlendState.AlphaBlend);
                    //lightingEffect.Parameters["lightMask"].SetValue(lightsTarget);
                    //lightingEffect.CurrentTechnique.Passes[0].Apply();
                    //spriteBatch.Draw(mainTarget, Vector2.Zero, Color.White);
                    //spriteBatch.End();

                    spriteBatch.Begin();
                    spriteBatch.DrawString(comicSans14, "Grid Size: " + grid.TileWidthCount + ", " + grid.TileHeightCount,
                    new Vector2(480, 220), Color.Orange);
                    spriteBatch.DrawString(comicSans14, "Direction: " + player.Facing,
                    new Vector2(480, 250), Color.Orange);
                    spriteBatch.DrawString(comicSans14, "Enemies: " + enemyManager.Count,
                    new Vector2(480, 280), Color.Orange);
                    spriteBatch.DrawString(comicSans14, "Position: " + player.PosX + ", " + player.PosY,
                    new Vector2(480, 310), Color.Orange);
                    spriteBatch.DrawString(comicSans14, "# of Deads: " + player.Hits,
                    new Vector2(480, 340), Color.Orange);
                    spriteBatch.DrawString(comicSans14, "1/2 square y: " + player.HalfY,
                    new Vector2(480, 370), Color.Orange);
                    spriteBatch.DrawString(comicSans14, "1/2 square x: " + player.HalfX,
                    new Vector2(480, 400), Color.Orange);
                    spriteBatch.DrawString(comicSans14, "Alpha V 0.12",
                    new Vector2(GraphicsDevice.Viewport.Width - 150, GraphicsDevice.Viewport.Height - 30), Color.Orange);
                    spriteBatch.End();
                    break;
                case GameState.menu:
                    spriteBatch.Begin();

                    this.IsMouseVisible = true;
                    spriteBatch.Draw(menuImage, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
                    spriteBatch.Draw(nameLogoImage, new Rectangle((GraphicsDevice.Viewport.Width / 2) - 100, 50, 250, 150), Color.White);

                    startButton.Draw(spriteBatch);
                    loadButton.Draw(spriteBatch);
                    optionButton.Draw(spriteBatch);

                    spriteBatch.End();
                    base.Draw(gameTime);
                    break;
            }




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
                                Enemy jim = new Enemy(enemyImage, gameObjectManager, new Point(j, i), grid, Direction.Down, SubSquares.TopLeft, true, 0, 0, true, 3, true);
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

