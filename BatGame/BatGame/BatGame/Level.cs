//Madison and Adam
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    class Level : LevelManager
    {
        private string levelfile;
        private GameObject[,] levelObjectArray;

        internal GameObject[,] LevelObjectArray1
        {
            get { return levelObjectArray; }
            set { levelObjectArray = value; }
        }
        private string[,] levelStringArray;
        bool hasStarted;
        int spawnPlayerX;
        int spawnPlayerY;
        int savedPlayerX;
        int savedPlayerY;
        Dictionary<string, Texture2D> textureDictionary;
        Grid grid;
        EnemyManager enemyManager;
        ImmobilesManager immobilesManager;
        GameObjectManager gameObjectManager;
        Level leftNeighbor;
        Level rightNeighbor;
        Level upNeighbor;
        Level bottomNeighbor;

        /// <summary>
        /// Constructor takes a string tht it uses for the path to the level's text file
        /// </summary>
        public Level(string lvlfile, Dictionary<string, Texture2D> textureDictionary, Grid grid, EnemyManager enemyManager, ImmobilesManager immobilesManager, GameObjectManager gameObjectManager)
        {
            levelfile = lvlfile;
            this.textureDictionary = textureDictionary;
            this.grid = grid;
            this.enemyManager = enemyManager;
            this.immobilesManager = immobilesManager;
            this.gameObjectManager = gameObjectManager;
        }

        #region Properties
        /// <summary>
        /// Property for the 2D array of Texture2D's for mapping the level
        /// </summary>
        public GameObject[,] LevelObjectArray
        {
            get { return levelObjectArray; }
        }
        public bool HasStarted
        {
            get { return hasStarted; }
            set { hasStarted = value; }
        }
        public int SpawnPlayerX
        {
            get { return spawnPlayerX; }
            set { spawnPlayerX = value; }
        }

        public int SpawnPlayerY
        {
            get { return spawnPlayerY; }
            set { spawnPlayerY = value; }
        }

        public int SavedPlayerX
        {
            get { return savedPlayerX; }
            set { savedPlayerX = value; }
        }

        public int SavedPlayerY
        {
            get { return savedPlayerY; }
            set { savedPlayerY = value; }
        }

        public Level LeftNeighbor
        {
            get { return leftNeighbor; }
            set { leftNeighbor = value; }
        }

        public Level RightNeighbor
        {
            get { return rightNeighbor; }
            set { rightNeighbor = value; }
        }

        public Level UpNeighbor
        {
            get { return upNeighbor; }
            set { upNeighbor = value; }
        }

        public Level DownNeighbor
        {
            get { return bottomNeighbor; }
            set { bottomNeighbor = value; }
        }
        #endregion

        /// <summary>
        /// This loads the text file given to the level and maps it to the 2D string array levelStringArray
        ///</summary>
        public string[,] loadLevel()
        {

            String[,] data = new string[100, 100];
            String[,] map;
            int longestRow = 0;

            StreamReader input = null;

            try
            {
                input = new StreamReader(levelfile);

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


                /*map = new string[counter, longestRow];
                for (int i = 0; i < counter; i++)
                {
                    for (int j = 0; j < longestRow; j++)
                    {
                        map[i, j] = data[i, j];
                    }
                }*/

                //levelStringArray = map;
                levelStringArray = data;

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

            return levelStringArray;
        }

        /// <summary>
        ///Sets up the level by taking the array of strings from levelStringArray and matching it to textures from the textureDictionary, 
        ///and putting those textures into GameObjects that go into levelObjectArray.
        /// </summary>
        public GameObject[,] setupLevel()
        {
            Reset();

            levelObjectArray = new GameObject[levelStringArray.GetLength(1), levelStringArray.GetLength(0)];

            for (int i = 0; i < levelStringArray.GetLength(0); i++)
            {
                for (int j = 0; j < levelStringArray.GetLength(1); j++)
                {
                    switch (levelStringArray[i, j])
                    {
                        #region Vertical Left Wall
                        case "]":
                            Wall tempVertL = new Wall(textureDictionary["verticalLeftWall"], gameObjectManager, new Point(j, i), grid, Direction.Right, SubSquares.TopLeft, true);
                            levelObjectArray[i, j] = tempVertL;
                            immobilesManager.AddImmobile(tempVertL);
                            gameObjectManager.AddGameObject(tempVertL);
                            break;
                        #endregion
                        #region Vertical Right Wall
                        case "[":
                            Wall tempVertR = new Wall(textureDictionary["verticalRightWall"], gameObjectManager, new Point(j, i), grid, Direction.Right, SubSquares.TopLeft, true);
                            levelObjectArray[i, j] = tempVertR;
                            immobilesManager.AddImmobile(tempVertR);
                            gameObjectManager.AddGameObject(tempVertR);
                            break;
                        #endregion
                        #region Top Left Corner
                        case "+":
                            Wall tempCornerDR = new Wall(textureDictionary["downRightCornerWall"], gameObjectManager, new Point(j, i), grid, Direction.Right, SubSquares.TopLeft, true);
                            levelObjectArray[i, j] = tempCornerDR;
                            immobilesManager.AddImmobile(tempCornerDR);
                            gameObjectManager.AddGameObject(tempCornerDR);
                            break;
                        #endregion
                        #region Top Right Corner
                        case ".":
                            Wall tempCornerDL = new Wall(textureDictionary["downLeftCornerWall"], gameObjectManager, new Point(j, i), grid, Direction.Right, SubSquares.TopLeft, true);
                            levelObjectArray[i, j] = tempCornerDL;
                            immobilesManager.AddImmobile(tempCornerDL);
                            gameObjectManager.AddGameObject(tempCornerDL);
                            break;
                        #endregion
                        #region Bottom Left Corner
                        case "<":
                            Wall tempCornerUR = new Wall(textureDictionary["upRightCornerWall"], gameObjectManager, new Point(j, i), grid, Direction.Right, SubSquares.TopLeft, true);
                            levelObjectArray[i, j] = tempCornerUR;
                            immobilesManager.AddImmobile(tempCornerUR);
                            gameObjectManager.AddGameObject(tempCornerUR);
                            break;
                        #endregion
                        #region Bottom Right Corner
                        case ">":
                            Wall tempCornerUL = new Wall(textureDictionary["upLeftCornerWall"], gameObjectManager, new Point(j, i), grid, Direction.Right, SubSquares.TopLeft, true);
                            levelObjectArray[i, j] = tempCornerUL;
                            immobilesManager.AddImmobile(tempCornerUL);
                            gameObjectManager.AddGameObject(tempCornerUL);
                            break;
                        #endregion
                        #region Bottom Left Corner Inverted
                        case "L":
                            Wall tempCornerDLInverted = new Wall(textureDictionary["downLeftCornerInvertedWall"], gameObjectManager, new Point(j, i), grid, Direction.Right, SubSquares.TopLeft, true);
                            levelObjectArray[i, j] = tempCornerDLInverted;
                            immobilesManager.AddImmobile(tempCornerDLInverted);
                            gameObjectManager.AddGameObject(tempCornerDLInverted);
                            break;
                        #endregion
                        #region Bottom Right Corner Inverted
                        case ",":
                            Wall tempCornerDRInverted = new Wall(textureDictionary["downRightCornerInvertedWall"], gameObjectManager, new Point(j, i), grid, Direction.Right, SubSquares.TopLeft, true);
                            levelObjectArray[i, j] = tempCornerDRInverted;
                            immobilesManager.AddImmobile(tempCornerDRInverted);
                            gameObjectManager.AddGameObject(tempCornerDRInverted);
                            break;
                        #endregion
                        #region Top Left Corner Inverted
                        case "x":
                            Wall tempCornerULInverted = new Wall(textureDictionary["upLeftCornerInvertedWall"], gameObjectManager, new Point(j, i), grid, Direction.Right, SubSquares.TopLeft, true);
                            levelObjectArray[i, j] = tempCornerULInverted;
                            immobilesManager.AddImmobile(tempCornerULInverted);
                            gameObjectManager.AddGameObject(tempCornerULInverted);
                            break;
                        #endregion
                        #region Top Right Corner Inverted
                        case "X":
                            Wall tempCornerURInverted = new Wall(textureDictionary["upRightCornerInvertedWall"], gameObjectManager, new Point(j, i), grid, Direction.Right, SubSquares.TopLeft, true);
                            levelObjectArray[i, j] = tempCornerURInverted;
                            immobilesManager.AddImmobile(tempCornerURInverted);
                            gameObjectManager.AddGameObject(tempCornerURInverted);
                            break;
                        #endregion

                        #region Horizontal wall Top
                        case "_":
                            Wall tempHorizU = new Wall(textureDictionary["horizontalUpWall"], gameObjectManager, new Point(j, i), grid, Direction.Right, SubSquares.TopLeft, true);
                            levelObjectArray[i, j] = tempHorizU;
                            immobilesManager.AddImmobile(tempHorizU);
                            gameObjectManager.AddGameObject(tempHorizU);

                            break;
                        #endregion
                        #region Horizontal Wall Bottom
                        case "-":
                            Wall tempHorizD = new Wall(textureDictionary["horizontalDownWall"], gameObjectManager, new Point(j, i), grid, Direction.Right, SubSquares.TopLeft, true);
                            levelObjectArray[i, j] = tempHorizD;
                            immobilesManager.AddImmobile(tempHorizD);
                            gameObjectManager.AddGameObject(tempHorizD);
                            break;
                        #endregion
                        #region Diagonal Wall Up Top
                        case "?":
                            DiagonalWall tempUpTop = new DiagonalWall(textureDictionary["upTopWall"], gameObjectManager, new Point(j, i), grid, Direction.Right, SubSquares.TopLeft, true, DiagonalWallType.upTop);
                            levelObjectArray[i, j] = tempUpTop;
                            immobilesManager.AddImmobile(tempUpTop);
                            gameObjectManager.AddGameObject(tempUpTop);
                            break;
                        #endregion
                        #region Diagonal Wall Up Bottom
                        case "/":
                            DiagonalWall tempUpBottom = new DiagonalWall(textureDictionary["upBottomWall"], gameObjectManager, new Point(j, i), grid, Direction.Right, SubSquares.TopLeft, true, DiagonalWallType.upBottom);
                            levelObjectArray[i, j] = tempUpBottom;
                            immobilesManager.AddImmobile(tempUpBottom);
                            gameObjectManager.AddGameObject(tempUpBottom);
                            break;
                        #endregion
                        #region Diagonal Wall Down Top
                        case "|":
                            DiagonalWall tempDownTop = new DiagonalWall(textureDictionary["downTopWall"], gameObjectManager, new Point(j, i), grid, Direction.Right, SubSquares.TopLeft, true, DiagonalWallType.downTop);
                            levelObjectArray[i, j] = tempDownTop;
                            immobilesManager.AddImmobile(tempDownTop);
                            gameObjectManager.AddGameObject(tempDownTop);
                            break;
                        #endregion
                        #region Diagonal Wall Down Bottom
                        case "\\":
                            DiagonalWall tempDownBottom = new DiagonalWall(textureDictionary["downBottomWall"], gameObjectManager, new Point(j, i), grid, Direction.Right, SubSquares.TopLeft, true, DiagonalWallType.downBottom);
                            levelObjectArray[i, j] = tempDownBottom;
                            immobilesManager.AddImmobile(tempDownBottom);
                            gameObjectManager.AddGameObject(tempDownBottom);
                            break;
                        #endregion
                        #region Floor
                        case "1":
                            Floor tempFloor = new Floor(textureDictionary["floorTile"], gameObjectManager, new Point(j, i), grid, Direction.Right, SubSquares.TopLeft, false);
                            levelObjectArray[i, j] = tempFloor;
                            immobilesManager.AddImmobile(tempFloor);
                            gameObjectManager.AddGameObject(tempFloor);

                            break;
                        #endregion
                        #region Enemy
                        case "e":
                            Floor tempFloor2 = new Floor(textureDictionary["floorTile"], gameObjectManager, new Point(j, i), grid, Direction.Right, SubSquares.TopLeft, false);
                            levelObjectArray[i, j] = tempFloor2;
                            immobilesManager.AddImmobile(tempFloor2);
                            gameObjectManager.AddGameObject(tempFloor2);

                            Enemy tempE = new Enemy(textureDictionary["enemyImage"], gameObjectManager, new Point(j, i), grid, Direction.Down, SubSquares.TopLeft, true, 0, 0, true, 3, false);
                            levelObjectArray[i, j] = tempE;
                            enemyManager.AddEnemy(tempE);
                            gameObjectManager.AddGameObject(tempE);
                            break;
                        #endregion
                        #region Player
                        case "p":
                            Floor tempFloor3 = new Floor(textureDictionary["floorTile"], gameObjectManager, new Point(j, i), grid, Direction.Right, SubSquares.TopLeft, false);
                            levelObjectArray[i, j] = tempFloor3;
                            immobilesManager.AddImmobile(tempFloor3);
                            gameObjectManager.AddGameObject(tempFloor3);

                            //Adds a player
                            Player player = new Player(textureDictionary["playerImage"], gameObjectManager, new Point(spawnPlayerX, spawnPlayerY), grid, Direction.Right, SubSquares.TopLeft, true, 0, .15, true, 3);
                            if (HasStarted)
                            {
                                player = new Player(textureDictionary["playerImage"], gameObjectManager, new Point(savedPlayerX, savedPlayerY), grid, Direction.Right, SubSquares.TopLeft, true, 0, .15, true, 3);
                                HasStarted = false;
                            }
                            gameObjectManager.Player = player;

                            break;
                        #endregion
                        #region SpiderWeb
                        case "*":
                            SpiderWeb tempWeb = new SpiderWeb(textureDictionary["spiderWeb"], gameObjectManager, new Point(j, i), grid, Direction.Down, SubSquares.TopLeft, false, true);
                            immobilesManager.AddImmobile(tempWeb);
                            gameObjectManager.AddGameObject(tempWeb);
                            break;
                        #endregion
                        #region Stalagmite
                        case "M":
                            
                            Floor tempFloor4 = new Floor(textureDictionary["floorTile"], gameObjectManager, new Point(j, i), grid, Direction.Right, SubSquares.TopLeft, false);
                            levelObjectArray[i, j] = tempFloor4;
                            immobilesManager.AddImmobile(tempFloor4);
                            
                            Stalagmite tempStag = new Stalagmite(textureDictionary["stalagmite"], gameObjectManager, new Point(j, i), grid, Direction.Down, SubSquares.TopLeft, false, true);
                            tempStag.DrawWeight = 2;
                            immobilesManager.AddImmobile(tempStag);
                            gameObjectManager.AddGameObject(tempStag);
                            break;
                        #endregion
                        #region Boulder
                        case "0":
                            Boulder tempBoulder = new Boulder(textureDictionary["boulder"], gameObjectManager, new Point(j, i), grid, Direction.Down, SubSquares.TopLeft, true);
                            immobilesManager.AddImmobile(tempBoulder);
                            gameObjectManager.AddGameObject(tempBoulder);
                            break;
                        #endregion
                        #region Skull
                        case "o":
                            Floor tempFloor5 = new Floor(textureDictionary["floorTile"], gameObjectManager, new Point(j, i), grid, Direction.Right, SubSquares.TopLeft, false);
                            levelObjectArray[i, j] = tempFloor5;
                            immobilesManager.AddImmobile(tempFloor5);

                            Skull tempSkull = new Skull(textureDictionary["skull"], gameObjectManager, new Point(j, i), grid, Direction.Down, SubSquares.TopLeft, false, true);
                            tempSkull.DrawWeight = 2;
                            immobilesManager.AddImmobile(tempSkull);
                            gameObjectManager.AddGameObject(tempSkull);
                            break;
                        #endregion
                        #region Up Exit
                        case "^":
                            Exit tempExit = new Exit(textureDictionary["floorTile"], gameObjectManager, new Point(j, i), grid, Direction.Right, SubSquares.TopLeft, false, true, Exit.ExitType.Up);
                            levelObjectArray[i, j] = tempExit;
                            immobilesManager.AddImmobile(tempExit);
                            gameObjectManager.AddGameObject(tempExit);
                            break;
                        #endregion
                        #region Down Exit
                        case "7":
                            Exit tempExit2 = new Exit(textureDictionary["floorTile"], gameObjectManager, new Point(j, i), grid, Direction.Right, SubSquares.TopLeft, false, true, Exit.ExitType.Down);
                            levelObjectArray[i, j] = tempExit2;
                            immobilesManager.AddImmobile(tempExit2);
                            gameObjectManager.AddGameObject(tempExit2);
                            break;
                        #endregion
                        #region Left Exit
                        case "8":
                            Exit tempExit3 = new Exit(textureDictionary["floorTile"], gameObjectManager, new Point(j, i), grid, Direction.Right, SubSquares.TopLeft, false, true, Exit.ExitType.Left);
                            levelObjectArray[i, j] = tempExit3;
                            immobilesManager.AddImmobile(tempExit3);
                            gameObjectManager.AddGameObject(tempExit3);
                            break;
                        #endregion
                        #region Right Exit
                        case "9":
                            Exit tempExit4 = new Exit(textureDictionary["floorTile"], gameObjectManager, new Point(j, i), grid, Direction.Right, SubSquares.TopLeft, false, true, Exit.ExitType.Right);
                            levelObjectArray[i, j] = tempExit4;
                            immobilesManager.AddImmobile(tempExit4);
                            gameObjectManager.AddGameObject(tempExit4);
                            break;
                        #endregion
                        #region Game Over Exit
                        case "6":
                            Exit tempExit5 = new Exit(textureDictionary["floorTile"], gameObjectManager, new Point(j, i), grid, Direction.Right, SubSquares.TopLeft, false, true, Exit.ExitType.Finish);
                            levelObjectArray[i, j] = tempExit5;
                            immobilesManager.AddImmobile(tempExit5);
                            gameObjectManager.AddGameObject(tempExit5);
                            break;
                        #endregion
                        #region Light Source
                        case "O":
                            Floor tempFloor6 = new Floor(textureDictionary["floorTile"], gameObjectManager, new Point(j, i), grid, Direction.Right, SubSquares.TopLeft, false);
                            levelObjectArray[i, j] = tempFloor6;
                            immobilesManager.AddImmobile(tempFloor6);

                            LightSource tempLight = new LightSource(textureDictionary["smallCrystal"], gameObjectManager, new Point(j, i), grid, Direction.Down, SubSquares.TopLeft, false, true);
                            levelObjectArray[i, j] = tempLight;
                            immobilesManager.AddImmobile(tempLight);
                            gameObjectManager.AddGameObject(tempLight);
                            break;
                        #endregion
                    }
                }
            }

            return levelObjectArray;
        }

        public void Reset()
        {
            gameObjectManager.Clear();
            immobilesManager.Clear();
            enemyManager.Clear();
        }

    }
}
