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
    class Level: LevelManager
    {
        private string levelfile;
        private GameObject[,] levelObjectArray;
        private string[,] levelStringArray;

        /// <summary>
        /// Constructor takes a string tht it uses for the path to the level's text file
        /// </summary>
        public Level(string lvlfile)
        {
            levelfile = lvlfile;
        }


        /// <summary>
        /// Property for the 2D array of Texture2D's for mapping the level
        /// </summary>
        public GameObject[,] LevelObjectArray
        {
            get { return levelObjectArray; }
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


                map = new string[counter, longestRow];
                for (int i = 0; i < counter; i++)
                {
                    for (int j = 0; j < longestRow; j++)
                    {
                        map[i, j] = data[i, j];
                    }
                }

                levelStringArray = map;

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
        public GameObject[,] setupLevel(Dictionary<string, Texture2D> textureDictionary, Grid grid, EnemyManager enemyManager)
        {

            levelObjectArray = new GameObject[levelStringArray.GetLength(1), levelStringArray.GetLength(0)];

            for (int i = 0; i < levelStringArray.GetLength(0); i++)
            {
                for (int j = 0; j < levelStringArray.GetLength(1); j++)
                {
                    switch (levelStringArray[i, j])
                    {
                        case "|":
                            //Adds a vertical wall
                            levelObjectArray[i, j] = new Immobiles(textureDictionary["verticalWall"], new Point(j, i), grid, Direction.Right, false);
                            break;
                        case "+":
                            //Adds a corner
                            levelObjectArray[i, j] = new Immobiles(textureDictionary["cornerWall"], new Point(j, i), grid, Direction.Right, false);
                            break;
                        case "-":
                            //Adds a horizontal wall
                            levelObjectArray[i, j] = new Immobiles(textureDictionary["horizontalWall"], new Point(j, i), grid, Direction.Right, false);
                            break;
                        case "1":
                            //Adds a floor tile
                            levelObjectArray[i, j] = new Immobiles(textureDictionary["floorTile"], new Point(j, i), grid, Direction.Right, false);
                            break;
                        case "e":
                            levelObjectArray[i, j] = new Immobiles(textureDictionary["floorTile"], new Point(j, i), grid, Direction.Right, false);
                            Enemy jim = new Enemy(textureDictionary["enemyImage"], new Point(j, i), grid, Direction.Down, true, 0, 0, true, 3, false);
                            enemyManager.AddEnemy(jim);
                            break;
                        case "p":
                            //Adds a floor tile
                            levelObjectArray[i, j] = new Immobiles(textureDictionary["floorTile"], new Point(j, i), grid, Direction.Right, false);
                            //Adds a player
                            Player player = new Player(textureDictionary["playerImage"], new Point(j, i), grid, Direction.Right, true, 0, 0, true, 3);

                            break;
                    }
                }
            }

            return levelObjectArray;
        }

    }
}
