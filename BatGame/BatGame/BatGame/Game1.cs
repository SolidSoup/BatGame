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
        Texture2D playerIdle;
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
        Texture2D webImage;
        Texture2D boulderImage;
        Texture2D stagImage;

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

        Level level1;
        Level level2;
        Level level3;
        String currentLevel;

        //Things that will eventually be moved to state manager
        GameState gameState;
        Texture2D menuImage;
        Texture2D startButtonImage;
        Texture2D quitButtonImage;
        Texture2D continueButtonImage;
        Texture2D menuButtonImage;
        Texture2D saveButtonImage;
        Texture2D loadButtonImage;
        Texture2D nameLogoImage;
        Texture2D pauseImage;
        Button startButton;
        Button quitButton;
        Button loadButton;
        Button continueButton;
        Button saveButton;
        Button menuButton;
        MouseState lastMouseState;
        MouseState currentMouseState;
        KeyboardState keyboardState;

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
            playerAnimation = new AnimationFarm(playerImage, 0, 32, 32);
            player = new Player(playerImage, gameObjectManager, new Point(2, 2), grid, Direction.Right,
                SubSquares.TopLeft, true, 0, .1, true, 0);

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

            playerImage = Content.Load<Texture2D>("Sprites/Bat_Sprites/Idle_Bat");
            spriteDictionary.Add("playerImage", playerImage);

            playerIdle = Content.Load<Texture2D>("Sprites/Bat_Sprites/Idle_Bat");
            spriteDictionary.Add("playerIdle", playerIdle);

            batUpDownImage = Content.Load<Texture2D>("Sprites/Bat_Sprites/Bat_Fly_Up");
            spriteDictionary.Add("BatUpDownImage", batUpDownImage);

            batSideImage = Content.Load<Texture2D>("Sprites/Bat_Sprites/Bat_Fly_Left");
            spriteDictionary.Add("batSideImage", batSideImage);

            enemyImage = Content.Load<Texture2D>("Sprites/Enemy_Sprites/enemy");
            spriteDictionary.Add("enemyImage", enemyImage);

            verticalWallImage = Content.Load<Texture2D>("Sprites/Walls/Vertical_Wall");
            spriteDictionary.Add("verticalWall", verticalWallImage);

            verticalLeftWallImage = Content.Load<Texture2D>("Sprites/Walls/VerticalRightWall");
            spriteDictionary.Add("verticalLeftWall", verticalLeftWallImage);

            verticalRightWallImage = Content.Load<Texture2D>("Sprites/Walls/VerticalLeftWall");
            spriteDictionary.Add("verticalRightWall", verticalRightWallImage);

            horizontalWallImage = Content.Load<Texture2D>("Sprites/Walls/Horizontal_Wall");
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

            webImage = Content.Load<Texture2D>("Sprites/Interactables/web");
            spriteDictionary.Add("spiderWeb", webImage);

            boulderImage = Content.Load<Texture2D>("Sprites/Interactables/boulder");
            spriteDictionary.Add("boulder", boulderImage);

            stagImage = Content.Load<Texture2D>("Sprites/Interactables/stalagmite");
            spriteDictionary.Add("stalagmite", stagImage);


            player.ObjTexture = playerImage;

            // LoadMap("Content/level1.txt");
            level1 = new Level("Content/Levels/level1.txt", spriteDictionary, grid, enemyManager, immobilesManager, gameObjectManager);
            //level2 = new Level("Content/Levels/level2.txt", spriteDictionary, grid, enemyManager, immobilesManager, gameObjectManager);


            //check = level1.loadLevel();
            //checkMap = level1.setupLevel(spriteDictionary, grid, enemyManager, immobilesManager, gameObjectManager);
            //player = gameObjectManager.Player;

            //Images and buttons for menu and pause screens
            menuImage = Content.Load<Texture2D>("Sprites/Menu_Sprites/menu");
            nameLogoImage = Content.Load<Texture2D>("Sprites/Menu_Sprites/nameLogo");
            startButtonImage = Content.Load<Texture2D>("Sprites/Menu_Sprites/startButton");
            loadButtonImage = Content.Load<Texture2D>("Sprites/Menu_Sprites/loadButton");
            quitButtonImage = Content.Load<Texture2D>("Sprites/Menu_Sprites/quitButton");
            continueButtonImage = Content.Load<Texture2D>("Sprites/Menu_Sprites/continueButton");
            saveButtonImage = Content.Load<Texture2D>("Sprites/Menu_Sprites/saveButton");
            menuButtonImage = Content.Load<Texture2D>("Sprites/Menu_Sprites/menuButton");
            pauseImage = Content.Load<Texture2D>("Sprites/Menu_Sprites/pause");

            int height = 2 * GraphicsDevice.Viewport.Height / 3;
            startButton = new Button((GraphicsDevice.Viewport.Width / 3) - 90, height, 120, 90, startButtonImage);
            loadButton = new Button((GraphicsDevice.Viewport.Width / 2) - 40, height, 120, 90, loadButtonImage);
            quitButton = new Button(2 * GraphicsDevice.Viewport.Width / 3, height, 120, 90, quitButtonImage);

            height = 100;
            menuButton = new Button((GraphicsDevice.Viewport.Width / 2) - 60, height * 3, 140, 90, menuButtonImage);
            continueButton = new Button((GraphicsDevice.Viewport.Width / 2) - 60, height, 140, 90, continueButtonImage);
            saveButton = new Button((GraphicsDevice.Viewport.Width / 2) - 60, height * 2, 140, 90, saveButtonImage);
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
                    immobilesManager.IManagerUpdate();

                    keyboardState = Keyboard.GetState();
                    if (keyboardState.IsKeyDown(Keys.Escape))
                    {
                        gameState = GameState.pause;
                    }

                    if (player.Hits > 0)
                    {
                        if (currentLevel.Equals("level1"))
                        {
                            gameState = GameState.level1;
                        }
                        if (currentLevel.Equals("level2"))
                        {
                            gameState = GameState.level2;
                        }
                    }
                    playerAnimation.AnimationUpdate(gameTime, player.Facing);
                    base.Update(gameTime);
                    break;
                case GameState.pause:
                    continueButton.Selected = false;
                    saveButton.Selected = false;
                    menuButton.Selected = false;

                    lastMouseState = currentMouseState;
                    currentMouseState = Mouse.GetState();
                    Point pos = new Point(currentMouseState.X, currentMouseState.Y);

                    if (continueButton.Rect.Contains(pos))
                    {
                        continueButton.Selected = true;

                        if (lastMouseState.LeftButton == ButtonState.Released && currentMouseState.LeftButton == ButtonState.Pressed)
                        {
                            gameState = GameState.game;
                        }
                    }
                    if (menuButton.Rect.Contains(pos))
                    {
                        menuButton.Selected = true;

                        if (lastMouseState.LeftButton == ButtonState.Released && currentMouseState.LeftButton == ButtonState.Pressed)
                        {
                            gameState = GameState.menu;
                        }
                    }
                    if (saveButton.Rect.Contains(pos))
                    {
                        saveButton.Selected = true;

                        if (lastMouseState.LeftButton == ButtonState.Released && currentMouseState.LeftButton == ButtonState.Pressed)
                        {
                            using (BinaryWriter writer = new BinaryWriter(File.Open("saveFile", FileMode.Create)))
                            {
                                writer.Write(currentLevel);
                                writer.Write(player.PosX);
                                writer.Write(player.PosY);
                            }
                        }
                    }

                    base.Update(gameTime);
                    break;
                case GameState.menu:
                    startButton.Selected = false;
                    quitButton.Selected = false;
                    loadButton.Selected = false;

                    lastMouseState = currentMouseState;
                    currentMouseState = Mouse.GetState();
                    Point pos2 = new Point(currentMouseState.X, currentMouseState.Y);

                    if (startButton.Rect.Contains(pos2))
                    {
                        startButton.Selected = true;

                        if (lastMouseState.LeftButton == ButtonState.Released && currentMouseState.LeftButton == ButtonState.Pressed)
                        {
                            gameState = GameState.level1;
                        }
                    }
                    if (quitButton.Rect.Contains(pos2))
                    {
                        quitButton.Selected = true;

                        if (lastMouseState.LeftButton == ButtonState.Released && currentMouseState.LeftButton == ButtonState.Pressed)
                        {
                            this.Exit();
                        }
                    }
                    if (loadButton.Rect.Contains(pos2))
                    {
                        loadButton.Selected = true;

                        if (lastMouseState.LeftButton == ButtonState.Released && currentMouseState.LeftButton == ButtonState.Pressed)
                        {
                            using (BinaryReader reader = new BinaryReader(File.Open("saveFile", FileMode.Open)))
                            {
                                currentLevel = reader.ReadString();
                                if (currentLevel.Equals("level1"))
                                {
                                    gameState = GameState.level1;
                                    level1.SpawnPlayerX = reader.ReadInt32();
                                    level1.SpawnPlayerY = reader.ReadInt32();
                                    level1.HasStarted = true;
                                }
                                if (currentLevel.Equals("level2"))
                                {
                                    gameState = GameState.level2;
                                    level2.SpawnPlayerX = reader.ReadInt32();
                                    level2.SpawnPlayerY = reader.ReadInt32();
                                    level2.HasStarted = true;
                                }
                            }
                        }
                    }

                    base.Update(gameTime);
                    break;
                case GameState.level1:
                    check = level1.loadLevel();
                    checkMap = level1.setupLevel();
                    player = gameObjectManager.Player;
                    gameState = GameState.game;
                    currentLevel = "level1";
                    break;
                case GameState.level2:
                    check = level2.loadLevel();
                    checkMap = level2.setupLevel();

                    player = gameObjectManager.Player;
                    gameState = GameState.game;
                    currentLevel = "level2";
                    break;
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

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

                        switch (player.Facing)  //switch statement draws echolocation wave wherever the player is facing
                        {
                            case Direction.Right:
                                cone = new Rectangle(player.RectX, player.RectY - 13, 56, 56);  //makes the starting position of the wave
                                spriteBatch.Draw(rightCone, cone, Color.Green);
                                player.Screechdirection = Direction.Right;
                                break;
                            case Direction.Left:
                                cone = new Rectangle(player.RectX - 60, player.RectY - 13, 56, 56);  //makes the starting position of the wave
                                spriteBatch.Draw(leftCone, cone, Color.Green);
                                player.Screechdirection = Direction.Left;
                                break;
                            case Direction.Up:
                                cone = new Rectangle(player.RectX - 10, player.RectY - 35, 56, 56);  //makes the starting position of the wave
                                spriteBatch.Draw(upCone, cone, Color.Green);
                                player.Screechdirection = Direction.Up;
                                break;
                            case Direction.Down:
                                cone = new Rectangle(player.RectX - 8, player.RectY + 8, 56, 56);  //makes the starting position of the wave
                                spriteBatch.Draw(downCone, cone, Color.Green);
                                player.Screechdirection = Direction.Down;
                                break;
                            case Direction.UpLeft:
                                cone = new Rectangle(player.RectX - 18, player.RectY - 20, 56, 56);  //makes the starting position of the wave
                                spriteBatch.Draw(upLeftCone, cone, Color.Green);
                                player.Screechdirection = Direction.UpLeft;
                                break;
                            case Direction.UpRight:
                                cone = new Rectangle(player.RectX - 5, player.RectY - 13, 56, 56);  //makes the starting position of the wave
                                spriteBatch.Draw(upRightCone, cone, Color.Green);
                                player.Screechdirection = Direction.UpRight;
                                break;
                            case Direction.DownLeft:
                                cone = new Rectangle(player.RectX - 25, player.RectY, 56, 56);  //makes the starting position of the wave
                                spriteBatch.Draw(downLeftCone, cone, Color.Green);
                                player.Screechdirection = Direction.DownLeft;
                                break;
                            case Direction.DownRight:
                                cone = new Rectangle(player.RectX - 5, player.RectY + 2, 56, 56);  //makes the starting position of the wave
                                spriteBatch.Draw(downRightCone, cone, Color.Green);
                                player.Screechdirection = Direction.DownRight;
                                break;
                        }
                        player.Screech = false;
                        player.ScreechTime = 150; //screech cooldown of 150 steps
                        player.Screeching = true;
                    }
                    else if (player.Screeching == true)
                    {
                        switch (player.Screechdirection)  //switch statement draws echolocation wave wherever the player was facing when they screeched
                        {                       //wave will travel to end of the screen starting from edge of player's personal lightMask
                            //currently does not stop ever
                            case Direction.Right:
                                Rectangle temp = new Rectangle(cone.X + 5, cone.Y - 6, cone.Width + 13, cone.Height + 12);
                                cone = temp;
                                spriteBatch.Draw(rightCone, cone, Color.Green);
                                break;
                            case Direction.Left:
                                temp = new Rectangle(cone.X - 16, cone.Y - 4, cone.Width + 10, cone.Height + 8);
                                cone = temp;
                                spriteBatch.Draw(leftCone, cone, Color.Green);
                                break;
                            case Direction.Up:
                                temp = new Rectangle(cone.X - 5, cone.Y - 11, cone.Width + 10, cone.Height + 10);
                                cone = temp;
                                spriteBatch.Draw(upCone, cone, Color.Green);
                                break;
                            case Direction.Down:
                                temp = new Rectangle(cone.X - 6, cone.Y + 3, cone.Width + 12, cone.Height + 10);
                                cone = temp;
                                spriteBatch.Draw(downCone, cone, Color.Green);
                                break;
                            case Direction.UpLeft:
                                temp = new Rectangle(cone.X - 12, cone.Y - 10, cone.Width + 10, cone.Height + 10);
                                cone = temp;
                                spriteBatch.Draw(upLeftCone, cone, Color.Green);
                                break;
                            case Direction.UpRight:
                                temp = new Rectangle(cone.X + 2, cone.Y - 11, cone.Width + 12, cone.Height + 10);
                                cone = temp;
                                spriteBatch.Draw(upRightCone, cone, Color.Green);
                                break;
                            case Direction.DownLeft:
                                temp = new Rectangle(cone.X - 14, cone.Y + 1, cone.Width + 11, cone.Height + 11);
                                cone = temp;
                                spriteBatch.Draw(downLeftCone, cone, Color.Green);
                                break;
                            case Direction.DownRight:
                                temp = new Rectangle(cone.X + 5, cone.Y + 2, cone.Width + 11, cone.Height + 11);
                                cone = temp;
                                spriteBatch.Draw(downRightCone, cone, Color.Green);
                                break;
                        }
                        if (player.ScreechTime > 0)
                            player.ScreechTime--; //cooldown for echolocation decreasing
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
                    immobilesManager.IManagerDrawBack(spriteBatch);
                    //gameObjectManager.GManagerDraw(spriteBatch);
                    //spriteBatch.Draw(player.ObjTexture, player.ObjRectangle, Color.White);
                    enemyManager.EManagerDraw(spriteBatch);
                    immobilesManager.IManagerDrawFront(spriteBatch);

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
                    spriteBatch.DrawString(comicSans14, "Alpha V 0.15",
                    new Vector2(GraphicsDevice.Viewport.Width - 150, GraphicsDevice.Viewport.Height - 30), Color.Orange);
                    
                    playerAnimation.PlayerFlyingAnimation(playerAnimation.FlyDirection, gameTime);
                    spriteBatch.Draw(player.ObjTexture, new Vector2((player.RectX+playerAnimation.Origin.X), (player.RectY+playerAnimation.Origin.Y)), playerAnimation.DrawRectangle, Color.White, 0f, playerAnimation.Origin, 1, SpriteEffects.None, 0);
                    
                    
                    spriteBatch.End();
                    break;
                case GameState.menu:
                    spriteBatch.Begin();

                    this.IsMouseVisible = true;
                    spriteBatch.Draw(menuImage, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
                    spriteBatch.Draw(nameLogoImage, new Rectangle((GraphicsDevice.Viewport.Width / 2) - 100, 50, 250, 150), Color.White);

                    startButton.Draw(spriteBatch);
                    loadButton.Draw(spriteBatch);
                    quitButton.Draw(spriteBatch);

                    spriteBatch.End();
                    base.Draw(gameTime);
                    break;

                case GameState.pause:
                    spriteBatch.Begin();

                    this.IsMouseVisible = true;
                    spriteBatch.Draw(pauseImage, new Rectangle((GraphicsDevice.Viewport.Width / 2) - 250, 0, 2 * GraphicsDevice.Viewport.Width / 3, GraphicsDevice.Viewport.Height), Color.White);

                    continueButton.Draw(spriteBatch);
                    menuButton.Draw(spriteBatch);
                    saveButton.Draw(spriteBatch);

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
