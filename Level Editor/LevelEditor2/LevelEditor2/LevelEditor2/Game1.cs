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

namespace LevelEditor2
{
    public enum TileType
    {
        topLeftCornerIn,
        topRightCornerIn,
        bottomLeftCornerIn,
        bottomRightCornerIn,
        topLeftCornerOut,
        topRightCornerOut,
        bottomLeftCornerOut,
        bottomRightCornerOut,
        floor,
        leftWall,
        rightWall,
        topWall,
        bottomWall,
        enemy,
        sleepingenemy,
        spiderenemy,
        player,
        blank,
        lightsource,
        skullhidingspot,
        spiderweb,
        stalagmites,
        boulder,
        diagonaldowntop,
        diagonaldownbottom,
        diagonaluptop,
        diagonalupbottom,
        exitend,
        exitLeft,
        exitRight,
        exitUp,
        exitDown,
        endgrid
    }
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Tile[,] tileArray;
        List<Tile> tiles;
        Texture2D tileTexture;
        List<Option> options;

        Texture2D floorTexture;
        Texture2D playerTexture;
        Texture2D enemyTexture;
        Texture2D sleepingenemyTexture;
        Texture2D spiderenemyTexture;
        Texture2D leftTexture;
        Texture2D rightTexture;
        Texture2D topTexture;
        Texture2D bottomTexture;
        Texture2D bottomLeftInTexture;
        Texture2D bottomRightInTexture;
        Texture2D topLeftInTexture;
        Texture2D topRightInTexture;
        Texture2D bottomLeftOutTexture;
        Texture2D bottomRightOutTexture;
        Texture2D topLeftOutTexture;
        Texture2D topRightOutTexture;
        Texture2D LightSource;
        Texture2D Skull;
        Texture2D DiagonalDownBottom;
        Texture2D DiagonalDownTop;
        Texture2D DiagonalUpBottom;
        Texture2D DiagonalUpTop;
        Texture2D SpiderWeb;
        Texture2D Stalagmites;
        Texture2D Boulder;
        Texture2D LevelExit;
        Texture2D LevelExitLeft;
        Texture2D LevelExitRight;
        Texture2D LevelExitUp;
        Texture2D LevelExitDown;
        Texture2D endgrid;
        SpriteFont font;

        MouseState lastMouseState;
        MouseState currentMouseState;
        KeyboardState lastKBState;
        KeyboardState currentKBState;

        TileType selectedType;
        Texture2D selectedTexture;

        string filename;
        bool saveEnabled = false;
        bool deleteFirst;
        bool loadEnabled = false;
        bool loadFailed = false;
        string errorMessage;

