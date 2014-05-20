//Everyone
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

        Texture2D upRightCornerInvertedWallImage;
        Texture2D upLeftCornerInvertedWallImage;
        Texture2D downRightCornerInvertedWallImage;
        Texture2D downLeftCornerInvertedWallImage;

        Texture2D upTopWallImage;
        Texture2D upBottomWallImage;
        Texture2D downTopWallImage;
        Texture2D downBottomWallImage;

        Texture2D floorTileImage;
        Texture2D webImage;
        Texture2D webBloodImage;
        Texture2D boulderImage;
        Texture2D stagImage;
        Texture2D skullImage;

        Texture2D lightMask;
        Texture2D rightCone;
        Texture2D leftCone;
        Texture2D upCone;
        Texture2D downCone;
        Texture2D upRightCone;
        Texture2D upLeftCone;
        Texture2D downRightCone;
        Texture2D downLeftCone;

        Texture2D rightShriek;
        Texture2D leftShriek;
        Texture2D upShriek;
        Texture2D downShriek;
        Texture2D upRightShriek;
        Texture2D upLeftShriek;
        Texture2D downRightShriek;
        Texture2D downLeftShriek;

        Point shriekStart;

        Rectangle cone;

        Shriek shriek;

        RenderTarget2D lightsTarget;
        RenderTarget2D mainTarget;

        Effect lightingEffect;

        SpriteFont comicSans14;

        Player player;
        Enemy enemy;
        PartyMode PARTY;

        EnemyManager enemyManager;
        ImmobilesManager immobilesManager;
        GameObjectManager gameObjectManager;

        String[,] check;
        GameObject[,] checkMap;

        Dictionary<string, Texture2D> spriteDictionary;
        AnimationFarm playerAnimation;

        Level level11;
        Level level12;
        Level level13;
        Level level21;
        Level level22;
        Level level23;
        Level level31;
        Level level32;
        Level level33;


        Level[,] gameMap;

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

        Song tunes;

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
            shriek = null;
            playerAnimation = new AnimationFarm(playerImage, 0, 32, 32);
            player = new Player(playerImage, gameObjectManager, new Point(2, 2), grid, Direction.Right,
                SubSquares.TopLeft, true, 0, .1, true, 0);
            PARTY = new PartyMode(GraphicsDevice, immobilesManager, gameObjectManager, enemyManager, tunes);

            Mouse.WindowHandle = Window.Handle;
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

            downRightShriek = Content.Load<Texture2D>("Sprites/Shriek/downRightShriek");
            spriteDictionary.Add("downRightShriek", downRightShriek);

            downLeftShriek = Content.Load<Texture2D>("Sprites/Shriek/downLeftShriek");
            spriteDictionary.Add("downLeftShriek", downLeftShriek);

            downShriek = Content.Load<Texture2D>("Sprites/Shriek/downShriek");
            spriteDictionary.Add("downShriek", downShriek);

            upRightShriek = Content.Load<Texture2D>("Sprites/Shriek/upRightShriek");
            spriteDictionary.Add("upnRightShriek", upRightShriek);

            upLeftShriek = Content.Load<Texture2D>("Sprites/Shriek/upLeftShriek");
            spriteDictionary.Add("upLeftShriek", upLeftShriek);

            upShriek = Content.Load<Texture2D>("Sprites/Shriek/upShriek");
            spriteDictionary.Add("upShriek", upShriek);

            rightShriek = Content.Load<Texture2D>("Sprites/Shriek/rightShriek");
            spriteDictionary.Add("rightShriek", rightShriek);

            leftShriek = Content.Load<Texture2D>("Sprites/Shriek/leftShriek");
            spriteDictionary.Add("leftShriek", leftShriek);

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

            downLeftCornerInvertedWallImage = Content.Load<Texture2D>("Sprites/Corners/DownLeft_Corner_Inverted");
            spriteDictionary.Add("downLeftCornerInvertedWall", downLeftCornerInvertedWallImage);

            upLeftCornerInvertedWallImage = Content.Load<Texture2D>("Sprites/Corners/TopLeft_Corner_Inverted");
            spriteDictionary.Add("upLeftCornerInvertedWall", upLeftCornerInvertedWallImage);

            downRightCornerInvertedWallImage = Content.Load<Texture2D>("Sprites/Corners/DownRight_Corner_Inverted");
            spriteDictionary.Add("downRightCornerInvertedWall", downRightCornerInvertedWallImage);

            upRightCornerInvertedWallImage = Content.Load<Texture2D>("Sprites/Corners/TopRight_Corner_Inverted");
            spriteDictionary.Add("upRightCornerInvertedWall", upRightCornerInvertedWallImage);

            upTopWallImage = Content.Load<Texture2D>("Sprites/Walls/VerticalRightWall");
            spriteDictionary.Add("upTopWall", upTopWallImage);

            upBottomWallImage = Content.Load<Texture2D>("Sprites/Walls/VerticalRightWall");
            spriteDictionary.Add("upBottomWall", upBottomWallImage);

            downTopWallImage = Content.Load<Texture2D>("Sprites/Walls/VerticalRightWall");
            spriteDictionary.Add("downTopWall", downTopWallImage);

            downBottomWallImage = Content.Load<Texture2D>("Sprites/Walls/VerticalRightWall");
            spriteDictionary.Add("downBottomWall", downBottomWallImage);

            floorTileImage = Content.Load<Texture2D>("Sprites/Floors/FloorTile");
            spriteDictionary.Add("floorTile", floorTileImage);

            webImage = Content.Load<Texture2D>("Sprites/Interactables/web");
            spriteDictionary.Add("spiderWeb", webImage);

            //webBloodImage = Content.Load<Texture2D>("Sprites/Interactables/web_bloody");
            //spriteDictionary.Add("spiderWeb", webBloodImage);

            boulderImage = Content.Load<Texture2D>("Sprites/Interactables/boulder");
            spriteDictionary.Add("boulder", boulderImage);

            stagImage = Content.Load<Texture2D>("Sprites/Interactables/stalagmite");
            spriteDictionary.Add("stalagmite", stagImage);

            skullImage = Content.Load<Texture2D>("Sprites/Interactables/skull");
            spriteDictionary.Add("skull", skullImage);


            player.ObjTexture = playerImage;

            level11 = new Level("Content/Levels/level11.txt", spriteDictionary, grid, enemyManager, immobilesManager, gameObjectManager);
            level12 = new Level("Content/Levels/level12.txt", spriteDictionary, grid, enemyManager, immobilesManager, gameObjectManager);
            level13 = new Level("Content/Levels/level13.txt", spriteDictionary, grid, enemyManager, immobilesManager, gameObjectManager);
            level21 = new Level("Content/Levels/level21.txt", spriteDictionary, grid, enemyManager, immobilesManager, gameObjectManager);
            level22 = new Level("Content/Levels/level22.txt", spriteDictionary, grid, enemyManager, immobilesManager, gameObjectManager);
            level23 = new Level("Content/Levels/level23.txt", spriteDictionary, grid, enemyManager, immobilesManager, gameObjectManager);
            level31 = new Level("Content/Levels/level31.txt", spriteDictionary, grid, enemyManager, immobilesManager, gameObjectManager);
            level32 = new Level("Content/Levels/level32.txt", spriteDictionary, grid, enemyManager, immobilesManager, gameObjectManager);
            level33 = new Level("Content/Levels/level33.txt", spriteDictionary, grid, enemyManager, immobilesManager, gameObjectManager);

            gameMap = new Level[3, 3] { { level11, level12, level13 }, { level21, level22, level23 }, { level31, level32, level33 } };

            /*for (int i = 0; i < gameMap.GetLength(0); i++)
            {
                for (int j = 0; j < gameMap.GetLength(1); j++)
                {
                    if (j + 1 < gameMap.GetLength(1))
                    {
                        gameMap[i, j].DownNeighbor = gameMap[i, j + 1];
                    }
                    if (j - 1 > 0)
                    {
                        gameMap[i, j].UpNeighbor = gameMap[i, j - 1];
                    }
                    if (i - 1 > 0)
                    {
                        gameMap[i, j].LeftNeighbor = gameMap[i - 1, j];
                    }
                    if (i + 1 < gameMap.GetLength(0))
                    {
                        gameMap[i, j].RightNeighbor = gameMap[i + 1, j];
                    }
                }
            }*/

            level11.SpawnPlayerX = 2;
            level11.SpawnPlayerY = 2;

            level11.RightNeighbor = level12;
            level11.DownNeighbor = level21;

            level12.LeftNeighbor = level11;
            level12.RightNeighbor = level13;
            level12.DownNeighbor = level22;

            level13.LeftNeighbor = level12;
            level13.DownNeighbor = level23;

            level21.RightNeighbor = level22;
            level21.UpNeighbor = level11;
            level21.DownNeighbor = level31;

            level22.LeftNeighbor = level21;
            level22.RightNeighbor = level23;
            level22.UpNeighbor = level12;
            level22.DownNeighbor = level32;

            level23.LeftNeighbor = level22;
            level23.UpNeighbor = level13;
            level23.DownNeighbor = level33;

            level31.UpNeighbor = level21;
            level31.RightNeighbor = level32;

            level32.UpNeighbor = level22;
            level32.RightNeighbor = level33;
            level32.LeftNeighbor = level31;

            level33.UpNeighbor = level23;
            level33.LeftNeighbor = level32;

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

            //"I Just Hold The Mirror" - Mark Castle
            tunes = Content.Load<Song>("danceMusic");
            PARTY.partyTime(tunes);
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

            if (keyboardState.IsKeyDown(Keys.P))
            {
                this.Window.Title = "Party Game";
                gameState = GameState.partyMode;
                player.HalfX = !player.HalfX;
                player.HalfY = !player.HalfY;
            }

            // TODO: Add your update logic here
            switch (gameState)
            {
                case GameState.game:
                    //too much code in Game1 is super bad, but it'll be sorted out later
                    if (player.Shriek == true)
                    {
                        shriekStart = new Point(player.PosX, player.PosY);
                        switch (player.Facing)  //switch statement draws shriek wherever the player is facing
                        {
                            case Direction.Right:
                                shriek = new Shriek(rightShriek, gameObjectManager, shriekStart, grid, Direction.Right, player.MiniSquare, true, 0, .3, true, 0, Direction.Right);
                                break;
                            case Direction.Left:
                                shriek = new Shriek(leftShriek, gameObjectManager, shriekStart, grid, Direction.Right, player.MiniSquare, true, 0, .05, true, 0, Direction.Left);
                                break;
                            case Direction.Up:
                                shriek = new Shriek(upShriek, gameObjectManager, shriekStart, grid, Direction.Right, player.MiniSquare, true, 0, .05, true, 0, Direction.Up);
                                break;
                            case Direction.Down:
                                shriek = new Shriek(downShriek, gameObjectManager, shriekStart, grid, Direction.Right, player.MiniSquare, true, 0, .05, true, 0, Direction.Down);
                                break;
                            case Direction.UpLeft:
                                shriek = new Shriek(upLeftShriek, gameObjectManager, shriekStart, grid, Direction.Right, player.MiniSquare, true, 0, .05, true, 0, Direction.UpLeft);
                                break;
                            case Direction.UpRight:
                                shriek = new Shriek(upRightShriek, gameObjectManager, shriekStart, grid, Direction.Right, player.MiniSquare, true, 0, .05, true, 0, Direction.UpRight);
                                break;
                            case Direction.DownLeft:
                                shriek = new Shriek(downLeftShriek, gameObjectManager, shriekStart, grid, Direction.Right, player.MiniSquare, true, 0, .05, true, 0, Direction.DownLeft);
                                break;
                            case Direction.DownRight:
                                shriek = new Shriek(downRightShriek, gameObjectManager, shriekStart, grid, Direction.Right, player.MiniSquare, true, 0, .05, true, 0, Direction.DownRight);
                                break;
                        }
                        gameObjectManager.AddGameObject(shriek);
                        player.ScreechTime = 250; //cooldown length
                    }
                    if (player.Shrieking == true && shriek != null)
                    {
                        shriek.Update(gameTime);
                        if (player.ScreechTime > 0)
                            player.ScreechTime--; //cooldown
                    }

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
                        LoadLevel(player.CurrentLevel);
                    }
                    if (player.HitBottom)
                    {
                        int spawnX = player.PosX;
                        int spawnY = 1;

                        player.CurrentLevel.DownNeighbor.SpawnPlayerX = spawnX;
                        player.CurrentLevel.DownNeighbor.SpawnPlayerY = spawnY;
                        LoadLevel(player.CurrentLevel.DownNeighbor);
                        player.Facing = Direction.Down;
                    }
                    if (player.HitTop)
                    {
                        int spawnX = player.PosX;
                        int spawnY = 13;
                        player.CurrentLevel.UpNeighbor.SpawnPlayerX = spawnX;
                        player.CurrentLevel.UpNeighbor.SpawnPlayerY = spawnY;
                        LoadLevel(player.CurrentLevel.UpNeighbor);
                        player.Facing = Direction.Up;
                    }
                    if (player.HitLeft)
                    {
                        int spawnX = 23;
                        int spawnY = player.PosY;
                        player.CurrentLevel.LeftNeighbor.SpawnPlayerX = spawnX;
                        player.CurrentLevel.LeftNeighbor.SpawnPlayerY = spawnY;
                        LoadLevel(player.CurrentLevel.LeftNeighbor);
                        player.Facing = Direction.Left;
                    }
                    if (player.HitRight)
                    {
                        int spawnX = 1;
                        int spawnY = player.PosY;
                        player.CurrentLevel.RightNeighbor.SpawnPlayerX = spawnX;
                        player.CurrentLevel.RightNeighbor.SpawnPlayerY = spawnY;
                        LoadLevel(player.CurrentLevel.RightNeighbor);
                        player.Facing = Direction.Right;
                    }

                    if (player.HitFinish)
                    {
                        level22.SpawnPlayerX = 2;
                        level22.SpawnPlayerY = 4;
                        LoadLevel(level22);
                        gameState = GameState.partyMode;
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
                            gameState = GameState.game;
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
                            LoadLevel(level11);
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

                                if (currentLevel.Equals("level11"))
                                {
                                    level11.HasStarted = true;
                                    level11.SavedPlayerX = reader.ReadInt32();
                                    level11.SavedPlayerY = reader.ReadInt32();
                                    LoadLevel(level11);
                                }
                                if (currentLevel.Equals("level12"))
                                {
                                    level12.HasStarted = true;
                                    level12.SavedPlayerX = reader.ReadInt32();
                                    level12.SavedPlayerY = reader.ReadInt32();
                                    LoadLevel(level12);
                                    
                                }
                                if (currentLevel.Equals("level13"))
                                {
                                    level13.HasStarted = true;
                                    level13.SavedPlayerX = reader.ReadInt32();
                                    level13.SavedPlayerY = reader.ReadInt32();
                                    LoadLevel(level13);
                                }
                                if (currentLevel.Equals("level21"))
                                {
                                    level21.HasStarted = true;
                                    level21.SavedPlayerX = reader.ReadInt32();
                                    level21.SavedPlayerY = reader.ReadInt32();
                                    LoadLevel(level21);
                                    
                                }
                                if (currentLevel.Equals("level22"))
                                {
                                    level22.HasStarted = true;
                                    level22.SavedPlayerX = reader.ReadInt32();
                                    level22.SavedPlayerY = reader.ReadInt32();
                                    LoadLevel(level22);
                                    
                                }
                                if (currentLevel.Equals("level23"))
                                {
                                    level23.HasStarted = true;
                                    level23.SavedPlayerX = reader.ReadInt32();
                                    level23.SavedPlayerY = reader.ReadInt32();
                                    LoadLevel(level23);
                                    
                                }
                                if (currentLevel.Equals("level31"))
                                {
                                    level31.HasStarted = true;
                                    level31.SavedPlayerX = reader.ReadInt32();
                                    level31.SavedPlayerY = reader.ReadInt32();
                                    LoadLevel(level31);
                                    
                                }
                                if (currentLevel.Equals("level32"))
                                {
                                    level32.HasStarted = true;
                                    level32.SavedPlayerX = reader.ReadInt32();
                                    level32.SavedPlayerY = reader.ReadInt32();
                                    LoadLevel(level32);
                                    
                                }
                                if (currentLevel.Equals("level33"))
                                {
                                    level33.HasStarted = true;
                                    level33.SavedPlayerX = reader.ReadInt32();
                                    level33.SavedPlayerY = reader.ReadInt32();
                                    LoadLevel(level33);
                                }
                            }
                        }
                    }

                    base.Update(gameTime);
                    break;
                case GameState.partyMode:
                    player.PartyMode();
                    player.PlayerUpdate();
                    player.Speed += gameTime.ElapsedGameTime.TotalSeconds;
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
                    for (int i = 0; i < player.CurrentLevel.LevelObjectArray.GetLength(0); i++)
                    {
                        for (int j = 0; j < player.CurrentLevel.LevelObjectArray.GetLength(1); j++)
                        {
                            if (player.CurrentLevel.LevelObjectArray[i, j] is LightSource)
                            {
                                Vector2 lightSource = new Vector2(player.CurrentLevel.LevelObjectArray[i, j].RectX - 45, player.CurrentLevel.LevelObjectArray[i, j].RectY - 50);
                                spriteBatch.Draw(lightMask, lightSource, Color.White);
                            }
                        }
                    }
                    
                    /*if (currentLevel == "level11")
                    {
                        for (int i = 0; i < level11.LevelObjectArray.GetLength(0); i++)
                        {
                            for (int j = 0; j < level11.LevelObjectArray.GetLength(1); j++)
                            {
                                if (level1.LevelObjectArray[i, j] is LightSource)
                                {
                                    Vector2 lightSource = new Vector2(level11.LevelObjectArray[i, j].RectX - 45, level11.LevelObjectArray[i, j].RectY - 50);
                                    spriteBatch.Draw(lightMask, lightSource, Color.White);
                                }
                            }
                        }
                    }
                    else if (currentLevel == "level2")
                    {
                        for (int i = 0; i < level2.LevelObjectArray.GetLength(0); i++)
                        {
                            for (int j = 0; j < level2.LevelObjectArray.GetLength(1); j++)
                            {
                                if (level2.LevelObjectArray[i, j] is LightSource)
                                {
                                    Vector2 lightSource = new Vector2(level2.LevelObjectArray[i, j].RectX - 45, level2.LevelObjectArray[i, j].RectY - 50);
                                    spriteBatch.Draw(lightMask, lightSource, Color.White);
                                }
                            }
                        }
                    }*/
                    
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
                    GraphicsDevice.Clear(Color.Black);
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

                    if (player.Shrieking == true && shriek != null) //draws shriek
                    {
                        shriek.Draw(spriteBatch);
                    }
                    playerAnimation.PlayerFlyingAnimation(player.Facing, gameTime);

                    //if (player.Facing == Direction.Down || player.Facing == Direction.Up || player.Facing == Direction.Left || player.Facing == Direction.Right)
                    //{
                        spriteBatch.Draw(player.ObjTexture, new Vector2((player.RectX + playerAnimation.Origin.X), (player.RectY + playerAnimation.Origin.Y)), playerAnimation.DrawRectangle, Color.White, 0f, playerAnimation.Origin, 1, SpriteEffects.None, 0);
                    /*}
                    else if (player.Facing == Direction.DownLeft || player.Facing == Direction.DownRight || player.Facing == Direction.UpLeft || player.Facing == Direction.UpRight)
                    {
                        spriteBatch.Draw(player.ObjTexture,
                            new Vector2((player.RectX + playerAnimation.Origin.X),
                                (player.RectY + playerAnimation.Origin.Y)), 
                                playerAnimation.DrawRectangle, 
                                Color.White,  
                                 45%(MathHelper.Pi*2),
                                playerAnimation.Origin, 
                                1, 
                                SpriteEffects.None, 
                                0);
                    }*/
                    enemyManager.EManagerDraw(spriteBatch);
                    immobilesManager.IManagerDrawFront(spriteBatch);
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
                    spriteBatch.DrawString(comicSans14, "Current level: " + currentLevel,
                    new Vector2(480, 430), Color.Orange);
                    spriteBatch.DrawString(comicSans14, "Beta V 0.2P",
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

                case GameState.warning:
                    spriteBatch.Begin();
                    spriteBatch.DrawString(comicSans14, "Warning!",
                    new Vector2(320, 60), Color.Red);
                    spriteBatch.DrawString(comicSans14, "This area contains flashing lights",
                    new Vector2(120, 140), Color.White);
                    spriteBatch.DrawString(comicSans14, "and cool beats.",
                    new Vector2(260, 180), Color.White);
                    spriteBatch.DrawString(comicSans14, "Do you still wish to continue?",
                    new Vector2(160, 220), Color.White);

                    continueButton.X = 150;
                    continueButton.Y = 330;
                    continueButton.Draw(spriteBatch);
                    quitButton.Draw(spriteBatch);
                    spriteBatch.End();
                    break;

                case GameState.partyMode:
                    PARTY.BeerGoggles(spriteBatch, player);
                    break;

            }
            base.Draw(gameTime);
        }

        private void LoadLevel(Level level)
        {
            if (level.Equals(level11))
            {
                currentLevel = "level11";
            }
            else if (level.Equals(level12))
            {
                currentLevel = "level12";
            }
            else if (level.Equals(level13))
            {
                currentLevel = "level13";
            }
            else if (level.Equals(level21))
            {
                currentLevel = "level21";
            }
            else if (level.Equals(level22))
            {
                currentLevel = "level22";
            }
            else if (level.Equals(level23))
            {
                currentLevel = "level23";
            }
            else if (level.Equals(level31))
            {
                currentLevel = "level31";
            }
            else if (level.Equals(level32))
            {
                currentLevel = "level32";
            }
            else if (level.Equals(level33))
            {
                currentLevel = "level33";
            }

            check = level.loadLevel();
            checkMap = level.setupLevel();
            player = gameObjectManager.Player;
            player.CurrentLevel = level;
            gameState = GameState.game;
        }
    }
}