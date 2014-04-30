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

namespace BatGame
{
    class GameObjectManager
    {
        List<GameObject> gameObjects = new List<GameObject>();

        public GameObjectManager()
        {

        }

        public void AddGameObject(GameObject g)
        {
            gameObjects.Add(g);
        }

        public void GManagerUpdate(GameTime gameTime, Player player)
        {
            foreach (GameObject g in gameObjects)
            {
                if (g is Enemy)
                {
                    Enemy e = (Enemy)g;
                    e.EnemyUpdate(gameTime, player);
                }
                else
                {

                    //g.Update(gameTime, player);
                }
            }
        }

        /// <summary>
        /// Draws all gameObjects
        /// </summary>
        /// <param name="batch">spritebatch object</param>
        public void GManagerDraw(SpriteBatch batch)
        {
            foreach (GameObject g in gameObjects)
            {
                batch.Draw(g.ObjTexture, g.ObjRectangle, Color.White);
            }
        }

        public bool spotTaken(Point moveLocation)
        {
            foreach (GameObject g in gameObjects)
            {
                if (g.Position == moveLocation && g.IsSolid)
                {
                    return true;
                }
            }
            return false;
        }

        //blank in game object, override for specific scenerios
        /// <summary>
        /// checks if spot is taken by a specific kind of object
        /// </summary>
        /// <param name="moveLocation">location to check</param>
        /// <param name="obj">the game object</param>
        /// <returns>game object colliding with</returns>
        public GameObject inSpot(Point moveLocation)
        {
            //Console.WriteLine("here");
            foreach (GameObject g in gameObjects)
            {
                if (g.Position == moveLocation) //&& g.IsSolid)
                {
                    return g;
                }
            }
            return default(GameObject);
        }

        public int Count
        {
            get
            {
                return gameObjects.Count;
            }
        }

        public List<GameObject> GameObjects
        {
            get { return this.gameObjects; }
            set { this.gameObjects = value; }
        }

    }
}
