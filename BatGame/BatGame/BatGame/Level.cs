﻿using System;
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
        private string[,] levelStringArray;
        bool hasStarted;
        int spawnPlayerX;
        int spawnPlayerY;
        Dictionary<string, Texture2D> textureDictionary;
        Grid grid;
        EnemyManager enemyManager;
        ImmobilesManager immobilesManager;
        GameObjectManager gameObjectManager;

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
        ///and putting those textures into GameObjects that go into levelObjectArray. I dont like how I need to put the EnemyManager and Grid objects as variables
        ///in this. Make it so it doesnt need that.                                                               
        ///I have everything loading in as immobiles right now. Its yelling because thats an abstract class. Wes and Joel need to go through 
        ///and create classes for all of the different GameObjects. I dont know how they want to go about doing that so im just leaving it like this for now. 
        ///Until then, if you want to run the game, just comment out where this method and where it is called
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
                        case "|":
                            Immobiles tempVert = new Immobiles(textureDictionary["verticalWall"], gameObjectManager, new Point(j, i), grid, Direction.Right, SubSquares.TopLeft, true);
                            levelObjectArray[i, j] = tempVert;
                            immobilesManager.AddImmobile(tempVert);
                            gameObjectManager.AddGameObject(tempVert);
                            break;
                        case "]":
                            Wall tempVertL = new Wall(textureDictionary["verticalLeftWall"], gameObjectManager, new Point(j, i), grid, Direction.Right, SubSquares.TopLeft, true);
                            levelObjectArray[i, j] = tempVertL;
                            immobilesManager.AddImmobile(tempVertL);
                            gameObjectManager.AddGameObject(tempVertL);
                            break;
                        case "[":
                            Wall tempVertR = new Wall(textureDictionary["verticalRightWall"], gameObjectManager, new Point(j, i), grid, Direction.Right, SubSquares.TopLeft, true);
                            levelObjectArray[i, j] = tempVertR;
                            immobilesManager.AddImmobile(tempVertR);
                            gameObjectManager.AddGameObject(tempVertR);
                            break;


                        case "+":
                            Wall tempCornerDR = new Wall(textureDictionary["downRightCornerWall"], gameObjectManager, new Point(j, i), grid, Direction.Right, SubSquares.TopLeft, true);
                            levelObjectArray[i, j] = tempCornerDR;
                            immobilesManager.AddImmobile(tempCornerDR);
                            gameObjectManager.AddGameObject(tempCornerDR);
                            break;
                        case ".":
                            Wall tempCornerDL = new Wall(textureDictionary["downLeftCornerWall"], gameObjectManager, new Point(j, i), grid, Direction.Right, SubSquares.TopLeft, true);
                            levelObjectArray[i, j] = tempCornerDL;
                            immobilesManager.AddImmobile(tempCornerDL);
                            gameObjectManager.AddGameObject(tempCornerDL);
                            break;
                        case "<":
                            Wall tempCornerUR = new Wall(textureDictionary["upRightCornerWall"], gameObjectManager, new Point(j, i), grid, Direction.Right, SubSquares.TopLeft, true);
                            levelObjectArray[i, j] = tempCornerUR;
                            immobilesManager.AddImmobile(tempCornerUR);
                            gameObjectManager.AddGameObject(tempCornerUR);
                            break;
                        case ">":
                            Wall tempCornerUL = new Wall(textureDictionary["upLeftCornerWall"], gameObjectManager, new Point(j, i), grid, Direction.Right, SubSquares.TopLeft, true);
                            levelObjectArray[i, j] = tempCornerUL;
                            immobilesManager.AddImmobile(tempCornerUL);
                            gameObjectManager.AddGameObject(tempCornerUL);
                            break;


                        case "_":
                            Wall tempHorizU = new Wall(textureDictionary["horizontalUpWall"], gameObjectManager, new Point(j, i), grid, Direction.Right, SubSquares.TopLeft, true);
                            levelObjectArray[i, j] = tempHorizU;
                            immobilesManager.AddImmobile(tempHorizU);
                            gameObjectManager.AddGameObject(tempHorizU);

                            break;
                        case "-":
                            Wall tempHorizD = new Wall(textureDictionary["horizontalDownWall"], gameObjectManager, new Point(j, i), grid, Direction.Right, SubSquares.TopLeft, true);
                            levelObjectArray[i, j] = tempHorizD;
                            immobilesManager.AddImmobile(tempHorizD);
                            gameObjectManager.AddGameObject(tempHorizD);
                            break;

                        case "1":
                            Immobiles tempFloor = new Immobiles(textureDictionary["floorTile"], gameObjectManager, new Point(j, i), grid, Direction.Right, SubSquares.TopLeft, false);
                            levelObjectArray[i, j] = tempFloor;
                            immobilesManager.AddImmobile(tempFloor);
                            gameObjectManager.AddGameObject(tempFloor);

                            break;
                        case "e":
                            Immobiles tempFloor2 = new Immobiles(textureDictionary["floorTile"], gameObjectManager, new Point(j, i), grid, Direction.Right, SubSquares.TopLeft, false);
                            levelObjectArray[i, j] = tempFloor2;
                            immobilesManager.AddImmobile(tempFloor2);
                            gameObjectManager.AddGameObject(tempFloor2);

                            Enemy tempE = new Enemy(textureDictionary["enemyImage"], gameObjectManager, new Point(j, i), grid, Direction.Down, SubSquares.TopLeft, true, 0, 0, true, 3, false);
                            levelObjectArray[i, j] = tempE;
                            enemyManager.AddEnemy(tempE);
                            gameObjectManager.AddGameObject(tempE);
                            break;
                        case "p":
                            Immobiles tempFloor3 = new Immobiles(textureDictionary["floorTile"], gameObjectManager, new Point(j, i), grid, Direction.Right, SubSquares.TopLeft, false);
                            levelObjectArray[i, j] = tempFloor3;
                            immobilesManager.AddImmobile(tempFloor3);
                            gameObjectManager.AddGameObject(tempFloor3);

                            //Adds a player
                            Player player = new Player(textureDictionary["playerImage"], gameObjectManager, new Point(j, i), grid, Direction.Right, SubSquares.TopLeft, true, 0, .1, true, 3);
                            if (HasStarted)
                            {
                                player = new Player(textureDictionary["playerImage"], gameObjectManager, new Point(SpawnPlayerX, SpawnPlayerY), grid, Direction.Right, SubSquares.TopLeft, true, 0, .1, true, 3);
                                HasStarted = false;
                            }
                            gameObjectManager.Player = player;

                            break;
                        case "*":
                            SpiderWeb tempWeb = new SpiderWeb(textureDictionary["spiderWeb"], gameObjectManager, new Point(j, i), grid, Direction.Down, SubSquares.TopLeft, false, true);
                            immobilesManager.AddImmobile(tempWeb);
                            gameObjectManager.AddGameObject(tempWeb);
                            break;
                        case "M":
                            Stalagmite tempStag = new Stalagmite(textureDictionary["stalagmite"], gameObjectManager, new Point(j, i), grid, Direction.Down, SubSquares.TopLeft, false, true);
                            tempStag.DrawWeight = 2;
                            immobilesManager.AddImmobile(tempStag);
                            gameObjectManager.AddGameObject(tempStag);
                            break;
                        case "0":
                            Boulder tempBoulder = new Boulder(textureDictionary["boulder"], gameObjectManager, new Point(j, i), grid, Direction.Down, SubSquares.TopLeft, true);
                            immobilesManager.AddImmobile(tempBoulder);
                            gameObjectManager.AddGameObject(tempBoulder);
                            break;
                        case "o":
                            Skull tempSkull = new Skull(textureDictionary["spiderWeb"], gameObjectManager, new Point(j, i), grid, Direction.Down, SubSquares.TopLeft, false, true);
                            immobilesManager.AddImmobile(tempSkull);
                            gameObjectManager.AddGameObject(tempSkull);
                            break;
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