        public Game1()
        {
            
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 790;
            graphics.PreferredBackBufferWidth = 1520;

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

            base.Initialize();
            this.IsMouseVisible = true;

            selectedType = TileType.blank;
            selectedTexture = tileTexture;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            font = Content.Load<SpriteFont>("SpriteFont1");
            tileTexture = Content.Load<Texture2D>("tile");
            floorTexture = Content.Load<Texture2D>("FloorTile");
            playerTexture = Content.Load<Texture2D>("player");
            enemyTexture = Content.Load<Texture2D>("enemy");
            sleepingenemyTexture = Content.Load<Texture2D>("sleepingenemy");
            spiderenemyTexture = Content.Load<Texture2D>("spiderenemy");
            leftTexture = Content.Load<Texture2D>("leftWall");
            rightTexture = Content.Load<Texture2D>("rightWall");
            topTexture = Content.Load<Texture2D>("topWall");
            bottomTexture = Content.Load<Texture2D>("bottomWall");
            bottomLeftInTexture = Content.Load<Texture2D>("bottomLeftIn");
            bottomRightInTexture = Content.Load<Texture2D>("bottomRightIn");
            topLeftInTexture = Content.Load<Texture2D>("topLeftIn");
            topRightInTexture = Content.Load<Texture2D>("topRightIn");
            bottomLeftOutTexture = Content.Load<Texture2D>("bottomLeftOut");
            bottomRightOutTexture = Content.Load<Texture2D>("bottomRightOut");
            topLeftOutTexture = Content.Load<Texture2D>("topLeftOut");
            topRightOutTexture = Content.Load<Texture2D>("topRightOut");
            LightSource = Content.Load<Texture2D>("LightSourceTile");
            Skull = Content.Load<Texture2D>("SkullHidingSpot");
            DiagonalDownBottom = Content.Load<Texture2D>("DiagonalWallDownBottom");
            DiagonalDownTop = Content.Load<Texture2D>("DiagonalWallDownTop");
            DiagonalUpBottom = Content.Load<Texture2D>("DiagonalWallUpBottom");
            DiagonalUpTop = Content.Load<Texture2D>("DiagonalWallUpTop");
            SpiderWeb = Content.Load<Texture2D>("SpiderWebTile");
            Stalagmites = Content.Load<Texture2D>("StalagmitesTile");
            Boulder = Content.Load<Texture2D>("BoulderTile");
            LevelExit = Content.Load<Texture2D>("levelexit");
            LevelExitLeft = Content.Load<Texture2D>("levelexitleft");
            LevelExitRight = Content.Load<Texture2D>("levelexitright");
            LevelExitUp = Content.Load<Texture2D>("levelexitup");
            LevelExitDown = Content.Load<Texture2D>("levelexitdown");
            endgrid = Content.Load<Texture2D>("end grid");

            int posX = 0;
            int posY = 0;
            tiles = new List<Tile>();
            tileArray = new Tile[15, 25];
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 25; j++)
                {
                    tileArray[i, j] = new Tile(posX,posY,40,40,tileTexture,i,j);
                    posX += 41;
                }
                posX = 0;
                posY += 42;
            }

            options = new List<Option>();
            options.Add(new Option(1110, 20, 20, 20, floorTexture, TileType.floor));
            options.Add(new Option(1110, 50, 20, 20, playerTexture, TileType.player));
            options.Add(new Option(1110, 80, 20, 20, enemyTexture, TileType.enemy));
            options.Add(new Option(1310, 80, 20, 20, sleepingenemyTexture, TileType.sleepingenemy));
            options.Add(new Option(1310, 110, 20, 20, spiderenemyTexture, TileType.spiderenemy));

            options.Add(new Option(1110, 110, 20, 20, rightTexture, TileType.rightWall));
            options.Add(new Option(1110, 140, 20, 20, leftTexture, TileType.leftWall));
            options.Add(new Option(1110, 170, 20, 20, topTexture, TileType.topWall));
            options.Add(new Option(1110, 200, 20, 20, bottomTexture, TileType.bottomWall));
            options.Add(new Option(1110, 230, 20, 20, topLeftInTexture, TileType.topLeftCornerIn));
            options.Add(new Option(1110, 260, 20, 20, topRightInTexture, TileType.topRightCornerIn));
            options.Add(new Option(1110, 290, 20, 20, bottomLeftInTexture, TileType.bottomLeftCornerIn));
            options.Add(new Option(1110, 320, 20, 20, bottomRightInTexture, TileType.bottomRightCornerIn));

            options.Add(new Option(1310, 230, 20, 20, topLeftOutTexture, TileType.topLeftCornerOut));
            options.Add(new Option(1310, 260, 20, 20, topRightOutTexture, TileType.topRightCornerOut));
            options.Add(new Option(1310, 290, 20, 20, bottomLeftOutTexture, TileType.bottomLeftCornerOut));
            options.Add(new Option(1310, 320, 20, 20, bottomRightOutTexture, TileType.bottomRightCornerOut));

            
            options.Add(new Option(1110, 350, 20, 20, DiagonalUpTop, TileType.diagonaluptop));
            options.Add(new Option(1110, 380, 20, 20, DiagonalUpBottom, TileType.diagonalupbottom));
            options.Add(new Option(1110, 410, 20, 20, DiagonalDownTop, TileType.diagonaldowntop));
            options.Add(new Option(1110, 440, 20, 20, DiagonalDownBottom, TileType.diagonaldownbottom));
            options.Add(new Option(1110, 470, 20, 20, LightSource, TileType.lightsource));
            options.Add(new Option(1110, 500, 20, 20, Skull, TileType.skullhidingspot));
            options.Add(new Option(1110, 530, 20, 20, SpiderWeb, TileType.spiderweb));
            options.Add(new Option(1110, 560, 20, 20, Stalagmites, TileType.stalagmites));
            options.Add(new Option(1110, 590, 20, 20, Boulder, TileType.boulder));
            options.Add(new Option(1110, 620, 20, 20, LevelExit, TileType.exitend));
            options.Add(new Option(1110, 650, 20, 20, endgrid, TileType.endgrid));

            options.Add(new Option(1310, 560, 20, 20, LevelExitLeft, TileType.exitLeft));
            options.Add(new Option(1310, 590, 20, 20, LevelExitRight, TileType.exitRight));
            options.Add(new Option(1310, 620, 20, 20, LevelExitUp, TileType.exitUp));
            options.Add(new Option(1310, 650, 20, 20, LevelExitDown, TileType.exitDown));


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
            
            lastKBState = currentKBState;
            currentKBState = Keyboard.GetState();

            if (lastKBState.IsKeyUp(Keys.E) && currentKBState.IsKeyDown(Keys.E) && !saveEnabled && !loadEnabled)
            {
                saveEnabled = true;
                deleteFirst = true;
                loadFailed = false;
            }

            if (saveEnabled && lastKBState.IsKeyUp(Keys.Enter) && currentKBState.IsKeyDown(Keys.Enter))
            {
                ExportMap();
            }

            if (lastKBState.IsKeyUp(Keys.L) && currentKBState.IsKeyDown(Keys.L) && !loadEnabled && !saveEnabled)
            {
                loadEnabled = true;
                deleteFirst = true;
                loadFailed = false;
            }

            if (loadEnabled && lastKBState.IsKeyUp(Keys.Enter) && currentKBState.IsKeyDown(Keys.Enter))
            {
                LoadMap();
            }

            if (saveEnabled || loadEnabled)
            {
                filename += AddNextLetter(lastKBState, currentKBState);
            }
            deleteFirst = false;

            lastMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
            Point pos = new Point(currentMouseState.X, currentMouseState.Y);

            foreach (Tile t in tileArray)
            {
                if (t.Rect.Contains(pos))
                {
                    t.Highlight = true;
                    if (currentMouseState.LeftButton == ButtonState.Pressed)
                    {
                        t.Type = selectedType;
                        t.Texture = selectedTexture;
                    }
                }
                else
                {
                    t.Highlight = false;
                }
            }

            foreach (Option o in options)
            {
                if (o.Rect.Contains(pos) && lastMouseState.LeftButton == ButtonState.Released && currentMouseState.LeftButton == ButtonState.Pressed)
                {
                    if (o.Selected == false)
                    {
                        o.Selected = true;
                        selectedType = o.Type;
                        selectedTexture = o.Texture;
                    }
                    else if (o.Selected == true)
                    {
                        o.Selected = false;
                        selectedType = TileType.blank;
                        selectedTexture = tileTexture;
                    }
                }
            }

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

            foreach (Tile t in tileArray)
            {
                t.Draw(spriteBatch);
            }
            foreach (Option o in options)
            {
                o.Draw(spriteBatch);
                spriteBatch.DrawString(font, o.Name, new Vector2(o.Rect.X+30, o.Rect.Y), Color.Black);
            }

            spriteBatch.DrawString(font, "Press L to load a map", new Vector2(1110, 700), Color.Black);
            spriteBatch.DrawString(font, "Press E to export map", new Vector2(1110, 720), Color.Black);
            if (saveEnabled)
            {
                spriteBatch.DrawString(font, "Save as: " + filename, new Vector2(1110, 750), Color.Black);
            } 
            if (loadEnabled)
            {
                spriteBatch.DrawString(font, "Load: " + filename, new Vector2(1110, 750), Color.Black);
            }
            if (loadFailed)
            {
                spriteBatch.DrawString(font, "Error loading file: " + errorMessage, new Vector2(300, 750), Color.Black);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }


        protected void ExportMap()
        {
            String[,] map = new string[15, 25];

            string asciiValue = null;
            foreach (Tile t in tileArray)
            {
                switch (t.Type)
                {
                    case TileType.topLeftCornerIn:
                        asciiValue = "+";
                        break;
                    case TileType.topRightCornerIn:
                        asciiValue = ".";
                        break;
                    case TileType.bottomLeftCornerIn:
                        asciiValue = "<";
                        break;
                    case TileType.bottomRightCornerIn:
                        asciiValue = ">";
                        break;
                    case TileType.topLeftCornerOut:
                        asciiValue = "x";
                        break;
                    case TileType.topRightCornerOut:
                        asciiValue = "X";
                        break;
                    case TileType.bottomLeftCornerOut:
                        asciiValue = "L";
                        break;
                    case TileType.bottomRightCornerOut:
                        asciiValue = ",";
                        break;
                    case TileType.floor:
                        asciiValue = "1";
                        break;
                    case TileType.leftWall:
                        asciiValue = "[";
                        break;
                    case TileType.rightWall:
                        asciiValue = "]";
                        break;
                    case TileType.topWall:
                        asciiValue = "_";
                        break;
                    case TileType.bottomWall:
                        asciiValue = "-";
                        break;
                    case TileType.enemy:
                        asciiValue = "e";
                        break;
                    case TileType.sleepingenemy:
                        asciiValue = "S";
                        break;
                    case TileType.spiderenemy:
                        asciiValue = "s";
                        break;
                    case TileType.player:
                        asciiValue = "p";
                        break;
                    case TileType.blank:
                        asciiValue = " ";
                        break;
                    case TileType.lightsource:
                        asciiValue = "O";
                        break;
                    case TileType.skullhidingspot:
                        asciiValue = "o";
                        break;
                    case TileType.spiderweb:
                        asciiValue = "*";
                        break;
                    case TileType.stalagmites:
                        asciiValue = "M";
                        break;
                    case TileType.boulder:
                        asciiValue = "0";
                        break;
                    case TileType.diagonaldownbottom:
                        asciiValue = "\\";
                        break;
                    case TileType.diagonaldowntop:
                        asciiValue = "|";
                        break;
                    case TileType.diagonalupbottom:
                        asciiValue = "/";
                        break;
                    case TileType.diagonaluptop:
                        asciiValue = "?";
                        break;
                    case TileType.exitUp:
                        asciiValue = "^";
                        break;
                    case TileType.exitLeft:
                        asciiValue = "8";
                        break;
                    case TileType.exitRight:
                        asciiValue = "9";
                        break;
                    case TileType.exitDown:
                        asciiValue = "7";
                        break;
                    case TileType.exitend:
                        asciiValue = "6";
                        break;
                    case TileType.endgrid:
                        asciiValue = "#";
                        break;
                }
                map[t.GridX, t.GridY] = asciiValue;

               
            }

            StreamWriter output = null;
            filename += ".txt";

            try
            {
                output = new StreamWriter(filename);

                for (int i = 0; i < map.GetLength(0); i++)
                {
                    for (int j = 0; j < map.GetLength(1); j++)
                    {
                        output.Write(map[i, j]);
                    }
                    output.WriteLine();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error writing data: " + e.Message);
            }
            finally
            {
                if (output != null)
                    output.Close();
            }

            saveEnabled = false;
            filename = "";
        }


        protected void LoadMap()
        {
            string[,] map = new string[15, 25];
            StreamReader input = null;
            string line;
            int i = 0;

            try
            {
                input = new StreamReader(filename + ".txt");

                while ((line = input.ReadLine()) != null)
                {
                    for (int j = 0; j < line.Length; j++)
                    {
                        map[i, j] = line[j].ToString();
                    }
                    i++;
                }
            }
            catch (Exception e)
            {
                loadFailed = true;
                errorMessage = e.Message;
            }
            finally
            {
                if (input != null)
                    input.Close();
            }

            //int u = 0;
            for(int a = 0; a < map.GetLength(0); a++)
            {
                for (int b = 0; b < map.GetLength(1); b++)
                {
                    switch (map[a, b])
                    {
                        case "+":
                            tileArray[a,b].Type = TileType.topLeftCornerIn;
                            tileArray[a, b].Texture = topLeftInTexture;
                            break;
                        case ".":
                            tileArray[a, b].Type = TileType.topRightCornerIn;
                            tileArray[a, b].Texture = topRightInTexture;
                            break;
                        case "<":
                            tileArray[a, b].Type = TileType.bottomLeftCornerIn;
                            tileArray[a, b].Texture = bottomLeftInTexture;
                            break;
                        case ">":
                            tileArray[a, b].Type = TileType.bottomRightCornerIn;
                            tileArray[a, b].Texture = bottomRightInTexture;
                            break;
                        case "x":
                            tileArray[a, b].Type = TileType.topLeftCornerOut;
                            tileArray[a, b].Texture = topLeftOutTexture;
                            break;
                        case "X":
                            tileArray[a, b].Type = TileType.topRightCornerOut;
                            tileArray[a, b].Texture = topRightOutTexture;
                            break;
                        case "L":
                            tileArray[a, b].Type = TileType.bottomLeftCornerOut;
                            tileArray[a, b].Texture = bottomLeftOutTexture;
                            break;
                        case ",":
                            tileArray[a, b].Type = TileType.bottomRightCornerOut;
                            tileArray[a, b].Texture = bottomRightOutTexture;
                            break;
                        case "1":
                            tileArray[a, b].Type = TileType.floor;
                            tileArray[a, b].Texture = floorTexture;
                            break;
                        case "[":
                            tileArray[a, b].Type = TileType.leftWall;
                            tileArray[a, b].Texture = leftTexture;
                            break;
                        case "]":
                            tileArray[a, b].Type = TileType.rightWall;
                            tileArray[a, b].Texture = rightTexture;
                            break;
                        case "_":
                            tileArray[a, b].Type = TileType.topWall;
                            tileArray[a, b].Texture = topTexture;
                            break;
                        case "-":
                            tileArray[a, b].Type = TileType.bottomWall;
                            tileArray[a, b].Texture = bottomTexture;
                            break;
                        case "e":
                            tileArray[a, b].Type = TileType.enemy;
                            tileArray[a, b].Texture = enemyTexture;
                            break;
                        case "S":
                            tileArray[a, b].Type = TileType.sleepingenemy;
                            tileArray[a, b].Texture = sleepingenemyTexture;
                            break;
                        case "s":
                            tileArray[a, b].Type = TileType.spiderenemy;
                            tileArray[a, b].Texture = spiderenemyTexture;
                            break;
                        case "p":
                            tileArray[a, b].Type = TileType.player;
                            tileArray[a, b].Texture = playerTexture;
                            break;
                        case " ":
                            tileArray[a, b].Type = TileType.blank;
                            tileArray[a, b].Texture = tileTexture;
                            break;
                        case "O":
                            tileArray[a, b].Type = TileType.lightsource;
                            tileArray[a, b].Texture = LightSource;
                            break;
                        case "o":
                            tileArray[a, b].Type = TileType.skullhidingspot;
                            tileArray[a, b].Texture = Skull;
                            break;
                        case "*":
                            tileArray[a, b].Type = TileType.spiderweb;
                            tileArray[a, b].Texture = SpiderWeb;
                            break;
                        case "M":
                            tileArray[a, b].Type = TileType.stalagmites;
                            tileArray[a, b].Texture = Stalagmites;
                            break;
                        case "0":
                            tileArray[a, b].Type = TileType.boulder;
                            tileArray[a, b].Texture = Boulder;
                            break;
                        case "\\":
                            tileArray[a, b].Type = TileType.diagonaldownbottom;
                            tileArray[a, b].Texture = DiagonalDownBottom;
                            break;
                        case "|":
                            tileArray[a, b].Type = TileType.diagonaldowntop;
                            tileArray[a, b].Texture = DiagonalDownTop;
                            break;
                        case "/":
                            tileArray[a, b].Type = TileType.diagonalupbottom;
                            tileArray[a, b].Texture = DiagonalUpBottom;
                            break;
                        case "?":
                            tileArray[a, b].Type = TileType.diagonaluptop;
                            tileArray[a, b].Texture = DiagonalUpTop;
                            break;
                        case "^":
                            tileArray[a, b].Type = TileType.exitUp;
                            tileArray[a, b].Texture = LevelExitUp;
                            break;
                        case "8":
                            tileArray[a, b].Type = TileType.exitLeft;
                            tileArray[a, b].Texture = LevelExitLeft;
                            break;
                        case "9":
                            tileArray[a, b].Type = TileType.exitRight;
                            tileArray[a, b].Texture = LevelExitRight;
                            break;
                        case "7":
                            tileArray[a, b].Type = TileType.exitDown;
                            tileArray[a, b].Texture = LevelExitDown;
                            break;
                        case "6":
                            tileArray[a, b].Type = TileType.exitend;
                            tileArray[a, b].Texture = LevelExit;
                            break;
                        case "#":
                            tileArray[a, b].Type = TileType.endgrid;
                            tileArray[a, b].Texture = endgrid;
                            break;
                    }

                    //u++;
                }
            }
            loadEnabled = false;
            filename = "";
        }


        /// <summary>
        /// gets the key input and returns it as a string
        /// </summary>
        /// <param name="lastKBState"></param>
        /// <param name="currentKBState"></param>
        /// <returns></returns>
        private string AddNextLetter(KeyboardState lastKBState, KeyboardState currentKBState)
        {
            if (lastKBState.IsKeyUp(Keys.A) && currentKBState.IsKeyDown(Keys.A))
                return "a";
            else if (lastKBState.IsKeyUp(Keys.B) && currentKBState.IsKeyDown(Keys.B))
                return "b";
            else if (lastKBState.IsKeyUp(Keys.C) && currentKBState.IsKeyDown(Keys.C))
                return "c";
            else if (lastKBState.IsKeyUp(Keys.D) && currentKBState.IsKeyDown(Keys.D))
                return "d";
            else if (lastKBState.IsKeyUp(Keys.E) && currentKBState.IsKeyDown(Keys.E) && !deleteFirst)
                return "e";
            else if (lastKBState.IsKeyUp(Keys.F) && currentKBState.IsKeyDown(Keys.F))
                return "f";
            else if (lastKBState.IsKeyUp(Keys.G) && currentKBState.IsKeyDown(Keys.G))
                return "g";
            else if (lastKBState.IsKeyUp(Keys.H) && currentKBState.IsKeyDown(Keys.H))
                return "h";
            else if (lastKBState.IsKeyUp(Keys.I) && currentKBState.IsKeyDown(Keys.I))
                return "i";
            else if (lastKBState.IsKeyUp(Keys.J) && currentKBState.IsKeyDown(Keys.J))
                return "j";
            else if (lastKBState.IsKeyUp(Keys.K) && currentKBState.IsKeyDown(Keys.K))
                return "k";
            else if (lastKBState.IsKeyUp(Keys.L) && currentKBState.IsKeyDown(Keys.L) && !deleteFirst)
                return "l";
            else if (lastKBState.IsKeyUp(Keys.M) && currentKBState.IsKeyDown(Keys.M))
                return "m";
            else if (lastKBState.IsKeyUp(Keys.N) && currentKBState.IsKeyDown(Keys.N))
                return "n";
            else if (lastKBState.IsKeyUp(Keys.O) && currentKBState.IsKeyDown(Keys.O))
                return "o";
            else if (lastKBState.IsKeyUp(Keys.P) && currentKBState.IsKeyDown(Keys.P))
                return "p";
            else if (lastKBState.IsKeyUp(Keys.Q) && currentKBState.IsKeyDown(Keys.Q))
                return "q";
            else if (lastKBState.IsKeyUp(Keys.R) && currentKBState.IsKeyDown(Keys.R))
                return "r";
            else if (lastKBState.IsKeyUp(Keys.S) && currentKBState.IsKeyDown(Keys.S))
                return "s";
            else if (lastKBState.IsKeyUp(Keys.T) && currentKBState.IsKeyDown(Keys.T))
                return "t";
            else if (lastKBState.IsKeyUp(Keys.U) && currentKBState.IsKeyDown(Keys.U))
                return "u";
            else if (lastKBState.IsKeyUp(Keys.V) && currentKBState.IsKeyDown(Keys.V))
                return "v";
            else if (lastKBState.IsKeyUp(Keys.W) && currentKBState.IsKeyDown(Keys.W))
                return "w";
            else if (lastKBState.IsKeyUp(Keys.X) && currentKBState.IsKeyDown(Keys.X))
                return "x";
            else if (lastKBState.IsKeyUp(Keys.Y) && currentKBState.IsKeyDown(Keys.Y))
                return "y";
            else if (lastKBState.IsKeyUp(Keys.Z) && currentKBState.IsKeyDown(Keys.Z))
                return "z";
            else if (lastKBState.IsKeyUp(Keys.D0) && currentKBState.IsKeyDown(Keys.D0))
                return "0";
            else if (lastKBState.IsKeyUp(Keys.D1) && currentKBState.IsKeyDown(Keys.D1))
                return "1";
            else if (lastKBState.IsKeyUp(Keys.D2) && currentKBState.IsKeyDown(Keys.D2))
                return "2";
            else if (lastKBState.IsKeyUp(Keys.D3) && currentKBState.IsKeyDown(Keys.D3))
                return "3";
            else if (lastKBState.IsKeyUp(Keys.D4) && currentKBState.IsKeyDown(Keys.D4))
                return "4";
            else if (lastKBState.IsKeyUp(Keys.D5) && currentKBState.IsKeyDown(Keys.D5))
                return "5";
            else if (lastKBState.IsKeyUp(Keys.D6) && currentKBState.IsKeyDown(Keys.D6))
                return "6";
            else if (lastKBState.IsKeyUp(Keys.D7) && currentKBState.IsKeyDown(Keys.D7))
                return "7";
            else if (lastKBState.IsKeyUp(Keys.D8) && currentKBState.IsKeyDown(Keys.D8))
                return "8";
            else if (lastKBState.IsKeyUp(Keys.D9) && currentKBState.IsKeyDown(Keys.D9))
                return "9";
            else if (lastKBState.IsKeyUp(Keys.Back) && currentKBState.IsKeyDown(Keys.Back))
                filename = filename.Remove(0, 3);

            return "";
        }
    }
}
